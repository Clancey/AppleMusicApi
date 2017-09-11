using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1.Nist;
namespace AppleMusic
{
	public static class AppleMusicKeysHelper
	{
		public class DeveloperToken
		{
			public string Token { get; set; }
			public DateTime Created { get; set; }
			public DateTime Expiration { get; set; }
		}

		public static DeveloperToken GenerateDeveloperToken (string keyIdentifier, string teamId, string privateKey, int expirationInSeconds = 1577700, string algorithim = "ES256")
		{
			var token = CreateJwtToken (keyIdentifier, teamId, expirationInSeconds, privateKey);
			return new DeveloperToken {
				Token = token,
				Expiration = DateTime.UtcNow.AddSeconds (expirationInSeconds),
				Created = DateTime.UtcNow
			};
		}

		public static string CreateJwtToken (string kid, string teamId, int expirationInSeconds, string privateKey ,string alg = "ES256")
		{

			var startTime = DateTime.Today.ToUnixTime ();
			var exp = startTime + expirationInSeconds;
			var header = new Dictionary<string, object> {
				{"alg",alg},
				{"kid",kid}
			};
			var headerString = $"{{\"alg\":\"{alg}\" ,\"kid\":\"{kid}\"}}";
			var payload = new Dictionary<string, object> {
				{"iss", teamId},
				{"iat", startTime},
				{"exp", startTime + expirationInSeconds}
			};

			var packagesString = $"{{\"iss\":\"{teamId}\",\"iat\":{startTime},\"exp\":{exp}}}";
			var signed = SignES256(privateKey, headerString, packagesString);
			return signed;
		}

		public static string SignES256(string privateKey, string header, string payload)
		{
			//This works on Windows only!
			throw new NotImplementedException ();
			//CngKey key = CngKey.Import(
			//	Convert.FromBase64String(privateKey),
			//	CngKeyBlobFormat.Pkcs8PrivateBlob);

			//using (ECDsaCng dsa = new ECDsaCng(key))
			//{
			//	dsa.HashAlgorithm = CngAlgorithm.Sha256;
			//	var unsignedJwtData =
			//		Encode(Encoding.UTF8.GetBytes(header)) + "." + Encode(Encoding.UTF8.GetBytes(payload));
			//	var signature =
			//		dsa.SignData(Encoding.UTF8.GetBytes(unsignedJwtData));
			//	return unsignedJwtData + "." + Encode(signature);
			//}
		}
		public static long ToUnixTime (this DateTime date)
		{
			var epoch = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return Convert.ToInt64 ((date - epoch).TotalSeconds);
		}

		public static string Encode (byte [] input)
		{
			var output = Convert.ToBase64String (input);
			output.TrimEnd('=').Replace('+', '-').Replace ('/', '_');
			return output;
		}
	}
}
