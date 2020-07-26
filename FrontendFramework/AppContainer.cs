using Castle.MicroKernel.Registration;
using System.Linq;
using Utilities.Castle;

namespace FrontendFramework
{
    class AppContainer : DependencyContainer
    {
        public AppContainer(params IWindsorInstaller[] types) 
            : base(types.Append(new FrontendFrameworkInstaller()).ToArray())
        {
        }
    }
}
