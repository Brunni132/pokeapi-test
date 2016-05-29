using System;
using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using UIKit;
using Foundation;

namespace CoachAPokeapi
{
	/// <summary>
	/// Singleton class exposing methods to interact with the Pokemon API.
	/// </summary>
	public class PokemonApi
	{
		private static string ApiPath = "http://pokeapi.co/api/v2/";
		// Ask for 500 pokemon at once
		private static int PokemonListedCount = 500;
//		private static string ApiPath = "http://localhost/~Florian/pokesample/api/v2/";
		private static readonly PokemonApi instance = new PokemonApi();
		private RestClient Client;

		private PokemonApi() {}

		/// <summary>
		/// Use this class as a singleton.
		/// </summary>
		/// <value>The instance.</value>
		public static PokemonApi Instance { get { return instance; } }

		/// <summary>
		/// Meant to be used externally to GET an API resource, given an URL returned by a previous call.
		/// </summary>
		/// <returns>The result to be awaited for.</returns>
		/// <param name="url">Resource URL (may have protocol+server or just be relative).</param>
		/// <typeparam name="T">Expected deserialized return type.</typeparam>
		public Task<T> FetchResource<T>(string url) {
			// HACK We accept prefixed strings too, but the REST client doesn't
			if (url.StartsWith (ApiPath)) {
				url = url.Substring(ApiPath.Length);
			}
			else if (url.ToLower().StartsWith("http")) {
				Console.WriteLine ("Warning: URL " + url + " is apparently not targetting the server");
			}

			var request = new RestRequest (url);
			return DoRequest<T> (request);
		}

		public Task<Collection<SummaryPokemon>> FetchPokemonList() {
			var request = new RestRequest ("pokemon");
			request.AddQueryParameter ("limit", PokemonListedCount.ToString());
			return DoRequest<Collection<SummaryPokemon>> (request);
		}

		public Task<Pokemon> FetchPokemon(int id) {
			var request = new RestRequest (String.Format (ApiPath + "pokemon/{0}", id));
			return DoRequest<Pokemon> (request);
		}

		/// <summary>
		/// Helper method to load an image from URL.
		/// </summary>
		/// <returns>The image from URL.</returns>
		/// <param name="absoluteUrl">Absolute URL.</param>
		public static Task<UIImage> LoadImageFromUrl(string absoluteUrl) {
			return Task.Run<UIImage> (() => {
				var url = new NSUrl (absoluteUrl);
				var data = NSData.FromUrl (url);
				return UIImage.LoadFromData (data);
			});
		}

		/// <summary>
		/// To be used privately.
		/// </summary>
		/// <returns>The result to be awaited for.</returns>
		/// <param name="request">Request to perform.</param>
		/// <typeparam name="T">Expected deserialized return type.</typeparam>
		private Task<T> DoRequest<T>(RestRequest request) {
			return Task<T>.Run(() => {
				// Create the client on the first time
				Client = Client ?? new RestClient (ApiPath);
				var response = Client.Execute(request);
				Console.WriteLine("Got object " + response.Content);
				return JsonConvert.DeserializeObject<T>(response.Content);
			});
		}
	}
}

