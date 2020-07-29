using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using System.Collections.Generic;
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
            _container.Register(Component.For<IProvideConfiguration>().ImplementedBy<ConfigProviderForTest>().IsDefault());
            _container.Register(Component.For<ReliesOnConfiguration>());
        }

        [Test]
        public void ShouldResolveConfiguration()
        {
            var component = _container.Resolve<ReliesOnConfiguration>();

            Assert.AreEqual("test", component.Value);
        }
    }

    internal class ConfigProviderForTest : IProvideConfiguration
    {
        public Dictionary<string, string> Values { get; } = new Dictionary<string, string>()
        {
            { "test.configuration.one", "test" } 
        };

        public string GetValue(string key)
        {
            return Values[key];
        }
    }

    internal class ReliesOnConfiguration
    {
        public ReliesOnConfiguration(IProvideConfiguration config)
        {
            Value = config.GetValue("test.configuration.one");
        }

        public string Value { get; }
    }
}
