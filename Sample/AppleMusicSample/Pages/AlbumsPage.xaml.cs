using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using AppleMusic;

namespace AppleMusicSample.Pages
{
	public partial class AlbumsPage : RefreshingContentPage
	{
		public AlbumsPage ()
		{
			this.Title = "Albums";
			InitializeComponent ();
		}

		public string [] AlbumIds { get; set; } = new [] { "310730204", "190758914" };

		IList<Album> albums;
		public IList<Album> Albums { 
			get => albums;
			set {
				albums = value;
				OnPropertyChanged ();
			}
		}

		protected override async Task OnRefreshing ()
		{
			Albums = (await App.Api.GetAlbums (AlbumIds)).Data;
		}

		public void Handle_ItemSelected (object sender, EventArgs e)
		{
			var album = ListView.SelectedItem as Album;
			if (album == null)
				return;
			ListView.SelectedItem = null;
			Navigation.PushAsync (new AlbumDetailsPage { Album = album });
		}
	}
}
