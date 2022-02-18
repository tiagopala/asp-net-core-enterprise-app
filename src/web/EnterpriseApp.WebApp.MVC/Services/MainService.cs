﻿using EnterpriseApp.Core.Communication;
using EnterpriseApp.Core.Responses;
using EnterpriseApp.WebApp.MVC.Exceptions;
using System.Net;
using System.Net.Http;
using System.Text;
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

        protected StringContent GetContent(object obj)
            => new(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

        protected async Task<T> DeserializeResponseMessage<T>(HttpResponseMessage responseMessage)
            => JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), _jsonSerializerOptions);

        protected async Task HandleErrorsResponse(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.StatusCode.Equals(HttpStatusCode.BadRequest))
                throw new CustomHttpRequestException(httpResponseMessage.StatusCode);

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var payload = JsonSerializer.Deserialize<ErrorApiResponse>(content, _jsonSerializerOptions);
            var messages = payload.Errors.Messages;
            throw new AuthException(messages);
        }

        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }
    }
}
