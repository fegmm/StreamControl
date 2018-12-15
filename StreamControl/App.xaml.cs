using StreamControl.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Regions;
using System.IO;
using System.Diagnostics;
using System.Reflection;

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
            containerRegistry.RegisterInstance<IConfigurationService>(ConfigurationService.Load(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\config.json"));
            containerRegistry.RegisterInstance<ICasparCGService>(new CasparCGService());
        }
    }
}
