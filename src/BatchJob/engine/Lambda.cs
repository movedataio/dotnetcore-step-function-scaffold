using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace BatchJob
{

    public class Lambda
    {
        public async Task<StepFunctionLaunchResponse> StepFunctionLauncherHandler(StepFunctionLaunchRequest request, ILambdaContext context) {
            var serviceProvider = new Startup().Initialise();

            var app = serviceProvider.GetService<IStepFunctionLauncher>();
            var response = await app.Execute(request);

            return response;
        }

        public async Task<WorkflowStepResponse> WorkflowStep01Handler(dynamic request, ILambdaContext context) {
            var serviceProvider = new Startup().Initialise();

            System.Console.WriteLine("Req: " + request);

            var app = serviceProvider.GetService<IWorkflowStep01>();
            var response = await app.Execute(request);

            return response;
        }

    }
}
