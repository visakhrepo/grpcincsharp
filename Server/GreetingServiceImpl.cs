using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace ConsoleApp1
{
    public class GreetingServiceImpl: GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            string result = $"hello {request.Greeting.FirstName} {request.Greeting.LastName}";
            return Task.FromResult(new GreetingResponse() { Result = result });
        }

        public async override Task GreetMany(GreetingRequest request, IServerStreamWriter<GreetingResponseMany> responseStream, ServerCallContext context)
        {
            string result = request.Greeting.FirstName + " " + request.Greeting.LastName;


            foreach (int i in Enumerable.Range(0,1000))
            {
                await responseStream.WriteAsync(new GreetingResponseMany() { MessageResult = result });
            }
            
        }

        public async override Task<GreetingResponse> GreetRequestManyTimes(IAsyncStreamReader<GreetRequestMany> requestStream, ServerCallContext context)
        {
            string result = string.Empty;
            while(await requestStream.MoveNext())
            {
                result += $"{requestStream.Current.FirstName} {requestStream.Current.LastName}{Environment.NewLine}";
            }

            return new GreetingResponse() { Result= result };
        }
    }
}
