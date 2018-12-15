using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using StreamControl.Models;
using StreamControl.Properties;
using StreamControl.Views;
using System.Collections.Generic;
using Prism.Regions;
using System.Linq;
using System.Threading.Tasks;

namespace StreamControl.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public static InteractionRequest<DataDialogConfirmation<ICollection<Lowerthird>>> a;
        public static DataDialogConfirmation<ICollection<Lowerthird>> b;

        private string _title = Settings.Default.ProgramTitle;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public InteractionRequest<DataDialogConfirmation<ICollection<Lowerthird>>> StartUpDialogRequest { get; set; }

        public MainWindowViewModel(IRegionManager manager, IConfigurationService conf)
        {
            manager.RegisterViewWithRegion("StreamRegion", typeof(Streams));
            manager.RegisterViewWithRegion("LowerthirdRegion", typeof(Lowerthirds));
            conf.Lowerthirds = new List<string>();
            conf.Lowerthirds.Add("Test");
            a = StartUpDialogRequest = new InteractionRequest<DataDialogConfirmation<ICollection<Lowerthird>>>();
            b = new DataDialogConfirmation<ICollection<Lowerthird>>(conf.Lowerthirds.Select(i => new Lowerthird() { Title = i }).ToList()) { Title = "Startup" };
        }


    }
}
