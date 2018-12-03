using Prism.Mvvm;
using StreamControl.Properties;
using StreamControl.Views;

namespace StreamControl.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = Settings.Default.ProgramTitle;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        
        public MainWindowViewModel(Prism.Regions.IRegionManager manager)
        {
            manager.RegisterViewWithRegion("StreamRegion", typeof(Streams));
            manager.RegisterViewWithRegion("LowerthirdRegion", typeof(Lowerthirds));
        }
    }
}
