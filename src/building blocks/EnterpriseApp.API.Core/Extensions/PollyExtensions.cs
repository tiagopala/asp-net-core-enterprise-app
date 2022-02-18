using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace EnterpriseApp.API.Core.Extensions
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> GetHttpErrorWaitAndRetryCustomPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                });
        } 
    }
}
