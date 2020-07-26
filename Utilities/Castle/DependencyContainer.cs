using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;

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

        private void AddDependencies(Type type, IWindsorInstaller installer, Dictionary<Type, IWindsorInstaller> set)
        {
            if (set.ContainsKey(type))
                return;
            set[type] = installer;

            var dependencies = type.GetAttribute<InstallerDependsOnAttribute>()?.InstallerTypes;
            if (dependencies == null)
                return;

            foreach (var d in dependencies)
            {
                AddDependencies(d, Activator.CreateInstance(d) as IWindsorInstaller, set);
            }
        }
    }
}
