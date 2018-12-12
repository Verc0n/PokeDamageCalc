using PokeAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDamageCalculator
{
    public static class Helper
    {
        public static T[] GetEnumValues<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("GetValues<T> can only be called for types derived from System.Enum", "T");
            }
            return (T[])Enum.GetValues(typeof(T));
        }

        public static double GetNatureModifierValue(this Nature n_, Stats stat_)
        {
            string stat = stat_.ToString().ToLower();
            stat.Replace("special", "special-");

            if (n_.IncreasedStat.Name == stat) return 1.1;
            if (n_.DecreasedStat.Name == stat) return 0.9;
            return 1;
        }


    }

    public static class Stringp
    {
        public static string ToUpperFirst(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }

    public static class Mathp
    {
        public static int Clamp(this int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static int PokeRound(double value)
        {
            if (value % 1 == 0.5) return Convert.ToInt32(value);
            return Convert.ToInt32(Math.Round(value, 0));
        }
    }

    public class VerconHttpBackend : IDataBackend
    {
        public readonly static string
            DefaultSiteURL = "https://vercon-pokeapi.netlify.com",
            DefaultBaseURL = DefaultSiteURL + "/api/v2/",
            DefaultUserAgent =
        "PokeAPI.NET (https://gitlab.com/PoroCYon/PokeApi.NET or a fork of it)",

            SLASH = "/";

        readonly HttpClient client = new HttpClient();
        readonly string ubase;

        public VerconHttpBackend(string baseurl = null, string userAgent = null)
        {
            ubase = String.IsNullOrWhiteSpace(baseurl)
                ? DefaultBaseURL : baseurl;

            if (ubase[ubase.Length - 1] != '/') ubase += SLASH;

            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                    String.IsNullOrWhiteSpace(userAgent)
                        ? DefaultUserAgent : userAgent);
        }

        public Task<Stream> GetStreamAsync(string path) =>
            client.GetStreamAsync(ubase + path);
        public Task<string> GetStringAsync(string path) =>
            client.GetStringAsync(ubase + path);

        public string MakePathRelative(string path)
        {
            if (path.StartsWith(ubase)) return path.Substring(ubase.Length);

            return path; // relative or invalid
        }
        public Uri MakeUriRelative(Uri uri)
        {
            // fuck it.
            if (!uri.IsAbsoluteUri) return uri; // might be invalid
            return new Uri(MakePathRelative(uri.AbsolutePath), UriKind.Relative);
        }
    }
}
