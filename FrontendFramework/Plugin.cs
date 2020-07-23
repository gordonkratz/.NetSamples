using System;
using System.Windows.Controls;

namespace FrontendFramework
{
    public interface IPlugin { }

    public interface IPlugin<T> : IPlugin where T : Control
    {
        string Header { get; }
    }
}
