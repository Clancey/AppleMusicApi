using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using AppleMusic;

namespace AppleMusicSample.Pages
{
	public partial class MusicVideosPage : RefreshingContentPage
	{
		public MusicVideosPage ()
		{
			InitializeComponent ();
		}

		IList<MusicVideo> items;
		public IList<MusicVideo> Items {
			get => items;
			set {
				items = value;
				OnPropertyChanged ();
			}
		}

		public string [] MusicVideosIds { get; set; } = new [] { "639032181", "870852283" };

		protected override async Task OnRefreshing ()
		{
			Items = (await App.Api.GetMusicVideos (MusicVideosIds)).Data;
		}

		public void Handle_ItemSelected (object sender, EventArgs e)
		{
			var video = ListView.SelectedItem as MusicVideo;
			if (video == null)
				return;
			ListView.SelectedItem = null;
			Navigation.PushAsync (new MusicVideoDetailsPage { MusicVideo = video });
		}
	}
}
