using StreamControl.Lowerthirds.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using StreamControl.Lowerthirds.Models;
using StreamControl.Core;

namespace StreamControl.Lowerthirds
{
    public class LowerthirdsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>()
                           .RegisterViewWithRegion(nameof(Core.Regions.MainContent), typeof(Views.Lowerthirds))
                           .RegisterViewWithRegion(nameof(Core.Regions.Startup), typeof(Views.StartupDialog));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(new Configuration().Load("lowerthirds.config.json"));
        }
    }
}