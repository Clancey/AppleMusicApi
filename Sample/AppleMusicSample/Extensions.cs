using System;
using Xamarin.Forms;
namespace AppleMusicSample
{
	public static class Extensions
	{
		public static T OnTap<T> (this T cell, Action<T> onTap) where T : Cell
		{
			cell.Tapped += (sender, e) => {
				onTap (cell);
			};
			return cell;
		}
	}
}
