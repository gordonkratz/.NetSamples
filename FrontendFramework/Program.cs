using Castle.MicroKernel.Registration;
using System.Windows;

namespace FrontendFramework
{
    public class FrontendFrameworkApp
    {

        public static void Run<T>() where T : IWindsorInstaller, new()
        {
            var container = new AppContainer();
            container.Install(new T());

            var app = new Application();
            app.Run(container.Resolve<MainWindow>());
        }
    }
}
