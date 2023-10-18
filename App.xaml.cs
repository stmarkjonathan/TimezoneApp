using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TimezoneApp
{

    public partial class App : Application
    {
        private Mutex _mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNewInstance = false;
            _mutex = new Mutex(true, "ExtendedClipboard", out isNewInstance);

            if (!isNewInstance)
            {
                App.Current.Shutdown();
            }

            base.OnStartup(e);
        }
    }
}
