using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace FrontendFramework
{
    class AppContainer : WindsorContainer
    {
        public AppContainer()
        {
            Install(new FrontendFrameworkInstaller());
        }
    }
}
