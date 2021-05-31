using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GreeterService(ILogger<GreeterService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            string header = "" + _httpContextAccessor.HttpContext.Request.Headers["myCustomHeader"];

            return Task.FromResult(new HelloReply
            {
                Message = $"Hello {request.Name} with custom header {header}"
            });
        }
    }
}
