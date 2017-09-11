using System;
using System.Threading.Tasks;
using SimpleAuth;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
namespace AppleMusic
{
	public class AppleAccount : Account
	{
		public string DeveloperToken { get; set; }
		public DateTime DeveloperTokenCreated { get; set; }
		public DateTime DeveloperTokenExpiration { get; set; }

		public string UserToken { get; set; }
		public bool CanAddToICloudMusic { get; set; }
		public bool CanPlayMusic { get; set; }
		public bool EligibleForSubscription { get; set; }
		public DateTime UserTokenCreated { get; set; }
		public DateTime UserTokenExpiration { get; set; }
		public override bool IsValid ()
		{
			return !string.IsNullOrWhiteSpace (UserToken) && UserTokenExpiration < DateTime.UtcNow;
		}
		public virtual bool IsDeveloperTokenValid ()
		{
			return !string.IsNullOrWhiteSpace (DeveloperToken) || DateTime.UtcNow < DeveloperTokenExpiration;
		}
	}
	public partial class AppleMusicApi : AuthenticatedApi
	{
		public static string DefaultUrl = "https://api.music.apple.com/";

		public static string ApiVersion = "v1";

		public string StoreFront { get; set; }

		public AppleMusicApi (string identifier, string encryptionKey = "appleMusic", HttpMessageHandler handler = null) : base (identifier, encryptionKey, handler)
		{
			BaseAddress = new Uri (DefaultUrl);
			NativeInit ();
		}

		partial void NativeInit ();

		public Func<Task<AppleAccount>> GetDeveloperToken { get; set; }

		public Func<AppleAccount, Task<AppleAccount>> GetUserToken { get; set; }

		public async Task<Response<StoreFront>> GetUserStoreFront (bool autoSetStorefront = true)
		{
			var resp = await Get<Response<StoreFront>> ("{version}/me/storefront");
			var storefront = resp.Data.FirstOrDefault ();

			if (autoSetStorefront)
				StoreFront = storefront.Id;
			return resp;
		}

		public async Task<Response<StoreFront>> GetStoreFronts ()
		{
			var resp = await Get<Response<StoreFront>> ("{version}/storefronts", authenticated: false);
			return resp;
		}

