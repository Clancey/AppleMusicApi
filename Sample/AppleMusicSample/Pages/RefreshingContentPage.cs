using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace AppleMusicSample.Pages
{
	public abstract class RefreshingContentPage : ContentPage
	{
		public RefreshingContentPage ()
		{
			BindingContext = this;
		}

		protected override void OnAppearing ()
		{
			Refresh ();
			base.OnAppearing ();
		}

		public void Handle_Refreshing (object sender, System.EventArgs e)
		{
			Refresh ();
		}

		Task refreshingTask;
		public Task Refresh ()
		{
			if (refreshingTask?.IsCompleted ?? true)
				refreshingTask = refreshing ();
			return refreshingTask;
		}

		bool isRefreshing;
		public bool IsRefreshing {
			get => isRefreshing;
			set {
				if (isRefreshing == value)
					return;
				isRefreshing = value;
				OnPropertyChanged (nameof(IsRefreshing));
			}
		}

		async Task refreshing ()
		{
			try {
				IsRefreshing = true;
				await OnRefreshing ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			} finally {
				IsRefreshing = false;
			}
		}

		protected abstract Task OnRefreshing ();
	}
}

