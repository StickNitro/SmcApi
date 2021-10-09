using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Smc.OrdersApi.Business.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> DeserializeBody<T>(this HttpRequest request, JsonSerializerSettings serializerSettings = null)
        {
            using var streamReader = new StreamReader(request.Body);
            var requestBody = await streamReader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(requestBody, serializerSettings);
        }
    }
}
