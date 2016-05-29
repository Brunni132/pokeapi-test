// TODO Credit: http://www.dotnetperls.com/levenshtein
using System;
using System.Collections.Generic;

namespace CoachAPokeapi
{
	/// <summary>
	/// Contains approximate string matching
	/// </summary>
	static class LevenshteinDistance {
		/// <summary>
		/// Compute the distance between two strings.
		/// </summary>
		public static int Compute(string s, string t)
		{
			int n = s.Length;
			int m = t.Length;
			int[,] d = new int[n + 1, m + 1];

			// Step 1
			if (n == 0)
			{
				return m;
			}

			if (m == 0)
			{
				return n;
			}

			// Step 2
			for (int i = 0; i <= n; d[i, 0] = i++)
			{
			}

			for (int j = 0; j <= m; d[0, j] = j++)
			{
			}

			// Step 3
			for (int i = 1; i <= n; i++)
			{
				//Step 4
				for (int j = 1; j <= m; j++)
				{
					// Step 5
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

					// Step 6
					d[i, j] = Math.Min(
						Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						d[i - 1, j - 1] + cost);
				}
			}
			// Step 7
			return d[n, m];
		}

		/// <summary>
		/// Finds the closest match between a word and a list of words.
		/// </summary>
		/// <returns>The closest match.</returns>
		/// <param name="word">The word to look for.</param>
		/// <param name="words">List of words, must contain at least one item.</param>
		public static string FindClosestMatch(string word, string[] words) {
			int closestId = 0, closestDistance = Compute(word, words[0]);
			for (int i = 1; i < words.Length; i++) {
				int dist = Compute (word, words [i]);
				if (dist <= closestDistance) {
					closestId = i;
					closestDistance = dist;
				}
			}
			return words [closestId];
		}
	}

}

