using System;
using System.Collections.Generic;

using Xamarin.Forms;
using AppleMusic;

namespace AppleMusicSample.Pages
{
	public partial class PlaylistDetailsPage : ContentPage
	{
		public PlaylistDetailsPage ()
		{
			InitializeComponent ();
		}

		Playlist playlist;
		public Playlist Playlist {
			get => playlist;
			set {
				playlist = value;
				OnPropertyChanged ();
			}
		}

	}
}
