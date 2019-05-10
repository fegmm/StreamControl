using StreamControl.Startup.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using StreamControl.Startup.Models;
using StreamControl.Core;
using StreamControl.Core.Services;
using System.Diagnostics;

namespace StreamControl.Startup
{
    public class StartupModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>()
                             .RegisterViewWithRegion(nameof(Core.Regions.Startup), typeof(Views.StartupDialog));

            var configuration = containerProvider.Resolve<StartupConfiguration>();

            var placeholderService = containerProvider.Resolve<IPlaceholderService>();
            placeholderService.AddAll(configuration.Defaults);

            var executeService = containerProvider.Resolve<IExecuteService>();
            foreach (var item in configuration.LaunchPrograms)
                executeService.RunProcess(item.Path, item.Arguments, item.WindowStyle);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(new StartupConfiguration().Load("startup.config.json"));
        }
    }
}