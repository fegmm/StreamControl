using Prism.Ioc;
using Prism.Modularity;
using StreamControl.Core.Models;

namespace StreamControl.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ICasparCGService>(new CasparCGService());
            containerRegistry.RegisterInstance(new Configuration().Load("core.config.json"));
        }
    }
}