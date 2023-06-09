using Microsoft.Extensions.Configuration;

using System.IO;

namespace LoETool.Infrastructure;

public static class ConfigurationFactory
{
    public static IConfiguration Create() => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(Constants.ConfigurationFilename, optional: false)
        .Build();
}
