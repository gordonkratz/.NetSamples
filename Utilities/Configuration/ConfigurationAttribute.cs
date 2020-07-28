using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Configuration
{
    public class ConfigurationAttribute : Attribute
    {
        public ConfigurationAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
}
