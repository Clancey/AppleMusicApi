using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using AppleMusic;
using System.Linq;

namespace AppleMusicSample.Pages
{
	public partial class AlbumDetailsPage : RefreshingContentPage
	{
		public AlbumDetailsPage ()
		{
			InitializeComponent ();
		}

		public string AlbumId { get; set; } = "310730204";

		Album album;
		public Album Album {
			get => album;
			set {
				album = value;
				OnPropertyChanged ();
			}
		}

		protected override async Task OnRefreshing ()
		{
			if (Album != null && Album.Relationships.Tracks?.Data?.Length  > 0) {
				return;
			}

			Album = (await App.Api.GetAlbum (AlbumId,true)).Item;
			
		}

		public void Handle_ItemSelected (object sender, EventArgs e)
		{
			var song = ListView.SelectedItem as Song;
			if (song == null)
				return;
			ListView.SelectedItem = null;
			(App.Current as App).PlaySong (song);
		}

	}
}
