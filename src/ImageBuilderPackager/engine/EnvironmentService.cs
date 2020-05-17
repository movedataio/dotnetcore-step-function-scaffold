public class EnvironmentService : IEnvironmentService
{
    public string EnvironmentName { get; set; }

    public EnvironmentService()
    {
        EnvironmentName = "Debug"; // Environment.GetEnvironmentVariable(EnvironmentVariables.AspnetCoreEnvironment) ?? Environments.Production;
    }
    
}