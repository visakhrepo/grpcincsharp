using Calclulator;
using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {

        const string Target = "127.0.0.1:50051";
        static async Task Main(string[] args)
        {
            Console.WriteLine(5 / 10);
            Channel channel = new Channel(Target, ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith(t =>
            {
                if(t.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("Success");
                }
            });

            //var client = new DummyService.DummyServiceClient(channel);
            var client = new GreetingService.GreetingServiceClient(channel);
            var greet = new Greeting();
            greet.FirstName = "Visakh";
            greet.LastName = "V A";

            //var greetingReq = new GreetingRequest();
            //greetingReq.Greeting = greet;

            //var res = client.Greet(greetingReq);
            //Console.WriteLine(res.Result);



            //var client2 = new calclulatorService.calclulatorServiceClient(channel);
            //var input = new calclulatorInput();
            //input.Number1 = 1;
            //input.Number2 = 2;


            //greetingReq.Greeting = greet;

            //var sumNumber = client2.AddTwoNumber(input);
            //Console.WriteLine(sumNumber.SumResult);

            //var resp = client.GreetMany(greetingReq);

            //while (await resp.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(resp.ResponseStream.Current.MessageResult);
            //    await Task.Delay(200);
            //}

            //var request = new GreetRequestMany();
            //request.FirstName = "Visakh";
            //request.LastName = "V A";

            //var stram = client.GreetRequestManyTimes();

            //foreach (var item in Enumerable.Range(0,100))
            //{
            //    await stram.RequestStream.WriteAsync(request);
            //}

            //await stram.RequestStream.CompleteAsync();

            //var resp = await stram.ResponseAsync;
            //Console.WriteLine(resp.Result);

            //var client2 = new calclulatorService.calclulatorServiceClient(channel);
            //var stream = client2.AveerageNumber();

            //foreach (var item in Enumerable.Range(0,100))
            //{
            //    await stream.RequestStream.WriteAsync(new calclulatorManyInput() { Number= item});
            //}
            //await stream.RequestStream.CompleteAsync();
            //var average = stream.ResponseAsync.Result;
            //Console.WriteLine($"Average is {average}");



            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
