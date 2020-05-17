using Microsoft.Extensions.Configuration;

public interface IConfigurationService
{
    IConfiguration GetConfiguration();
}