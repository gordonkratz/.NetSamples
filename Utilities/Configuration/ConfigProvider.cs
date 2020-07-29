using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utilities.Configuration
{
    public interface IProvideConfiguration
    {
        string GetValue(string key);
    }

    internal class ConfigProvider : IProvideConfiguration
    {
        private Dictionary<string, string> _config = new Dictionary<string, string>();

        public ConfigProvider()
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


        public string GetValue(string key)
        {
            return _config[key];
        }

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
