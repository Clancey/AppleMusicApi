using System;

using Xamarin.Forms;
using AppleMusicSample.Pages;
using System.Linq;

namespace AppleMusicSample
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
			Title = "Apple Music";
			Content = new TableView {
				Root = new TableRoot ("Apple Music") {
					new TableSection("Sign in"){
						new TextCell{Text = "Get Developer Token"}.OnTap (async (cell)=>{
							var token = await App.Api.GetDeveloperToken() ;
						}),
						new TextCell{Text = "Get User Token"}.OnTap (async (cell)=>{
							var account = await App.Api.Authenticate() ;
						}),
					},
					new TableSection("Store"){
						new TextCell{Text = "Get Stores"}.OnTap (async (obj) => {
							await Navigation.PushAsync (new StoreFrontsPage());
						}),
						new TextCell{Text = "Get User Store"}.OnTap (async (obj) => {
							var store = (await App.Api.GetUserStoreFront ()).Item;
							await this.DisplayAlert ("Users store",store.Attributes.Name,"Ok");
						}),
					},
					new TableSection ("Library") {
						new TextCell {Text = "Get Albums"}.OnTap (async (cell) =>{
							await Navigation.PushAsync (new AlbumsPage());
						}),
						new TextCell {Text = "Get Single Album"}.OnTap (async (obj) => {
							await Navigation.PushAsync (new AlbumDetailsPage());
						}),
						new TextCell {Text = "Get Music Videos"}.OnTap (async (obj) => {
							await Navigation.PushAsync (new MusicVideosPage());
						}),
						new TextCell {Text = "Get Single Music Video"}.OnTap (async (obj) => {
							await Navigation.PushAsync (new MusicVideoDetailsPage());
						}),
						new TextCell {Text = "Get Playlists"}.OnTap (async (obj) => {
							await Navigation.PushAsync (new PlaylistsPage());
						}),
						new TextCell {Text = "Get Single Playlist"}.OnTap (async (obj) => {
							await Navigation.PushAsync (new PlaylistDetailsPage());
						}),
					},
					new TableSection ("Reccomendations"){
						new TextCell {Text = "Reccomendations"}.OnTap (async (obj) => {
							await Navigation.PushAsync (new RecommendationsPage());
						}),
					}
				}
			};
		}
	}
}

