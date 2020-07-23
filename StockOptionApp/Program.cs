using FrontendFramework;
using System;

namespace StockOptionApp
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            FrontendFrameworkApp.Run<Installer>();
        }
    }
}
