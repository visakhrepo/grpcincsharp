using Calclulator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calclulator.calclulatorService;

namespace ConsoleApp1
{
    public class CalculatorServiceImp: calclulatorServiceBase
    {
        public override Task<global::Calclulator.calclulatorRespose> AddTwoNumber(calclulatorInput request, ServerCallContext context)
        {
            calclulatorRespose calclulatorRespose = new calclulatorRespose();
            calclulatorRespose.SumResult = request.Number1 + request.Number2;
            return Task.FromResult(calclulatorRespose);
        }
        public async override Task<calclulatorRespose> AveerageNumber(IAsyncStreamReader<calclulatorManyInput> requestStream, ServerCallContext context)
        {
            try
            {
                int total = 0;
                int count = 0;
                while (await requestStream.MoveNext())
                {
                    int currentNumber = requestStream.Current.Number;
                    total += currentNumber;
                    count++;
                }

                decimal average = (decimal)total / (decimal)count;
                return await Task.FromResult(new calclulatorRespose() { SumResult = (int)average });
            }
            catch(RpcException)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "number issue"));
            }
        }
    }
}
 