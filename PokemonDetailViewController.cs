using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Threading.Tasks;

namespace CoachAPokeapi
{
	partial class PokemonDetailViewController : UIViewController
	{
		private SummaryPokemon ViewedPokemon;
		public PokemonDetailViewController (IntPtr handle) : base (handle) {}

		/// <summary>
		/// Call this at initialization to give the necessary information.
		/// </summary>
		public void InitWithPokemon(SummaryPokemon pokemon) {
			ViewedPokemon = pokemon;
		}

		public override void ViewDidLoad ()
		{
			NavigationItem.Title = ViewedPokemon.BeautifiedName;
			NameLabel.Text = ViewedPokemon.BeautifiedName;
			PokemonImage.Layer.MagnificationFilter = "nearest";
			DescriptionLabel.Text = "Fetching data, please wait…";

			UpdateUiWithPokemon (ViewedPokemon.Url);
		}

		// Run on main thread
		private void UpdateUiWithPokemon(string pokemonUrl) {
			Task.Run (async () => {
				var pokemon = await PokemonApi.Instance.FetchResource<Pokemon> (pokemonUrl);
				var image = await pokemon.FetchDefaultFrontImage();
				var description = await pokemon.FetchDescription();

				BeginInvokeOnMainThread(() => {
					// TODO it's not centimeters, it's not clear; charmander is supposed to be 2'0" but is 6 something in the DB
					HeightLabel.Text = string.Format("{0} cm", pokemon.Height);
					WeightLabel.Text = string.Format("{0} lbs", pokemon.Weight);
					DescriptionLabel.Text = description.Replace("\n", " ");
					DescriptionLabel.LineBreakMode = UILineBreakMode.WordWrap;
					DescriptionLabel.TextAlignment = UITextAlignment.Justified;
					DescriptionLabel.Lines = 0;
					DescriptionLabel.SizeToFit();
					PokemonImage.Image = image;
				});
			});
		}
	}
}
