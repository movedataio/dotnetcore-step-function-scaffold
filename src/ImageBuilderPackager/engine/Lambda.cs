using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ImageBuilderPackager
{

    public class Lambda
    {
        public async Task<ImageBuilderPackagerResponse> SearchAmiHandler(ImageBuilderPackagerRequest request, ILambdaContext context) {
            var serviceProvider = new Startup().Initialise();

            var app = serviceProvider.GetService<ISearchAmiService>();
            await app.Execute(request);

            return new ImageBuilderPackagerResponse {
                success = true
            };
        }

    }
}
