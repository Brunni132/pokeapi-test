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
	[Register ("PokemonDetailViewController")]
	partial class PokemonDetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel DescriptionLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel HeightLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel NameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView PokemonImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel WeightLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}
			if (HeightLabel != null) {
				HeightLabel.Dispose ();
				HeightLabel = null;
			}
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}
			if (PokemonImage != null) {
				PokemonImage.Dispose ();
				PokemonImage = null;
			}
			if (WeightLabel != null) {
				WeightLabel.Dispose ();
				WeightLabel = null;
			}
		}
	}
}
