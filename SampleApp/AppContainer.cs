using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace SampleApp
{
    class AppContainer : WindsorContainer
    {
        public AppContainer()
        {
            Install(new Installer());
        }
    }
}
