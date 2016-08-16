using System;
using System.Linq;

namespace Gite.WebSite.Extensions
{
    public static class StringExtensions
    {
        public static string Get(this string query, string parameterName)
        {
            var parameter = query.TrimStart('?', '&').Split('&').Select(x =>
            {
                var parts = x.Split('=');
                return new Tuple<string, string>(parts[0], parts[1]);
            }).SingleOrDefault(x => x.Item1 == parameterName);

            return parameter == null ? null : parameter.Item2;
        }
    }
}