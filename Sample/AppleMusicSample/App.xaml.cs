using System;
using Xamarin.Forms;
using AppleMusic;
using System.Threading.Tasks;
using AppleMusicSample.Pages;

namespace AppleMusicSample
{
	public partial class App : Application
	{
		public static AppleMusicApi Api = new AppleMusicApi ("AppleMusic") {
			StoreFront = "us",
			GetDeveloperToken = () =>{
				return Task.FromResult (new AppleAccount {
					//TODO: Get this from your server!!!
					DeveloperToken = ApiKeys.DeveloperToken,
					DeveloperTokenExpiration = ApiKeys.DeveloperTokenExpiration,
				});
			},
			GetUserToken = (account) => {
				throw new Exception ("Not available Cross platform");
			}
		};
		PlaybackPage playbackPage;
		public App ()
		{
			InitializeComponent ();

			MainPage = new TabbedPage {
				Children = {
					new NavigationPage(new MainPage ()){Title = "Browse"},
					new NavigationPage(playbackPage = new PlaybackPage()){Title = "Controls"},
				}
			};
		}
		public async Task PlaySong (Song song)
		{
			var account = await Api.Authenticate () as AppleAccount;
			if (!account.CanPlayMusic) {
				if (account.EligibleForSubscription)
					//TODO: Show Signup
					await this.MainPage.DisplayAlert ("Error", "Please sign up for Apple Music", "Ok");
				else
					await this.MainPage.DisplayAlert ("Error", "Apple Music is not available", "Ok");
				return;
			}


			playbackPage.CurrentSong = song;
			PlatformPlaySong (song);
		}
		public Action<Song> PlatformPlaySong { get; set; }

		public Action<bool> IsPlaying { get; set; }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
