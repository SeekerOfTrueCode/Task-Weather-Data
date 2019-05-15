using Microsoft.Extensions.Configuration;

namespace Technical_Task.Core.Logic
{
    public class JsonConfigValuesProvider
    {
        public static JsonConfigValuesProvider Config { get; } = new JsonConfigValuesProvider();
        private readonly IConfigurationRoot _config = new ConfigurationBuilder()
                                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                            .Build();

        public IConfigurationSection this[string value] => _config.GetSection(value);
    }
}
