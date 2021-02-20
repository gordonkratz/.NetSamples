using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FrontendFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public class Plugin : IPlugin<SudokuControl>
    {
        public string Header => "Sudoku";
    }

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterPlugin<Plugin, SudokuControl>();
            container.Register(Component.For<SudokuViewModel>());
        }
    }
}
