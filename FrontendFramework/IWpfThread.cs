using System;
using System.Windows.Threading;
using Utilities.Threading;

namespace FrontendFramework
{
    public interface IWpfThread : IPostWorkToAnotherThread
    {
    }

    public class WpfDispatcher : IWpfThread
    {
        private readonly Dispatcher wpfDispatcher;

        public WpfDispatcher()
        {
            /*
             * accessing the dispatcher here assumes that the ctor is called on the UI thread at startup. The current 
             * startup model guarantees that by explicitly resolving this object in the main method. 
             */
            wpfDispatcher = Dispatcher.CurrentDispatcher;
        }

        public void Post(Action action, bool forcePost = false)
        {
            if (!forcePost && wpfDispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                wpfDispatcher.BeginInvoke(action);
            }
        }
    }
}
