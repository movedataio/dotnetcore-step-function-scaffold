using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BatchJob
{

    public class StepFunctionLauncher : IStepFunctionLauncher
    {
        private readonly ILogger logger;
        private readonly Amazon.StepFunctions.IAmazonStepFunctions stepFunctionClient;

        public StepFunctionLauncher(ILogger<IStepFunctionLauncher> loggerService, Amazon.StepFunctions.IAmazonStepFunctions stepFunctionService) {
            this.logger = loggerService;
            this.stepFunctionClient = stepFunctionService;
        }

        /// Execute the Step Function with the Event Params.
        public async Task<StepFunctionLaunchResponse> Execute(StepFunctionLaunchRequest request)
        {
            // Request Details
            this.logger.LogInformation("Request.target: " + request.target);

            var startExecutionRequest = new Amazon.StepFunctions.Model.StartExecutionRequest {
                StateMachineArn = request.target,
                Name = Guid.NewGuid().ToString(),
                Input = "{}",                
            };

            var taskStartExecutionResponse = await this.stepFunctionClient.StartExecutionAsync(startExecutionRequest);

            this.logger.LogInformation("taskStartExecutionResponse: " + taskStartExecutionResponse);
            // Assert.Equal(HttpStatusCode.OK, taskStartExecutionResponse.HttpStatusCode);

            // Return the selected AMI Image
            return new StepFunctionLaunchResponse {
                success = (HttpStatusCode.OK == taskStartExecutionResponse.HttpStatusCode)
            };
        }

    }
}
