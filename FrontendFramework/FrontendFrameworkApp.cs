using Castle.MicroKernel.Registration;
using System.Windows;

namespace FrontendFramework
{
    public class FrontendFrameworkApp
    {

        public static void Run<T>() where T : IWindsorInstaller, new()
        {
            var container = new AppContainer(new T());

            var app = new Application();

            // done to guarantee that the wpf invoker has access to the UI thread
            var dispatcher = container.Resolve<IWpfThread>();

            app.Run(container.Resolve<MainWindow>());
        }
    }
}
