using System;
using StoreKit;
using MediaPlayer;
using System.Threading.Tasks;
namespace AppleMusic
{
	public partial class AppleMusicApi
	{
		partial void NativeInit ()
		{
			GetUserToken = (account) => {

				return GetAppleUserAccount (account);
			};
		}
		public static bool AskUserForPermisionIfDenied { get; set; }
		SKCloudServiceController cloudServiceController = new SKCloudServiceController ();
		async Task<AppleAccount> GetAppleUserAccount (AppleAccount account)
		{
			try {



				var status = SKCloudServiceController.AuthorizationStatus;
				if (!AskUserForPermisionIfDenied && (status == SKCloudServiceAuthorizationStatus.Denied || status == SKCloudServiceAuthorizationStatus.Restricted)) {
					return account;
				}
				status = await SKCloudServiceController.RequestAuthorizationAsync ();

				var tcs = new TaskCompletionSource<SKCloudServiceCapability>();
				cloudServiceController.RequestCapabilities ((SKCloudServiceCapability arg1, Foundation.NSError arg2) => {
					if (arg2 != null)
						tcs.TrySetException (new Exception (arg2.ToString ()));
					else
						tcs.TrySetResult (arg1);
				});
				var capabilities = await tcs.Task;
				if (capabilities.HasFlag (SKCloudServiceCapability.AddToCloudMusicLibrary))
					account.CanAddToICloudMusic = true;
				if (capabilities.HasFlag (SKCloudServiceCapability.MusicCatalogPlayback))
					account.CanPlayMusic = true;
				if (capabilities.HasFlag (SKCloudServiceCapability.MusicCatalogSubscriptionEligible)) {
					account.EligibleForSubscription = true;
					var vc = new SKCloudServiceSetupViewController ();
					var result = await vc.LoadAsync (new SKCloudServiceSetupOptions { Action = SKCloudServiceSetupAction.Subscribe, AffiliateToken = AffiliateToken});
				}
				
				if (account.CanAddToICloudMusic || account.CanPlayMusic) {
					var token = await cloudServiceController.RequestUserTokenAsync (account.DeveloperToken);
					account.UserToken = token;
				}
				return account;
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
			return account;
		}
	}
}
