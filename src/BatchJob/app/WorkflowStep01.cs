using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BatchJob
{

    public class WorkflowStep01 : IWorkflowStep01
    {
        private readonly ILogger logger;

        public WorkflowStep01(ILogger<IWorkflowStep01> loggerService) {
            this.logger = loggerService;
        }

        /// Execute the Step Function with the Event Params.
        public async Task<WorkflowStepResponse> Execute(dynamic request) {

            // Request Details
            //this.logger.LogInformation("Request: " + request);

            // Return the selected AMI Image
            return new WorkflowStepResponse {
                success = true
            };
        }

    }
}
