using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using AppleMusic;

namespace AppleMusicSample.Pages
{
	public partial class MusicVideoDetailsPage : RefreshingContentPage
	{
		public MusicVideoDetailsPage ()
		{
			InitializeComponent ();
		}

		public string MusicVideoId { get; set; } = "639032181";

		MusicVideo musicVideo;
		public MusicVideo MusicVideo {
			get => musicVideo;
			set {
				musicVideo = value;
				OnPropertyChanged ();
			}
		}

		protected override async Task OnRefreshing ()
		{
			if (MusicVideo != null) {
				return;
			}

			MusicVideo = (await App.Api.GetMusicVideo (MusicVideoId)).Item;
		}

	}
}
