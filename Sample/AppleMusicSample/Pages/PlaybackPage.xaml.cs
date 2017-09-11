using System;
using System.Collections.Generic;

using Xamarin.Forms;
using AppleMusic;

namespace AppleMusicSample.Pages
{
	public partial class PlaybackPage : ContentPage
	{
		Song song;
		public Song CurrentSong {
			get => song;
			set {
				song = value;
				OnPropertyChanged ();
				OnPropertyChanged (nameof (PlayBackButtonEnabled));
			}
		}

		bool isPlaying;
		public bool IsPlaying {
			get => isPlaying;
			set {
				isPlaying = value;
				OnPropertyChanged ();
				OnPropertyChanged (nameof (PlaybackButtonText));
			}
		}

		public string PlaybackButtonText => IsPlaying ? "Pause" : "Play";

		public bool PlayBackButtonEnabled => CurrentSong != null;

		public PlaybackPage ()
		{
			InitializeComponent ();
			this.BindingContext = this;
		}
	}
}
