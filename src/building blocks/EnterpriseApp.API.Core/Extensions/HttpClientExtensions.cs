using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace EnterpriseApp.API.Core.Extensions
{
    public static class HttpClientExtensions
    {
        public static IHttpClientBuilder AddCustomCertificate(this IHttpClientBuilder httpClientBuilder)
        {
            return httpClientBuilder.ConfigureHttpMessageHandlerBuilder(builder =>
            {
                 builder.PrimaryHandler = new HttpClientHandler
                 {
                     ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                 };
            });
        }
    }
}
