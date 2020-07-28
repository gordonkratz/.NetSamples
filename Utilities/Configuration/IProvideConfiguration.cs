using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Configuration
{
    public interface IProvideConfiguration
    {
        T GetConfig<T>();
    }

    internal class ConfigProvider : IProvideConfiguration
    {
        IRawConfigProvider _rawConfig;

        public ConfigProvider(IRawConfigProvider rawConfig)
        {
            _rawConfig = rawConfig;
        }

        public T GetConfig<T>()
        {
            throw new NotImplementedException();
        }
    }
}
