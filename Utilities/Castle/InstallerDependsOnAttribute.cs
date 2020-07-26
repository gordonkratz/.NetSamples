using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Castle
{
    public class InstallerDependsOnAttribute : Attribute
    {
        public InstallerDependsOnAttribute(params Type[] installerTypes)
        {
            InstallerTypes = installerTypes;
        }

        public Type[] InstallerTypes { get; }
    }
}
