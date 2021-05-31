using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeterClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreeterController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromHeader][Required] string myCustomHeader,
            [FromBody] InputHelloRequest model)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress("http://localhost:5005");
            
            var client = new Greeter.GreeterClient(channel);

            Metadata headers = new()
            {
                { "myCustomHeader", myCustomHeader }
            };

            var reply = await client.SayHelloAsync(new HelloRequest { Name = model.Name }, headers);

            return Ok(reply.Message);
        }
    }
}
