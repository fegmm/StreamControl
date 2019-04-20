﻿using StreamControl.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Regions;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using StreamControl.Core;
using StreamControl.Lowerthirds;
using StreamControl.Streams;

namespace StreamControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(CoreModule));
            moduleCatalog.AddModule(typeof(LowerthirdsModule));
            moduleCatalog.AddModule(typeof(StreamsModule));

            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
