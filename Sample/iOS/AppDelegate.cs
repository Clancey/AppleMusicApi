using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using MediaPlayer;
using AppleMusic;

namespace AppleMusicSample.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		App CurrentApp;
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			LoadApplication (CurrentApp = new App { PlatformPlaySong = PlaySong });
			NSNotificationCenter.DefaultCenter.AddObserver (MPMusicPlayerController.PlaybackStateDidChangeNotification, PlaybackStateChanged);
			NSNotificationCenter.DefaultCenter.AddObserver (MPMusicPlayerController.NowPlayingItemDidChangeNotification, PlaybackStateChanged);
			return base.FinishedLaunching (app, options);
		}
		async void PlaySong (Song song)
		{
			try {
				CurrentPlayer.SetQueue (new [] { song.Id });
				await CurrentPlayer.PrepareToPlayAsync ();
				CurrentPlayer.Play ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}
		MPMusicPlayerController currentPlayer;
		MPMusicPlayerController CurrentPlayer {
			get => currentPlayer ?? (currentPlayer = CreatePlayer ());
			set => currentPlayer = value;
		}

		MPMusicPlayerController CreatePlayer ()
		{
			var player = MPMusicPlayerController.ApplicationMusicPlayer;
			player.RepeatMode = MPMusicRepeatMode.None;
			player.BeginGeneratingPlaybackNotifications ();
			return player;
		}

		void PlaybackStateChanged (NSNotification notification)
		{
			CurrentApp.IsPlaying (CurrentPlayer.CurrentPlaybackRate != 0);
		}

		void PlayerSongChanged (NSNotification notification)
		{
			Console.WriteLine ("Song Changed");
		}
	}
}
