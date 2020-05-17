using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ImageBuilderPackager
{

    public class SearchAmiService : ISearchAmiService
    {
        private const string AWS_MARKETPLACE_ID = "679593333241";
        private readonly ILogger logger;
        private readonly Amazon.EC2.IAmazonEC2 ec2Client;

        public SearchAmiService(ILogger<ISearchAmiService> loggerService, Amazon.EC2.IAmazonEC2 ec2ClientService) {
            this.logger = loggerService;
            this.ec2Client = ec2ClientService;
        }

        public async Task Execute(ImageBuilderPackagerRequest request)
        {
            // Request Details
            this.logger.LogInformation(string.Format("Benchmark Name: {0}", request.benchmarkName));
            this.logger.LogInformation(string.Format("Profile Level: {0}", request.profileLevel));

            // Get the AMI Image Details from EC2 AWS Marketplace
            var image = await GetBaseAmiDetails(request);
        }

        // --------------------------------------------------------------------------------------------------------------------------------

        private async Task<Amazon.EC2.Model.Image> GetBaseAmiDetails(ImageBuilderPackagerRequest request) {
            Amazon.EC2.Model.Image response = null;
            
            // Construct the AMI name pattern
            var amiNamePattern = request.benchmarkName + "*" + request.benchmarkName + "*";

            // Create a Describe Images Request
            var describeRequest = new Amazon.EC2.Model.DescribeImagesRequest{
                Owners = new List<string>{ AWS_MARKETPLACE_ID },
                Filters = new List<Amazon.EC2.Model.Filter>{
                    new Amazon.EC2.Model.Filter{
                        Name = "name",
                        Values = new List<string>{ amiNamePattern }
                    }
                }
            };

            // Execute the request.
            this.logger.LogInformation("EC2 Describe-Images: Request - Begin");
            var describeResponse = await this.ec2Client.DescribeImagesAsync(describeRequest);
            this.logger.LogInformation("EC2 Describe-Images: Request - End");

            // Process the response.
            this.logger.LogInformation("EC2 Describe-Images: Response - Begin");
            if (describeResponse != null) {
                // Log all responses.
                describeResponse.Images.ForEach(item => this.logger.LogDebug(this.GetAmiImageDetailsAsString(item)));

                // Find the most recent AMI and set a result.
                response = describeResponse.Images.OrderByDescending(item => item.CreationDate).FirstOrDefault();
                this.logger.LogInformation(string.Format("SELECTED: {0}", this.GetAmiImageDetailsAsString(response)));

            }
            this.logger.LogInformation("EC2 Describe-Images: Response - End");

            // Return the selected AMI Image
            return response;
        }

        private string GetAmiImageDetailsAsString(Amazon.EC2.Model.Image item) {
            return string.Format("{0} | {1} - {2} - {3} ({4})", item.Name, item.Description, item.ImageId, item.PlatformDetails, item.CreationDate);
        }

    }
}
