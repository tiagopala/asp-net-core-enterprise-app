using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EnterpriseApp.BFF.Compras.Services.gRPC.Interceptors
{
    public class GrpcServiceInterceptor : Interceptor
    {
        private readonly ILogger<GrpcServiceInterceptor> _logger;
        private readonly IHttpContextAccessor _httpContext;

        public GrpcServiceInterceptor(
            ILogger<GrpcServiceInterceptor> logger,
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContext = httpContext;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request, 
            ClientInterceptorContext<TRequest, TResponse> context, 
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var authorizationHeader = _httpContext.HttpContext.Request.Headers["Authorization"];

            var metaData = new Metadata
            {
                { "Authorization", authorizationHeader }
            };

            var options = context.Options.WithHeaders(metaData);

            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);

            return base.AsyncUnaryCall(request, context, continuation);
        }
    }
}
