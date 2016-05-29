using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

namespace CoachAPokeapi
{
	/// <summary>
	/// Name lookup results table source.
	/// </summary>
	class ResultTableSource: UITableViewSource {
		List<SummaryPokemon> TableItems;
		string CellIdentifier = "TableCell";
		// TODO make event handler
		public Action<int> OnRowSelected; 

		public ResultTableSource (List<SummaryPokemon> items)
		{
			TableItems = items;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return TableItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);

			//---- if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, CellIdentifier);
			}

			string pokemonName = TableItems [indexPath.Row].BeautifiedName;
			cell.TextLabel.Text = pokemonName;
			cell.ImageView.Image = null;
			cell.DetailTextLabel.Text = "Loading pokédex data…";
			Task.Run(async () => {
				// Fetch in separate thread
				var pokemon = await TableItems [indexPath.Row].Pokemon;
				var icon = await pokemon.FetchDefaultFrontImage();
				// Need to run on main thread
				BeginInvokeOnMainThread(() => {
					cell.TextLabel.Text = string.Format("{0} {1}", pokemon.Id, pokemonName);
					cell.ImageView.Image = icon;
					cell.DetailTextLabel.Text = string.Format("Height: {0} cm, Weight: {1} lbs", pokemon.Height, pokemon.Weight);
				});
			});
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (OnRowSelected != null) {
				OnRowSelected (indexPath.Row);
			}
		}
	}
}
