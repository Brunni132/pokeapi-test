using System;
using System.Threading.Tasks;
using UIKit;
using System.Globalization;

namespace CoachAPokeapi
{
	/// <summary>
	/// Pokemon as returned for a list of pokemon.
	/// </summary>
	public class SummaryPokemon
	{
		public string Name;
		public string Url;

		public string BeautifiedName {
			get {
				return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name);
			}
		}

		// Fetches the actual pokemon with more detail
		public Task<Pokemon> Pokemon {
			get {
				return PokemonApi.Instance.FetchResource<Pokemon> (Url);
			}
		}

	}
}

