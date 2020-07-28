using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utilities.Configuration
{
    internal interface IRawConfigProvider
    {
        public IReadOnlyDictionary<string, string> Values { get; }
    }

    internal class RawConfigProvider : IRawConfigProvider
    {
        private Dictionary<string, string> _config = new Dictionary<string, string>();

        public RawConfigProvider()
        {
            var filePath = "config.txt";

            /*var sampleConfig = new Config
            {
                Values = new[] { new ConfigValue { Key = "test", Value = "testValue" } ,
                                new ConfigValue { Key = "test1", Value = "testValue1" } }
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(sampleConfig));*/

            if (!File.Exists(filePath))
                return;

            using (var file = new StreamReader(filePath))
            {
                var configuration = JsonConvert.DeserializeObject<Config>(file.ReadToEnd());
                _config = configuration.Values.ToDictionary(v => v.Key, v => v.Value);
            }
        }

        public IReadOnlyDictionary<string, string> Values => _config;

        private class Config
        {
            public ConfigValue[] Values { get; set; }
        }

        private class ConfigValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }

}
