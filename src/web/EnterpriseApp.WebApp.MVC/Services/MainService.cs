using EnterpriseApp.Core.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public abstract class MainService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MainService() 
        {
            _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        protected async Task<IEnumerable<string>> GetErrorsResponse(HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var payload = JsonSerializer.Deserialize<ErrorApiResponse>(content, _jsonSerializerOptions);
            var messages = payload.Errors.Messages;
            return messages;
        }
    }
}
