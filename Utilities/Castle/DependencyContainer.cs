using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities.Castle
{
    public class DependencyContainer : WindsorContainer
    {
        public DependencyContainer(params IWindsorInstaller[] installers)
        {
            var searched = new Dictionary<Type, IWindsorInstaller>();

            foreach (var installer in installers)
            {
                AddDependencies(installer.GetType(), installer, searched);
            }

            foreach (var installer in searched.Values)
            {
                Install(installer);
            }
        }

        private void AddDependencies(Type type, IWindsorInstaller instance, Dictionary<Type, IWindsorInstaller> currentInstallers)
        {
            if (currentInstallers.ContainsKey(type))
                return;
            currentInstallers[type] = instance;

            var dependencies = type.GetAttribute<InstallerDependsOnAttribute>()?.InstallerTypes;
            if (dependencies == null)
                return;

            foreach (var d in dependencies)
            {
                if (!typeof(IWindsorInstaller).IsAssignableFrom(d))
                    throw new Exception($"{d} does not implement IWindsorInstaller. It cannot be installed");

                AddDependencies(d, Activator.CreateInstance(d) as IWindsorInstaller, currentInstallers);
            }
        }
    }
}
