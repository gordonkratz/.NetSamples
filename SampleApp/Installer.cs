using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SampleApp
{
    class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            container.Install(
                new SudokuSolver.Installer(),
                new TicTacToe.Installer(),
                new BindingSamples.Installer()
                );
        }
    }
}
