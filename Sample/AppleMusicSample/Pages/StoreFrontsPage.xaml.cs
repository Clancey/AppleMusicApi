using System;
using System.Collections.Generic;

using Xamarin.Forms;
using AppleMusic;
using System.Threading.Tasks;

namespace AppleMusicSample.Pages
{
	public partial class StoreFrontsPage : RefreshingContentPage
	{
		public StoreFrontsPage ()
		{
			InitializeComponent ();
		}

		IList<StoreFront> storeFronts;
		public IList<StoreFront> StoreFronts {
			get => storeFronts;
			set {
				storeFronts = value;
				OnPropertyChanged (nameof (StoreFronts));
			}
		}

		protected override async Task OnRefreshing ()
		{
			StoreFronts = (await App.Api.GetStoreFronts ()).Data;
		}

		public void Handle_ItemSelected (object sender, EventArgs e)
		{

		}
	}
}
