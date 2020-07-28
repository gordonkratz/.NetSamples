using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Castle;
using Utilities.Configuration;

namespace Utilities.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        IWindsorContainer _container;

        [SetUp]
        public void SetUp()
        {
            _container = new DependencyContainer(new ConfigurationInstaller());
            _container.Register(Component.For<IRawConfigProvider>().ImplementedBy<ConfigProviderForTest>().IsDefault());
            var resolver = _container.Resolve<ConfigDependecyResolver>();
            _container.Kernel.Resolver.AddSubResolver(resolver);
            _container.Register(Component.For<ReliesOnConfiguration>());
        }

        [Test]
        public void ShouldResolveConfiguration()
        {
            var component = _container.Resolve<ReliesOnConfiguration>();

            Assert.AreEqual("test", component.Config.RawValue);
        }
    }

    internal class ConfigProviderForTest : IRawConfigProvider
    {
        public IReadOnlyDictionary<string, string> Values { get; } = new Dictionary<string, string>()
        {
            { "test.configuration.one", "test" } 
        };
    }

    internal class ReliesOnConfiguration
    {
        public ReliesOnConfiguration(SampleConfiguration config)
        {
            Config = config;
        }

        public SampleConfiguration Config { get; }
    }

    [Configuration("blah")]
    internal class SampleConfiguration : ICustomConfiguration
    {
        [Configuration("test.configuration.one")]
        public string RawValue { get; private set; }

        public string Key => "test.configuration.one";
    }
}
