using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TicTacToe
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<TicTacToeViewModel>(),
                Component.For(typeof(ICheckTicTacToeEnd<>)).ImplementedBy(typeof(NaiveChecker<>)),
                Component.For(typeof(IComputerLogic<>)).ImplementedBy(typeof(RandomLogic<>))
                );
        }
    }
}
