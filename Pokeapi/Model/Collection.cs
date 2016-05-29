using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoachAPokeapi
{
	public class Collection<CollectionType>
	{
		public int Count;
		public string Next;
		public string Previous;
		public List<CollectionType> Results;

		public bool HasNext { get { return Next != null; } }

		public Task<Collection<CollectionType>> FetchNext() {
			return PokemonApi.Instance.FetchResource<Collection<CollectionType>> (Next);
		}
	}
}

