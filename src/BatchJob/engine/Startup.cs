using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace BatchJob
{

    public class Startup
    {
        public ServiceProvider Initialise() {

            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();

            this.ConfigureServices(serviceCollection);

            this.ConfigureAWS(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add Console Logger
            serviceCollection.AddLogging(cfg => cfg.AddConsole());

            // Register services with DI system
            serviceCollection.AddTransient<IEnvironmentService, EnvironmentService>();
            serviceCollection.AddTransient<IConfigurationService, ConfigurationService>();
            serviceCollection.AddTransient<IStepFunctionLauncher, StepFunctionLauncher>();
            serviceCollection.AddTransient<IWorkflowStep01, WorkflowStep01>();
        }

        private void ConfigureAWS(IServiceCollection serviceCollection)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get Configuration Service from DI system
            var ConfigService = serviceProvider.GetService<IConfigurationService>();
            IConfiguration Configuration = ConfigService.GetConfiguration();

            var options = Configuration.GetAWSOptions();
            serviceCollection.AddDefaultAWSOptions(options);
            serviceCollection.AddAWSService<Amazon.StepFunctions.IAmazonStepFunctions>();
        }

    }
}
