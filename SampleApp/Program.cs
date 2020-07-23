using System;

namespace SampleApp
{
    public class Program
    {

        [STAThread]
        public static void Main()
        {
            var container = new AppContainer();
            var app = container.Resolve<App>();
            app.Run(container.Resolve<MainWindow>());
        }
    }
}
