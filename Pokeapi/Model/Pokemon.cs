using System;
using UIKit;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoachAPokeapi
{
	public class Pokemon
	{
		public class SpritesNode
		{
			public string back_female, back_shiny_female, back_default, front_female, front_shiny_female, back_shiny, front_default, front_shiny;
		}
		public class SpeciesNode {
			public string Name, Url;
		}

		// Public getters/setters to go quick
		public int Id;
		public string Name;
		public int Order;
		public int Weight;
		public int Height;
		// TODO there since (game_indices)
		// TODO Capabilities (moves)
		public SpritesNode Sprites;
		public SpeciesNode Species;
		private Task<UIImage> defaultBackImage, defaultFrontImage;

		public Task<UIImage> FetchDefaultBackImage() {
			return defaultBackImage = defaultBackImage ?? PokemonApi.LoadImageFromUrl (Sprites.back_default);
		}

		public Task<UIImage> FetchDefaultFrontImage() {
			return defaultFrontImage = defaultFrontImage ?? PokemonApi.LoadImageFromUrl (Sprites.front_default);
		}

		public async Task<string> FetchDescription() {
			var species = await PokemonApi.Instance.FetchResource<Species> (Species.Url);
			return species.GetEnglishDescriptionIfPossible ();
		}
	}
}

