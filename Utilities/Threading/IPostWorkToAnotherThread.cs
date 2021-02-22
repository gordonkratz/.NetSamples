using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Utilities.Threading
{
    public interface IPostWorkToAnotherThread
    {
        void Post(Action action, bool forcePost = false);
    }

    public static class ThreadPostExtensions
    {

        public static Action Wrap(this IPostWorkToAnotherThread thread, Action action)
        {
            return () => thread.Post(action);
        }

        public static Action<T> Wrap<T>(this IPostWorkToAnotherThread thread, Action<T> action)
        {
            return item => thread.Post(() => action(item));
        }
    }
}
