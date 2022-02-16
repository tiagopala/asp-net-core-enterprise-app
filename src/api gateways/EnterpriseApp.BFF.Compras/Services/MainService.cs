using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services
{
    public abstract class MainService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MainService()
        {
            _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        protected StringContent GetContent(object obj)
            => new(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

        protected async Task<T> DeserializeResponseMessage<T>(HttpResponseMessage responseMessage)
            => JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), _jsonSerializerOptions);

        protected bool HandleResponseErrors(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                return false;

            responseMessage.EnsureSuccessStatusCode();
            return true;
        }
    }
}
