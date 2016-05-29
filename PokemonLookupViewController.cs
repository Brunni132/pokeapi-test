using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

namespace CoachAPokeapi
{
	partial class PokemonLookupViewController : UIViewController
	{
		private Task ListingPokemonTask;
		private LoadingOverlay LoadingOverlay;
		private List<SummaryPokemon> PokemonList;
		private SummaryPokemon ResultPokemon;

		public PokemonLookupViewController (IntPtr handle) : base (handle) {}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Upon startup, preload the list of pokemon that we'll need later on anyway
			RunListingPokemonTask();

			// Validate with enter/go button
			PlayerName.ClearButtonMode = UITextFieldViewMode.WhileEditing;
			PlayerName.ShouldReturn += (UITextField field) => {
				DoLookupPokemon ();
				return false;
			};
			// Or by click on the appropriate button
			LookupButton.TouchUpInside += (object sender, EventArgs e) => {
				DoLookupPokemon();
			};
		}

		// Run from main thread.
		private void DoLookupPokemon() {
			string name = PlayerName.Text;
			PlayerName.ResignFirstResponder ();

			if (name.Length < 1) {
				ShowMessageBox ("Please correct input", "Please input your name in order to proceed");
				return;
			}

			if (!ListingPokemonTask.IsCompleted) {
				ShowActivityIndicator ();
			}
			Task.Run (() => LookupPokemon (name));
		}

		private async Task LookupPokemon(string playerName) {
			// Wait that we have the list of pokemon loaded
			await ListingPokemonTask;

			var results = new List<SummaryPokemon>();
			results.Add (PokeLogic.GetClosestPokemonName (playerName, PokemonList));
			ResultPokemon = results [0];

			// Once found print it
			BeginInvokeOnMainThread (() => {
				var source = new ResultTableSource (results);
				source.OnRowSelected = this.RowSelected;
				ResultTable.Source = source;
				ResultTable.ReloadData();
				HideActivityIndicator();
			});
		}

		// Run from main thread.
		private void HideActivityIndicator() {
			lock (this) {
				if (LoadingOverlay != null) {
					LoadingOverlay.RemoveFromSuperview ();
					LoadingOverlay = null;
				}
			}
		}

		// Go to the pokemon detail screen
		private void RowSelected(int index) {
			var next = this.Storyboard.InstantiateViewController("PokemonDetailViewController") as PokemonDetailViewController;
			next.InitWithPokemon (ResultPokemon);
			NavigationController.PushViewController (next, true);
		}

		/// <summary>
		/// Launchs the pokemon lookup task. Wait for finished by await ListingPokemonTask.
		/// </summary>
		private void RunListingPokemonTask() {
			ListingPokemonTask = Task.Run (async () => {
				// List the pokemon names
				var list = await PokemonApi.Instance.FetchPokemonList();
				var pokemons = new List<SummaryPokemon>();

				while (list != null) {
					foreach (var p in list.Results) {
						pokemons.Add(p);
					}

					if (list.HasNext) {
						Console.WriteLine("Making additional request to " + list.Next);
						list = await list.FetchNext();
					}
					else {
						list = null;
					}
				}

				lock (this) {
					PokemonList = pokemons;
				}
			});
		}

		// Run from main thread.
		private void ShowActivityIndicator() {
			var bounds = UIScreen.MainScreen.Bounds;

			// show the loading overlay on the UI thread using the correct orientation sizing
			lock (this) {
				LoadingOverlay = new LoadingOverlay (bounds);
				View.Add (LoadingOverlay);
			}
		}

		// Run from main thread.
		private void ShowMessageBox(string title, string message) {
			UIAlertView alert = new UIAlertView () {
				Title = title, Message = message
			};
			alert.AddButton("OK");
			alert.Show ();
		}
	}
}
