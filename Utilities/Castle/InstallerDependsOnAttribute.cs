using System;

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
