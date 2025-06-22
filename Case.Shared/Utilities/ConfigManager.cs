using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Case.Shared.Utilities
{
    public static class ConfigManager
    {
        public static IConfiguration GetConfiguration()
        {
            var settingFileName = "appsettings.json";

#if DEBUG
            settingFileName = "appsettings.Development.json";
#endif

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingFileName, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static T GetOptions<T>() where T : new()
        {
            var config = GetConfiguration();
            var sectionName = typeof(T).Name;

            var instance = new T();
            config.GetSection(sectionName).Bind(instance);
            return instance;
        }

        public static IConfigurationRoot CreateConfigurationFromResource(string resourceName)
        {
            var resourceFile = ReadResourceFile(resourceName);
            return new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(resourceFile)))
                .Build();
        }

        public static string ReadResourceFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
                throw new Exception($"{resourceName} not found. Available: {string.Join(", ", assembly.GetManifestResourceNames())}");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
