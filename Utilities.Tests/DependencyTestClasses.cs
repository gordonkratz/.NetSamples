
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using Utilities.Castle;

namespace Utilities.Tests
{
    public class Base { }

    public class BaseInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Base>());
        }
    }

    public class FirstLevelDependent
    {
        private readonly Base dependency;

        public FirstLevelDependent(Base dependency)
        {
            this.dependency = dependency;
        }
    }

    [InstallerDependsOn(typeof(BaseInstaller))]
    public class FirstLevelDependentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FirstLevelDependent>());
        }
    }

    public class SecondLevelDependent
    {
        private readonly FirstLevelDependent dependency;

        public SecondLevelDependent(FirstLevelDependent dependency)
        {
            this.dependency = dependency;
        }
    }

    [InstallerDependsOn(typeof(FirstLevelDependentInstaller))]
    public class SecondLevelDependentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<SecondLevelDependent>());
        }
    }


    [InstallerDependsOn(typeof(Base))]
    public class BadDependecyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
        }
    }

    [InstallerDependsOn(typeof(DependsOnB))]
    public class DependsOnA : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
        }
    }

    [InstallerDependsOn(typeof(DependsOnA))]
    public class DependsOnB : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
        }
    }

    [InstallerDependsOn(typeof(BaseInstaller))]
    public class AlsoDependentOnBase : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
        }
    }
}