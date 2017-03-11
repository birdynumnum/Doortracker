using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http.ValueProviders;

namespace WebAppDoorTracker.Filters
{
    public class HttpValueProvider : IValueProvider
    {
        private readonly HeadersMap _headers;

        public HttpValueProvider(HeadersMap map)
        {
            _headers = map;
        }

        PropertyInfo GetProp(string name)
        {
            var p = typeof(HttpRequestHeaders).GetProperty(name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            return p;
        }

        public bool ContainsPrefix(string prefix)
        {
            //return _headerKeys.Any(h => h.Replace("-", "").Equals(prefix, StringComparison.OrdinalIgnoreCase));
            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            string value = _headers[key];

            //var header = _headerKeys.FirstOrDefault(h => h.Replace("-", "").Equals(key, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(value))
            {
                return new ValueProviderResult(value, value, CultureInfo.CurrentCulture);
            }

            return null;
        }
    }
}