using System;
using System.Collections.Generic;

using Xamarin.Forms;
using AppleMusic;
using System.Threading.Tasks;

namespace AppleMusicSample.Pages
{
	public partial class PlaylistsPage : RefreshingContentPage
	{
		public PlaylistsPage ()
		{
			InitializeComponent ();
		}


		IList<Playlist> items;
		public IList<Playlist> Items {
			get => items;
			set {
				items = value;
				OnPropertyChanged ();
			}
		}

		public string [] PlaylistIds { get; set; } = new [] { "pl.acc464c750b94302b8806e5fcbe56e17", "pl.97c6f95b0b884bedbcce117f9ea5d54b" };

		protected override async Task OnRefreshing ()
		{
			Items = (await App.Api.GetPlaylists (PlaylistIds)).Data;
		}

		public void Handle_ItemSelected (object sender, EventArgs e)
		{
			var playlist = ListView.SelectedItem as Playlist;
			if (playlist == null)
				return;
			ListView.SelectedItem = null;
			Navigation.PushAsync (new PlaylistDetailsPage { Playlist = playlist });
		}
	}
}