		public async Task<Response<Album>> GetAlbum (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/albums/{id}";
			var resp = await GetItem<Response<Album>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Album>> GetAlbums (params string [] ids)
		{
			return GetAlbums (ids, false, null);
		}
		public async Task<Response<Album>> GetAlbums (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/albums";
			var resp = await GetItems<Response<Album>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}

		public async Task<Response<MusicVideo>> GetMusicVideo (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/music-videos/{id}";
			var resp = await GetItem<Response<MusicVideo>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<MusicVideo>> GetMusicVideos (params string [] ids)
		{
			return GetMusicVideos (ids, false, null);
		}
		public async Task<Response<MusicVideo>> GetMusicVideos (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/music-videos";
			var resp = await GetItems<Response<MusicVideo>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}

		public async Task<Response<Playlist>> GetPlaylist (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/playlists/{id}";
			var resp = await GetItem<Response<Playlist>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Playlist>> GetPlaylists (params string [] ids)
		{
			return GetPlaylists (ids, false, null);
		}
		public async Task<Response<Playlist>> GetPlaylists (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/playlists";
			var resp = await GetItems<Response<Playlist>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}

		public async Task<Response<Song>> GetSong (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/songs/{id}";
			var resp = await GetItem<Response<Song>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Song>> GetSongs (params string [] ids)
		{
			return GetSongs (ids, false, null);
		}
		public async Task<Response<Song>> GetSongs (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/songs";
			var resp = await GetItems<Response<Song>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}

		public async Task<Response<Station>> GetStation (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/stations/{id}";
			var resp = await GetItem<Response<Station>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Station>> GetStations (params string [] ids)
		{
			return GetStations (ids, false, null);
		}
		public async Task<Response<Station>> GetStations (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/stations";
			var resp = await GetItems<Response<Station>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}

		public async Task<Response<Artist>> GetArtist (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/artists/{id}";
			var resp = await GetItem<Response<Artist>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Artist>> GetArtists (params string [] ids)
		{
			return GetArtists (ids, false, null);
		}

		public async Task<Response<Artist>> GetArtists (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/artists";
			var resp = await GetItems<Response<Artist>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}

		public async Task<Response<Currator>> GetCurrator (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/curators/{id}";
			var resp = await GetItem<Response<Currator>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Currator>> GetCurrators (params string [] ids)
		{
			return GetCurrators (ids, false, null);
		}

		public async Task<Response<Currator>> GetCurrators (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/curators";
			var resp = await GetItems<Response<Currator>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}


		public async Task<Response<Activity>> GetActivity (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/activities/{id}";
			var resp = await GetItem<Response<Activity>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Activity>> GetActivities (params string [] ids)
		{
			return GetActivities (ids, false, null);
		}

		public async Task<Response<Activity>> GetActivities (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/activities";
			var resp = await GetItems<Response<Activity>> (path, ids, includeAdditional, languageTags, false);

			return resp;
		}

		public async Task<Response<Currator>> GetAppleCurrator (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/apple-curators/{id}";
			var resp = await GetItem<Response<Currator>> (path, id, includeAdditional, languageTags, false);
			return resp;
		}

		public Task<Response<Currator>> GetAppleCurrators (params string [] ids)
		{
			return GetAppleCurrators (ids, false, null);
		}

		public async Task<Response<Currator>> GetAppleCurrators (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/charts";
			var resp = await GetItems<Response<Currator>> (path, ids, includeAdditional, languageTags, false);
			return resp;
		}

		/// <summary>
		/// Gets the charts.
		/// </summary>
		/// <returns>The charts.</returns>
		/// <param name="types">A list of the types of charts to include in the results. The possible values are albums, songs, and music-videos.</param>
		/// <param name="chart">(Optional) The chart to fetch for the specified types. For possible values, get all the charts by sending this endpoint without the chart parameter. The possible values for this parameter are the chart attributes of the Chart objects in the response.</param>
		/// <param name="genre">(Optional) The identifier for the genre to use in the chart results. To get the genre identifiers, go to Get top charts genres. (https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/AppleMusicWebServicesReference/GetPopularGenres.html#//apple_ref/doc/uid/TP40017625-CH15-SW1)</param>
		/// <param name="limit">(Optional) The number of resources to include per chart. The default value is 20 and the maximum value is 100.</param>
		/// <param name="offset">(Optional; only with chart specified) The next page or group of objects to fetch. See Fetch Resources by Page.</param>
		/// <param name="languageTags">(Optional) The localization to use, specified by a language tag. The possible values are in the supportedLanguageTags array belonging to the Storefront object specified by storefront. Otherwise, the storefront’s defaultLanguageTag is used.</param>
		public async Task<Response<Chart>> GetCharts (string [] types, string chart = null, string genre = null, int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/charts";
			var typesString = string.Join (",", types);
			var parameters = new Dictionary<string, string> { { "types", typesString } }.AddOptionals (false, languageTags);
			if (!string.IsNullOrWhiteSpace (chart))
				parameters ["chart"] = chart;
			if (!string.IsNullOrWhiteSpace (genre))
				parameters ["genre"] = genre;
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0) {
				if (string.IsNullOrWhiteSpace (chart))
					throw new ArgumentException ("Offset can only be used when you specify a chart");
				parameters ["offset"] = offset.ToString ();
			}

			var resp = await Get<Response<Chart>> (path, parameters, authenticated: false);
			return resp;
		}

		public async Task<Response<Genre>> GetGenres (int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/charts";
			var parameters = new Dictionary<string, string> ().AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0)
				parameters ["offset"] = offset.ToString ();

			var resp = await Get<Response<Genre>> (path, parameters, authenticated: false);
			return resp;
		}


		public async Task<SearchResponse> Search (string searchTerm, string [] types = null, int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/search";
			searchTerm = searchTerm.Replace (' ', '+');
			var parameters = new Dictionary<string, string> ().AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0)
				parameters ["offset"] = offset.ToString ();
			if (types?.Length > 0)
				parameters ["types"] = string.Join (",", types);

			var resp = await Get<SearchResponse> (path, parameters, authenticated: false);
			return resp;
		}

		public async Task<SearchHintsResponse> SearchHints (string searchTerm, string [] types = null, int limit = 0, string languageTags = null)
		{
			const string path = "{version}/catalog/{storefront}/hints";
			searchTerm = searchTerm.Replace (' ', '+');
			var parameters = new Dictionary<string, string> ().AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (types?.Length > 0)
				parameters ["types"] = string.Join (",", types);

			var resp = await Get<SearchHintsResponse> (path, parameters, authenticated: false);
			return resp;
		}


		public async Task<Response<Resource>> GetHeavyRotation (int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/catalog/me/history/heavy-rotation";
			var parameters = new Dictionary<string, string> ().AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0)
				parameters ["offset"] = offset.ToString ();

			var resp = await Get<Response<Resource>> (path, parameters, authenticated: true);
			return resp;
		}

		public async Task<Response<Resource>> GetRecentlyPlayed (int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/catalog/me/recent/played";
			var parameters = new Dictionary<string, string> ().AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0)
				parameters ["offset"] = offset.ToString ();

			var resp = await Get<Response<Resource>> (path, parameters, authenticated: true);
			return resp;
		}

		public async Task<Response<Station>> GetRecentStations (int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/catalog/me/recent/radio-stations";
			var parameters = new Dictionary<string, string> ().AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0)
				parameters ["offset"] = offset.ToString ();

			var resp = await Get<Response<Station>> (path, parameters, authenticated: true);
			return resp;
		}

		async Task<Response<Rating>> GetRating (string path, string id, bool includeAdditional = false, string languageTags = null)
		{
			var resp = await GetItem<Response<Rating>> (path, id, includeAdditional, languageTags, true);
			return resp;
		}

		Task<Response<Rating>> GetRatings (string path, params string [] ids)
		{
			return GetRatings (path, ids, false, null);
		}

		async Task<Response<Rating>> GetRatings (string path, string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			var resp = await GetItems<Response<Rating>> (path, ids, includeAdditional, languageTags, true);
			return resp;
		}

		async Task<Response<Rating>> SetRating (string path, string id, int rating)
		{
			if (rating == 1 || rating == -1)
				throw new ArgumentOutOfRangeException (nameof (rating), "Rating must be 1 or -1");
			var parameter = new Dictionary<string, string> { { "id", id } };
			var resp = await Put<Response<Rating>> (new { type = "rating", attributes = new { value = rating } }, path, parameter);
			return resp;
		}

		async Task<bool> DeleteRating (string path, string id)
		{
			var parameter = new Dictionary<string, string> { { "id", id } };
			await Delete (null, path, parameter);
			return true;
		}

		public Task<Response<Rating>> GetAlbumRating (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/albums/{id}";
			return GetRating (path, id, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> GetAlbumRatings (params string [] ids)
		{
			return GetAlbumRatings (ids, false, null);
		}

		public Task<Response<Rating>> GetAlbumRatings (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/albums";
			return GetRatings (path, ids, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> SetAlbumRating (string id, int rating)
		{
			const string path = "{version}/me/ratings/albums/{id}";
			return SetRating (path, id, rating);
		}

		public Task<bool> DeleteAlbumRating (string id)
		{
			const string path = "{version}/me/ratings/albums/{id}";
			return DeleteRating (path, id);
		}

		public Task<Response<Rating>> GetMusicVideoRating (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/albums/{id}";
			return GetRating (path, id, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> GetMusicVideoRatings (params string [] ids)
		{
			return GetMusicVideoRatings (ids, false, null);
		}

		public Task<Response<Rating>> GetMusicVideoRatings (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/music-videos";
			return GetRatings (path, ids, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> SetMusicVideoRating (string id, int rating)
		{
			const string path = "{version}/me/ratings/music-videos/{id}";
			return SetRating (path, id, rating);
		}

		public Task<bool> DeleteMusicVideoRating (string id)
		{
			const string path = "{version}/me/ratings/music-videos/{id}";
			return DeleteRating (path, id);
		}

		public Task<Response<Rating>> GetPlaylistRating (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/playlist/{id}";
			return GetRating (path, id, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> GetPlaylistRatings (params string [] ids)
		{
			return GetPlaylistRatings (ids, false, null);
		}

		public Task<Response<Rating>> GetPlaylistRatings (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/playlists";
			return GetRatings (path, ids, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> SetPlaylistRating (string id, int rating)
		{
			const string path = "{version}/me/ratings/playlists/{id}";
			return SetRating (path, id, rating);
		}

		public Task<bool> DeletePlaylistRating (string id)
		{
			const string path = "{version}/me/ratings/playlists/{id}";
			return DeleteRating (path, id);
		}

		public Task<Response<Rating>> GetSongRating (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/songs/{id}";
			return GetRating (path, id, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> GetSongRatings (params string [] ids)
		{
			return GetSongRatings (ids, false, null);
		}

		public Task<Response<Rating>> GetSongRatings (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/songs";
			return GetRatings (path, ids, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> SetSongRating (string id, int rating)
		{
			const string path = "{version}/me/ratings/songs/{id}";
			return SetRating (path, id, rating);
		}

		public Task<bool> DeleteSongsRating (string id)
		{
			const string path = "{version}/me/ratings/songs/{id}";
			return DeleteRating (path, id);
		}

		public Task<Response<Rating>> GetStationsRating (string id, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/ratings/stations/{id}";
			return GetRating (path, id, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> GetStationsRatings (params string [] ids)
		{
			return GetStationsRatings (ids, false, null);
		}

		public Task<Response<Rating>> GetStationsRatings (string [] ids, bool includeAdditional = false, string languageTags = null)
		{
			const string path = "{version}/me/stations/songs";
			return GetRatings (path, ids, includeAdditional, languageTags);
		}

		public Task<Response<Rating>> SetStationsRating (string id, int rating)
		{
			const string path = "{version}/me/ratings/stations/{id}";
			return SetRating (path, id, rating);
		}

		public Task<bool> DeleteStationsRating (string id)
		{
			const string path = "{version}/me/ratings/stations/{id}";
			return DeleteRating (path, id);
		}

		public Task<Response<Recommendation>> GetRecommendations (string type = null, int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/me/recommendations";
			var parameters = new Dictionary<string, string> ().AddOptionals (false, languageTags);
			if (!string.IsNullOrWhiteSpace (type))
				parameters ["type"] = type;
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0) {
				parameters ["offset"] = offset.ToString ();
			}

			return Get<Response<Recommendation>> (path, parameters);
		}

		public Task<Response<Recommendation>> GetAlbumRecommendations (int limit = 0, int offset = 0, string languageTags = null)
		{
			return GetRecommendations ("albums", limit, offset, languageTags);
		}

		public Task<Response<Recommendation>> GetPlaylistRecommendations (int limit = 0, int offset = 0, string languageTags = null)
		{
			return GetRecommendations ("playlists", limit, offset, languageTags);
		}

		public Task<Response<Recommendation>> GetReccomendation (string id, int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/me/recommendations/{id}";
			var parameters = new Dictionary<string, string> { { "id", id } }.AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0) {
				parameters ["offset"] = offset.ToString ();
			}
			return Get<Response<Recommendation>> (path, parameters);
		}

		public Task<Response<Recommendation>> GetReccomendations (string [] ids, int limit = 0, int offset = 0, string languageTags = null)
		{
			const string path = "{version}/me/recommendations/{id}";
			var idString = string.Join (",", ids);
			var parameters = new Dictionary<string, string> { { "ids", idString } }.AddOptionals (false, languageTags);
			if (limit > 0)
				parameters ["limit"] = limit.ToString ();
			if (offset > 0) {
				parameters ["offset"] = offset.ToString ();
			}
			return Get<Response<Recommendation>> (path, parameters);
		}

		#region helpers

		async Task<T> GetItem<T> (string path, string id, bool includeAdditional = false, string languageTags = null, bool authenticated = true)
		{
			var parameters = new Dictionary<string, string> { { "id", id } };
			parameters.AddOptionals (includeAdditional, languageTags);
			var resp = await Get<T> (path, parameters, authenticated: authenticated);
			return resp;
		}

		async Task<T> GetItems<T> (string path, string [] ids, bool includeAdditional = false, string languageTags = null, bool authenticated = true)
		{
			var idString = string.Join (",", ids);
			var parameters = new Dictionary<string, string> { { "ids", idString } }.AddOptionals (includeAdditional, languageTags);
			var resp = await Get<T> (path, parameters, authenticated: authenticated);
			return resp;
		}

		public AppleAccount CurrentAppleAccount {
			get => CurrentAccount as AppleAccount;
			set => CurrentAccount = value;
		}

		protected override Task<string> PrepareUrl (string path, bool authenticated = true)
		{
			if (path.Contains ("{storefront}")) {
				if (string.IsNullOrWhiteSpace (StoreFront))
					throw new Exception ("StoreFront is not set");
				path = path.Replace ("{storefront}", StoreFront);
			}
			path = path.Replace ("{version}", ApiVersion);
			return base.PrepareUrl (path, authenticated);
		}

		public override async Task<HttpResponseMessage> SendMessage (HttpRequestMessage message, bool authenticated = true, HttpCompletionOption completionOption = 0)
		{
			await VerifyDeveloperTokens ();
			return await base.SendMessage (message, authenticated, completionOption);
		}

		protected virtual async Task<bool> VerifyDeveloperTokens ()
		{
			if (CurrentAppleAccount?.IsDeveloperTokenValid () ?? false)
				return true;
			var account = await PerformDeveloperAuthenticate ();
			if (!account?.IsDeveloperTokenValid () ?? false)
				throw new Exception ("Developer token is invalid!!!!");
			await PrepareClient (Client);
			return true;
		}

		protected virtual async Task<AppleAccount> PerformDeveloperAuthenticate ()
		{
			if (GetDeveloperToken == null)
				throw new Exception ($"'{nameof (GetDeveloperToken)}' needs to be set");
			var account = CurrentAppleAccount ?? GetAccount<AppleAccount> (Identifier);
			if (account == null || !account.IsDeveloperTokenValid ()) {
				CurrentAppleAccount = account = await GetDeveloperToken ();
				SaveAccount (CurrentAppleAccount);
			}
			return account;
		}

		protected override async Task<Account> PerformAuthenticate ()
		{
			if (GetUserToken == null)
				throw new Exception ($"'{nameof (GetUserToken)}' needs to be set");
			var account = CurrentAppleAccount ?? GetAccount<AppleAccount> (Identifier);
			if (account == null || !account.IsDeveloperTokenValid ()) {
				CurrentAppleAccount = account = await PerformDeveloperAuthenticate ();
				SaveAccount (CurrentAppleAccount);
			}
			if (!account.IsValid ()) {
#if __IOS__
				account = await GetAppleUserAccount (account);
#else
				account = await GetUserToken (account);
#endif
			}
			if (account?.IsValid () ?? false) {
				CurrentAppleAccount = account = await PerformDeveloperAuthenticate ();
				SaveAccount (CurrentAppleAccount);
			}
			return account;
		}
		public override async Task PrepareClient (HttpClient client)
		{
			if (!string.IsNullOrWhiteSpace (CurrentAppleAccount?.DeveloperToken))
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", CurrentAppleAccount.DeveloperToken);

			//Music-User-Token
			if (!string.IsNullOrWhiteSpace (CurrentAppleAccount?.UserToken))
				client.DefaultRequestHeaders.Add ("Music-User-Token", CurrentAppleAccount.UserToken);
		}

		protected override async Task<bool> RefreshAccount (Account account)
		{
			var a = await PerformAuthenticate ();
			return a.IsValid ();
		}
	}

	static class ApiExtensions
	{
		public static Dictionary<string, string> AddOptionals (this Dictionary<string, string> dict, bool include, string local)
		{
			if (include)
				dict.Add ("include", "true");
			if (!string.IsNullOrWhiteSpace (local))
				dict.Add ("l", local);
			return dict;
		}
	}
#endregion //helpers
}
