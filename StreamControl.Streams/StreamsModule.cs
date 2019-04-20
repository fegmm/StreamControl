using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace StreamControl.Streams
{
    public class StreamsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>()
                .RegisterViewWithRegion(nameof(Core.Regions.MainContent), typeof(Views.Streams));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}