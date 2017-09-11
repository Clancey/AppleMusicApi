using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AppleMusic
{
	#region Resources
	public class Resource<T> : Resource where T : Attributes
	{
		/// <summary>
		/// Attributes belonging to the resource (can be a subset of the attributes).
		///  The members are the names of the attributes defined in the object model.
		/// </summary>
		/// <value>The attributes.</value>
		[JsonProperty ("attributes")]
		public T Attributes { get; set; }
	}

	[JsonConverter (typeof (JsonResourceConverter))]
	public class Resource
	{
		/// <summary>
		/// Persistent identifier of the resource. This member is required.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty ("id")]
		public string Id { get; set; }

		/// <summary>
		/// The type of resource. This member is required.
		/// </summary>
		/// <value>The type.</value>
		[JsonProperty ("type")]
		public string Type { get; set; }

		/// <summary>
		/// A URL subpath that fetches the resource as the primary object. This member is only present in responses.
		/// </summary>
		/// <value>The HR ef.</value>
		[JsonProperty ("href")]
		public string HRef { get; set; }


		/// <summary>
		/// Relationships belonging to the resource (can be a subset of the relationships).
		///  The members are the names of the relationships defined in the object model.
		/// See Relationship (https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/AppleMusicWebServicesReference/RelationshipDictionary.html#//apple_ref/doc/uid/TP40017625-CH45-SW1) object for the values of the members.
		/// </summary>
		/// <value>The relationships.</value>
		[JsonProperty ("relationships")]
		public Relationships Relationships { get; set; }

		/// <summary>
		/// Information about the request or response. The members may be any of the endpoint parameters.
		/// </summary>
		/// <value>The meta.</value>
		[JsonProperty ("meta")]
		public JObject Meta { get; set; }
	}

	public class StoreFront : Resource<StoreFrontAttributes>
	{
	}

	public class Activity : Resource<AcivityAttributes>
	{

	}

	public class Album : Resource<AlbumAttributes>
	{

	}

	public class MusicVideo : Resource<MusicVideoAttributes>
	{

	}

	public class Playlist : Resource<PlaylistAttributes>
	{

	}

	public class Song : Resource<SongAttributes>
	{
	}

	public class Station : Resource<StationAttributes>
	{

	}

	public class Artist : Resource<MediaAttributes>
	{

	}

	public class Currator : Resource<CurratorAttributes>
	{

	}

	public class Chart : Resource<ChartAttributes>
	{

	}

	public class Recommendation : Resource<RecommendationAttributes>
	{

	}

	public class Genre : Resource<Attributes>
	{

	}

	public class Rating : Resource<RatingAttributes>
	{

	}

	#endregion //Resources

	#region Attributes

	public class Artwork
	{
		/// <summary>
		/// The average background color of the image.
		/// </summary>
		/// <value>The color of the background.</value>
		[JsonProperty ("bgColor")]
		public string BgColor { get; set; }

		/// <summary>
		/// The maximum height available for the image.
		/// </summary>
		/// <value>The height.</value>
		[JsonProperty ("height")]
		public int Height { get; set; }

		/// <summary>
		/// The primary text color that may be used if the background color is displayed.
		/// </summary>
		/// <value>The text color1.</value>
		[JsonProperty ("textColor1")]
		public string TextColor1 { get; set; }

		/// <summary>
		/// The secondary text color that may be used if the background color is displayed.
		/// </summary>
		/// <value>The text color2.</value>
		[JsonProperty ("textColor2")]
		public string TextColor2 { get; set; }

		/// <summary>
		/// The tertiary text color that may be used if the background color is displayed.
		/// </summary>
		/// <value>The text color3.</value>
		[JsonProperty ("textColor3")]
		public string TextColor3 { get; set; }

		/// <summary>
		/// The final post-tertiary text color that maybe be used if the background color is displayed.
		/// </summary>
		/// <value>The text color4.</value>
		[JsonProperty ("textColor4")]
		public string TextColor4 { get; set; }

		/// <summary>
		/// The URL to request the image asset. 
		/// The image file name must be preceded by {w}x{h},
		/// as placeholders for the width and height values described above (for example, {w}x{h}bb.jpg).
		/// </summary>
		/// <value>The URL.</value>
		[JsonProperty ("url")]
		public string Url { get; set; }

		/// <summary>
		/// The maximum width available for the image.
		/// </summary>
		/// <value>The width.</value>
		[JsonProperty ("width")]
		public int Width { get; set; }

		public string GetUrl (int width, int height) => Url?.Replace ("{w}", width.ToString ()).Replace ("{h}", height.ToString ());

		public string DefaultUrl => GetUrl (Width, Height);
	}

	public class EditorialNotes
	{
		/// <summary>
		/// Notes shown when the content is being prominently displayed.
		/// </summary>
		/// <value>The short.</value>
		[JsonProperty ("short")]
		public string Short { get; set; }

		/// <summary>
		/// Abbreviated notes shown in-line or when the content is shown alongside other content.
		/// </summary>
		/// <value>The standard.</value>
		[JsonProperty ("standard")]
		public string Standard { get; set; }
	}

	public class PlayParams
	{
		/// <summary>
		/// The ID of the content to use for playback.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty ("id")]
		public string Id { get; set; }

		/// <summary>
		/// The kind of the content to use for playback.
		/// </summary>
		/// <value>The kind.</value>
		[JsonProperty ("kind")]
		public string Kind { get; set; }
	}

	public class ResourceGroup<T>
	{
		/// <summary>
		/// One or more destination objects.
		/// </summary>
		/// <value>The data.</value>
		[JsonProperty ("data")]
		public T [] Data { get; set; }

		/// <summary>
		/// A URL subpath that fetches the resource as the primary object. This member is only present in responses.
		/// </summary>
		/// <value>The href.</value>
		[JsonProperty ("href")]
		public string Href { get; set; }

		/// <summary>
		/// Information about the request or response. The members may be any of the endpoint parameters.
		/// </summary>
		/// <value>The meta.</value>
		[JsonProperty ("meta")]
		public JObject Meta { get; set; }

		/// <summary>
		/// Link to the next page of data or results.
		///  Contains the offset query parameter that specifies the next page.
		/// See Fetch Resources by Page:
		/// (https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/AppleMusicWebServicesReference/RelationshipsandPagination.html#//apple_ref/doc/uid/TP40017625-CH135-SW3)
		/// </summary>
		/// <value>The next.</value>
		[JsonProperty ("next")]
		public string Next { get; set; }
	}

	public class Relationships
	{
		[JsonProperty ("artists")]
		public ResourceGroup<Artist> Artists { get; set; }

		[JsonProperty ("tracks")]
		public ResourceGroup<Song> Tracks { get; set; }

		[JsonProperty ("music-videos")]
		public ResourceGroup<MusicVideo> MusicVideos { get; set; }

		[JsonProperty ("curator")]
		public ResourceGroup<Currator> Curator { get; set; }

		[JsonProperty ("playlists")]
		public ResourceGroup<Playlist> Playlists { get; set; }

		[JsonProperty ("albums")]
		public ResourceGroup<Album> Albums { get; set; }

		/// <summary>
		/// The contents associated with the content recommendation type.
		/// </summary>
		/// <value>The contents.</value>
		[JsonProperty ("contents")]
		public ResourceGroup<Resource> Contents { get; set; }

		/// <summary>
		/// The recommendations associated with the group recommendation type.
		/// </summary>
		/// <value>The recommendations.</value>
		[JsonProperty ("recommendations")]
		public ResourceGroup<Resource> Recommendations { get; set; }
	}

	public class Attributes
	{
		/// <summary>
		/// The localized name of the Object
		/// </summary>
		/// <value>The name.</value>
		[JsonProperty ("name")]
		public string Name { get; set; }


		/// <summary>
		/// Extra attributes found durring deserialization
		/// </summary>
		/// <value>The extra attributes.</value>
		[JsonExtensionData]
		public IDictionary<string, JToken> ExtraAttributes { get; set; }
	}

	public class AlbumAttributes : MediaAttributes
	{
		/// <summary>
		/// Indicates whether the album is complete. If true, the album is complete; otherwise, it is not. An album is complete if it contains all its tracks and songs.
		/// </summary>
		/// <value><c>true</c> if is complete; otherwise, <c>false</c>.</value>
		[JsonProperty ("isComplete")]
		public bool IsComplete { get; set; }

		/// <summary>
		/// Indicates whether the album contains a single song.
		/// </summary>
		/// <value><c>true</c> if is single; otherwise, <c>false</c>.</value>
		[JsonProperty ("isSingle")]
		public bool IsSingle { get; set; }

		/// <summary>
		/// The number of tracks.
		/// </summary>
		/// <value>The track count.</value>
		[JsonProperty ("trackCount")]
		public int TrackCount { get; set; }
	}

	public class MediaAttributes : Attributes
	{
		/// <summary>
		/// The artist’s name.
		/// </summary>
		/// <value>The name of the artist.</value>
		[JsonProperty ("artistName")]
		public string ArtistName { get; set; }

		/// <summary>
		/// The album artwork.
		/// </summary>
		/// <value>The artwork.</value>
		[JsonProperty ("artwork")]
		public Artwork Artwork { get; set; }

		/// <summary>
		/// (Optional) The RIAA rating of the content. The possible values for this rating are clean and explicit. No value means no rating.
		/// </summary>
		/// <value>The content rating.</value>
		[JsonProperty ("contentRating")]
		public string ContentRating { get; set; }

		/// <summary>
		/// The copyright text.
		/// </summary>
		/// <value>The copyright.</value>
		[JsonProperty ("copyright")]
		public string Copyright { get; set; }

		/// <summary>
		/// (Optional) The notes about the album that appear in the iTunes Store.
		/// </summary>
		/// <value>The editorial notes.</value>
		[JsonProperty ("editorialNotes")]
		public EditorialNotes EditorialNotes { get; set; }

		/// <summary>
		/// The names of the genres associated with this album.
		/// </summary>
		/// <value>The genre names.</value>
		[JsonProperty ("genreNames")]
		public string [] GenreNames { get; set; }

		[JsonProperty ("playParams")]
		public PlayParams PlayParams { get; set; }

		[JsonProperty ("durationInMillis")]
		public int DurationInMillis { get; set; }

		[JsonProperty ("composerName")]
		public string ComposerName { get; set; }

		[JsonProperty ("discNumber")]
		public int DiscNumber { get; set; }

		/// <summary>
		/// (Optional) The number of the music video in the album’s track list.
		/// </summary>
		/// <value>The track number.</value>
		[JsonProperty ("trackNumber")]
		public int TrackNumber { get; set; }

		/// <summary>
		/// The URL for sharing content in the iTunes Store.
		/// </summary>
		/// <value>The URL.</value>
		[JsonProperty ("url")]
		public string Url { get; set; }


		/// <summary>
		/// The release date of the album in YYYY-MM-DD format.
		/// </summary>
		/// <value>The release date.</value>
		[JsonProperty ("releaseDate")]
		public string ReleaseDate { get; set; }
	}

	public class SongAttributes : MediaAttributes
	{
		/// <summary>
		/// (Optional) The song’s composer.
		/// </summary>
		/// <value>The composer.</value>
		[JsonProperty ("composer")]
		public string Composer { get; set; }

		/// <summary>
		/// (Optional, classical music only) The movement count of this song.
		/// </summary>
		/// <value>The movement count.</value>
		[JsonProperty ("movementCount")]
		public int MovementCount { get; set; }

		/// <summary>
		/// (Optional, classical music only) The movement name of this song.
		/// </summary>
		/// <value>The name of the movement.</value>
		[JsonProperty ("movementName")]
		public string MovementName { get; set; }

		/// <summary>
		/// (Optional, classical music only) The movement number of this song.
		/// </summary>
		/// <value>The movement number.</value>
		[JsonProperty ("movementNumber")]
		public int MovementNumber { get; set; }

		/// <summary>
		/// (Optional, classical music only) The name of the associated work.
		/// </summary>
		/// <value>The name of the work.</value>
		[JsonProperty ("workName")]
		public string WorkName { get; set; }
	}

	public class MusicVideoAttributes : MediaAttributes
	{
		/// <summary>
		/// (Optional) The video subtype associated with the content.
		/// </summary>
		/// <value>The type of the video sub.</value>
		[JsonProperty ("videoSubType")]
		public string VideoSubType { get; set; }
	}

	public class StationAttributes : MediaAttributes
	{
		/// <summary>
		/// Indicates whether the station is a live stream
		/// </summary>
		/// <value><c>true</c> if is live; otherwise, <c>false</c>.</value>
		[JsonProperty ("isLive")]
		public bool IsLive { get; set; }

		/// <summary>
		/// (Optional) The episode number of the station. Only emitted when the station represents an episode of a show or other content.
		/// </summary>
		/// <value>The episode number.</value>
		public int EpisodeNumber { get; set; }
	}

	public class PlaylistAttributes : MediaAttributes
	{
		/// <summary>
		/// Optional) The display name of the curator.
		/// </summary>
		/// <value>The name of the curator.</value>
		[JsonProperty ("curatorName")]
		public string CuratorName { get; set; }

		/// <summary>
		/// (Optional) A description of the playlist.
		/// </summary>
		/// <value>The description.</value>
		[JsonProperty ("description")]
		public EditorialNotes Description {
			get => EditorialNotes;
			set => EditorialNotes = value;
		}

		/// <summary>
		/// The date the playlist was last modified.
		/// </summary>
		/// <value>The last modified date.</value>
		[JsonProperty ("lastModifiedDate")]
		public DateTime LastModifiedDate { get; set; }

		/// <summary>
		/// The type of playlist.
		/// </summary>
		/// <value>The type of the playlist.</value>
		[JsonProperty ("playlistType")]
		public string PlaylistType { get; set; }

  	}

	public static class PlaylistType
	{
		/// <summary>
		/// A playlist created and shared by an Apple Music user.
		/// </summary>
		public const string UserShared = "user-shared";
		/// <summary>
		/// A playlist created by an Apple Music curator
		/// </summary>
		public const string Editorial = "editorial";
		/// <summary>
		/// A playlist created by an non-Apple curator or brand.
		/// </summary>
		public const string External = "external";
		/// <summary>
		/// A personalized playlist for an Apple Music user.
		/// </summary>
		public const string PersonalMix = "personal-mix";
	}

	public class StoreFrontAttributes : Attributes
	{
		/// <summary>
		/// The numeric ID of the storefront.
		/// </summary>
		/// <value>The storefront identifier.</value>
		[JsonProperty ("storefrontId")]
		public long StorefrontId { get; set; }

		/// <summary>
		/// The localizations that the storefront supports, represented as an array of language tags.
		/// </summary>
		/// <value>The supported language tags.</value>
		[JsonProperty ("supportedLanguageTags")]
		public string [] SupportedLanguageTags { get; set; }

		/// <summary>
		/// The default language for the storefront, represented as a language tag.
		/// </summary>
		/// <value>The default language tag.</value>
		[JsonProperty ("defaultLanguageTag")]
		public string DefaultLanguageTag { get; set; }

	}

	public class CurratorAttributes : Attributes
	{
		/// <summary>
		/// The curator artwork.
		/// </summary>
		/// <value>The artwork.</value>
		[JsonProperty ("artwork")]
		public Artwork Artwork { get; set; }

		/// <summary>
		/// (Optional) The notes about the curator.
		/// </summary>
		/// <value>The editorial notes.</value>
		[JsonProperty ("editorialNotes")]
		public EditorialNotes EditorialNotes { get; set; }

		/// <summary>
		/// The URL for sharing a curator in Apple Music.
		/// </summary>
		/// <value>The URL.</value>
		[JsonProperty ("url")]
		public string Url { get; set; }
	}

	public class AcivityAttributes : Attributes
	{
		/// <summary>
		/// The activity artwork.
		/// </summary>
		/// <value>The artwork.</value>
		[JsonProperty ("artwork")]
		public Artwork Artwork { get; set; }

		/// <summary>
		/// (Optional) The notes about the activity that appear in the iTunes Store.
		/// </summary>
		/// <value>The editorial notes.</value>
		[JsonProperty ("editorialNotes")]
		public EditorialNotes EditorialNotes { get; set; }

		/// <summary>
		/// The URL for sharing an activity in the iTunes Store.
		/// </summary>
		/// <value>The URL.</value>
		[JsonProperty ("url")]
		public string Url { get; set; }
	}

	public class ChartAttributes : Attributes
	{
		/// <summary>
		/// The chart identifier.
		/// </summary>
		/// <value>The chart.</value>
		[JsonProperty ("chart")]
		public string Chart { get; set; }

		/// <summary>
		/// An array of the objects that were requested ordered by popularity.
		/// For example, if songs were specified as the chart type in the request, the array contains Song objects
		/// </summary>
		/// <value>The data.</value>
		public Resource [] Data { get; set; }

		/// <summary>
		/// Link to the next page of data or results.
		/// Contains the offset query parameter that specifies the next page.
		/// See Fetch Resources by Page:
		/// (https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/AppleMusicWebServicesReference/RelationshipsandPagination.html#//apple_ref/doc/uid/TP40017625-CH135-SW3)
		/// </summary>
		/// <value>The next.</value>
		[JsonProperty ("next")]
		public string Next { get; set; }

	}

	public class RecommendationAttributes : Attributes
	{
		/// <summary>
		/// Whether the recommendation is of group type.
		/// </summary>
		/// <value><c>true</c> if is group recommendation; otherwise, <c>false</c>.</value>
		[JsonProperty ("isGroupRecommendation")]
		public bool IsGroupRecommendation { get; set; }

		/// <summary>
		/// The localized title for the recommendation.
		/// </summary>
		/// <value>The title.</value>
		[JsonProperty ("title")]
		public string Title {
			get => Name;
			set => Name = value;
		}

		/// <summary>
		/// The localized reason for the recommendation.
		/// </summary>
		/// <value>The reason.</value>
		[JsonProperty ("reason")]
		public string Reason { get; set; }

		/// <summary>
		/// The resource types supported by the recommendation.
		/// </summary>
		/// <value>The resource types.</value>
		[JsonProperty ("resourceTypes")]
		public string [] ResourceTypes { get; set; }

		/// <summary>
		/// The date in UTC format when the recommendation is updated.
		/// </summary>
		/// <value>The next update date.</value>
		[JsonProperty ("nextUpdateDate")]
		public DateTime NextUpdateDate { get; set; }
	}

	public class RatingAttributes : Attributes
	{
		[JsonProperty( "value")]
		public int Value { get; set; }
	}

	#endregion //Attributes

	#region Responses

	public class Response<T>
	{
		/// <summary>
		/// The primary data for a request or response. If data exists, an array of one or more resource objects. If no data exists, an empty array or null.
		/// </summary>
		/// <value>The data.</value>
		[JsonProperty ("data")]
		public T [] Data { get; set; }

		/// <summary>
		/// Gets the first data item if it exists. Useful for singluar responses.
		/// </summary>
		/// <value>The item.</value>
		[JsonIgnore]
		public T Item => Data == null ? default (T) : Data.FirstOrDefault ();
	}

	public class Response
	{
		/// <summary>
		/// An array of one or more errors that occurred executing the operation.
		/// </summary>
		/// <value>The errors.</value>
		[JsonProperty ("errors")]
		public Error [] Errors { get; set; }

		/// <summary>
		/// Information about the request or response. The members may be any of the endpoint parameters.
		/// </summary>
		/// <value>The meta.</value>
		[JsonProperty ("meta")]
		public JObject Meta { get; set; }

		/// <summary>
		/// Link to the next page of data or results.
		///  Contains the offset query parameter that specifies the next page.
		/// See Fetch Resources by Page:
		/// (https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/AppleMusicWebServicesReference/RelationshipsandPagination.html#//apple_ref/doc/uid/TP40017625-CH135-SW3)
		/// </summary>
		/// <value>The next.</value>
		[JsonProperty ("next")]
		public string Next { get; set; }

		/// <summary>
		/// Link to the request that generated the response data or results. Not present in a request.
		/// </summary>
		/// <value>The HRef.</value>
		[JsonProperty ("href")]
		public string HRef { get; set; }
	}

	public class SearchResponse : Response
	{
		/// <summary>
		/// The results of the operation. If there are results, the object contains contents; otherwise, it is empty or null.
		/// </summary>
		/// <value>The results.</value>
		[JsonProperty ("results")]
		public Relationships Results { get; set; }
	}

	public class SearchHintsResponse : Response
	{
		[JsonProperty ("results")]
		public SearchHintResult Results { get; set; }
	}

	public class SearchHintResult
	{
		[JsonProperty ("terms")]
		public string [] Terms { get; set; }
	}

	public class Error
	{
		/// <summary>
		/// A unique identifier for this occurrence of the error.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty ("id")]
		public string Id { get; set; }
		/// <summary>
		/// A link to more information about this occurrence.
		/// </summary>
		/// <value>The about.</value>
		[JsonProperty ("about")]
		public string About { get; set; }
		/// <summary>
		/// The HTTP status code for this problem.
		/// </summary>
		/// <value>The status.</value>
		[JsonProperty ("status")]
		public string Status { get; set; }

		/// <summary>
		/// The code for this error. For possible values,
		///  see HTTP Status Codes (https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/AppleMusicWebServicesReference/HTTPStatusCodes.html#//apple_ref/doc/uid/TP40017625-CH50-SW1).
		/// </summary>
		/// <value>The code.</value>
		[JsonProperty ("code")]
		public string Code { get; set; }

		/// <summary>
		/// A short description of the problem that may be localized.
		/// </summary>
		/// <value>The title.</value>
		[JsonProperty ("title")]
		public string Title { get; set; }

		/// <summary>
		/// A long description of the problem that may be localized.
		/// </summary>
		/// <value>The detail.</value>
		[JsonProperty ("detail")]
		public string Detail { get; set; }

		/// <summary>
		/// A object containing references to the source of the error. For possible members, see Source object.
		/// </summary>
		/// <value>The source.</value>
		[JsonProperty ("source")]
		public SourceObject Source { get; set; }

		/// <summary>
		/// Contains meta information about the error.
		/// </summary>
		/// <value>The meta.</value>
		[JsonProperty ("meta")]
		public JObject Meta { get; set; }
	}

	public class SourceObject
	{
		/// <summary>
		/// The URI query parameter that caused the error.
		/// </summary>
		/// <value>The parameter.</value>
		[JsonProperty ("parameter")]
		public string Parameter { get; set; }

		/// <summary>
		/// A pointer to the associated entry in the request document.
		/// </summary>
		/// <value>The pointer.</value>
		[JsonProperty ("pointer")]
		public JObject Pointer { get; set; }
	}

	#endregion //Responses

	public class JsonResourceConverter : JsonCreationConverter<Resource>
	{
		public static Dictionary<string, Type> TypeMappings = new Dictionary<string, Type>
		{
			{"activities",typeof (Activity)},
			{"albums",typeof (Album)},
			{"apple-curators",typeof (Currator)},
			{"curators",typeof (Currator)},
			{"artists",typeof (Artist)},
			{"charts",typeof (Chart)},
			{"music-videos",typeof (MusicVideo)},
			{"playlists",typeof (Playlist)},
			{"recommendations",typeof (Recommendation)},
			{"songs",typeof(Song)},
			{"stations",typeof (Station)},
			{"storefronts",typeof (StoreFront)},
		};

		protected override Resource Create (System.Type objectType, JObject jsonObject, JsonReader reader)
		{
			try {
				JToken token;
				if (jsonObject.TryGetValue ("type", StringComparison.CurrentCultureIgnoreCase, out token)) {
					var type = token?.ToString ()?.ToLower ();
					Type objType = typeof (Resource<Attributes>);
					if (!TypeMappings.TryGetValue (type, out objType))
						Console.WriteLine ($"Unknown Resource type:{type}");
					return (Resource)Activator.CreateInstance (objType);
				}
				return new Resource ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
			return new Resource ();
		}
	}

	public abstract class JsonCreationConverter<T> : JsonConverter
	{
		protected abstract T Create (System.Type objectType, JObject jsonObject, JsonReader reader);

		public override bool CanConvert (System.Type objectType)
		{
			return typeof (T).IsAssignableFrom (objectType);
		}

		public override object ReadJson (JsonReader reader, System.Type objectType,
		  object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load (reader);
			var target = Create (objectType, jsonObject, reader);
			serializer.Populate (jsonObject.CreateReader (), target);
			return target;
		}
		public override bool CanWrite => false;

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException ();
		}

	}
}
