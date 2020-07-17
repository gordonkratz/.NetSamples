using SampleApp.BindingSamples;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp
{
    public class SampleAppDataContext
    {
        public SampleAppDataContext()
        {
            BindingSampleViewModel = new BindingSamplesViewModel();
        }

        public BindingSamplesViewModel BindingSampleViewModel { get; }
    }
}
