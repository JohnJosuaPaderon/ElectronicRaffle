using Sorschia.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRaffle
{
    public static class Configuration
    {
        static Configuration()
        {
            var configurationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SystemConfiguration", "ElectronicRaffle v.1");
            var configurationFile = Path.Combine(configurationDirectory, "Configuration.json");
            var connectionStringSourceFile = Path.Combine(configurationDirectory, "ConnectionString.txt");

            if (!Directory.Exists(configurationDirectory))
            {
                Directory.CreateDirectory(configurationDirectory);
            }

            if (!File.Exists(connectionStringSourceFile))
            {
                using (var streamWriter = File.CreateText(connectionStringSourceFile))
                {
                    var temporaryConnectionStringSourceFile = Path.Combine(Environment.CurrentDirectory, "ConnectionString.txt");
                    streamWriter.WriteLine(Crypto.Encrypt(File.ReadAllText(temporaryConnectionStringSourceFile)));
                }
            }

            ConfigurationDirectory = configurationDirectory;
            ConfigurationFile = configurationFile;
            ConnectionString = Crypto.Decrypt(File.ReadAllText(connectionStringSourceFile));
        }

        private static string ConfigurationFile { get; }
        private static string ConfigurationDirectory { get; }
        public static string ConnectionString { get; }
    }
}
