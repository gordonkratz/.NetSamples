using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using System;
using System.Reflection;

namespace Utilities.Configuration
{
    internal class ConfigDependecyResolver : ISubDependencyResolver
    {
        private readonly IRawConfigProvider _provider;

        public ConfigDependecyResolver(IRawConfigProvider provider)
        {
            _provider = provider;
        }
        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return  typeof(ICustomConfiguration).IsAssignableFrom(dependency.TargetType);
            /*if (attributes.Any(a => a.GetType() == typeof(ConfigurationAttribute)))
                return true;
            return false;*/
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            var config = Activator.CreateInstance(dependency.TargetType) as ICustomConfiguration;
            dependency.TargetType.GetProperty(nameof(ICustomConfiguration.RawValue))
                .SetValue(config, _provider.Values[config.Key]);
            return config;
        }
    }
}