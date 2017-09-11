using System;
using System.Collections.Generic;
using AppleMusic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace AppleMusicSample.Pages
{
	public partial class RecommendationsPage : RefreshingContentPage
	{
		public RecommendationsPage ()
		{
			InitializeComponent ();
		}

		public string Type { get; set; }

		IList<Recommendation> items;
		public IList<Recommendation> Items {
			get => items;
			set {
				items = value;
				OnPropertyChanged ();
			}
		}

		protected override async Task OnRefreshing ()
		{
			//Items = (await App.Api.GetRecentStations (PlaylistIds)).Data;
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
