// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CoachAPokeapi
{
	[Register ("PokemonLookupViewController")]
	partial class PokemonLookupViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton LookupButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField PlayerName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView ResultTable { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LookupButton != null) {
				LookupButton.Dispose ();
				LookupButton = null;
			}
			if (PlayerName != null) {
				PlayerName.Dispose ();
				PlayerName = null;
			}
			if (ResultTable != null) {
				ResultTable.Dispose ();
				ResultTable = null;
			}
		}
	}
}
