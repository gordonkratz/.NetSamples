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
