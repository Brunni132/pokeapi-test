using System;
using System.Collections.Generic;

namespace CoachAPokeapi
{
	public class PokeLogic
	{
		/// <summary>
		/// Finds out the closest pokemon within a list.
		/// </summary>
		/// <returns>The closest pokemon.</returns>
		/// <param name="name">Player name.</param>
		/// <param name="list">List of pokemon as fetched via the PokemonApi.</param>
		public static SummaryPokemon GetClosestPokemonName(string name, List<SummaryPokemon> list) {
			int closestId = 0, closestDistance = LevenshteinDistance.Compute(name, list[0].Name);
			for (int i = 1; i < list.Count; i++) {
				int dist = LevenshteinDistance.Compute (name, list[i].Name);
				if (dist <= closestDistance) {
					closestId = i;
					closestDistance = dist;
				}
			}

			return list [closestId];
		}
	}
}

