using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace n5.webApi.Extensions
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
}
