using System;
using System.Collections.Generic;

namespace CoachAPokeapi
{
	// https://pokeapi.co/docsv2/#pokemon-species
	public class Species
	{
		public class LanguageNode {
			public string Name, Url;
		}
		public class FlavorTextEntryNode
		{
			public string Flavor_text;
			public LanguageNode Language;
		}

		private static readonly string UnavailableDescriptionText = "(Description unavailable)";
		public string Name;
		public List<FlavorTextEntryNode> Flavor_text_entries;

		/// <summary>
		/// Fetches the english description using the flavors, if possible. Returns a default text or a description in another language if unavailable.
		/// </summary>
		/// <returns>The english description.</returns>
		public string GetEnglishDescriptionIfPossible() {
			if (Flavor_text_entries.Count == 0) {
				return UnavailableDescriptionText;
			}

			foreach (var flavor in Flavor_text_entries) {
				if (flavor.Language.Name == "en") {
					return flavor.Flavor_text;
				}
			}
			return Flavor_text_entries [0].Flavor_text;
		}
	}
}

