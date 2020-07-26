using NUnit.Framework;
using System;
using Utilities.Castle;

namespace Utilities.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void ShouldFindFirstTierDependency()
        {
            var container = new DependencyContainer(new FirstLevelDependentInstaller());
            Assert.DoesNotThrow(() => container.Resolve<FirstLevelDependent>());
        }

        [Test]
        public void ShouldFindSecondTierDependency()
        {
            var container = new DependencyContainer(new SecondLevelDependentInstaller());
            Assert.DoesNotThrow(() => container.Resolve<SecondLevelDependent>());
        }

        [Test]
        public void BadDependecyShouldThrow()
        {
            Assert.Throws<Exception>(() =>
            {
                var container = new DependencyContainer(new BadDependecyInstaller());
            });
        }

        [Test]
        public void CircularDependeciesShouldTerminate()
        {
            Assert.DoesNotThrow(() =>
            {
                var container = new DependencyContainer(new DependsOnA());
            });

            Assert.DoesNotThrow(() =>
            {
                var container = new DependencyContainer(new DependsOnB());
            });
        }

        [Test]
        public void ShouldAllowRedundantDependencies()
        {
            Assert.DoesNotThrow(() =>
            {
                var container = new DependencyContainer(new SecondLevelDependentInstaller(),
                    new AlsoDependentOnBase()
                    );
            });
        }
    }

}
