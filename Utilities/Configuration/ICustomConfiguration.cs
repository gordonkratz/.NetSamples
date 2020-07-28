using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Configuration
{
    public interface ICustomConfiguration
    {
        string Key { get; }
        string RawValue { get; }
    }
}
