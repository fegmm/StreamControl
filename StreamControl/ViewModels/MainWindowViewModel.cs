using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using StreamControl.Models;
using StreamControl.Properties;
using StreamControl.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamControl.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IConfigurationService conf;

        public string Title { get; }
        public DelegateCommand ShowStartupCommand { get; }
        public InteractionRequest<Confirmation> StartUpDialogRequest { get; }

        private bool showOverlay;
        public bool ShowOverlay
        {
            get { return showOverlay; }
            set { SetProperty(ref showOverlay, value); }
        }


        public MainWindowViewModel(IRegionManager manager, IConfigurationService conf)
        {
            this.conf = conf;

            manager.RegisterViewWithRegion("StreamRegion", typeof(Streams));
            manager.RegisterViewWithRegion("LowerthirdRegion", typeof(Lowerthirds));

            Title = Settings.Default.ProgramTitle;
            ShowStartupCommand = new DelegateCommand(ShowStartUpDialog);
            StartUpDialogRequest = new InteractionRequest<Confirmation>();
        }

        private void ShowStartUpDialog()
        {
            Confirmation confirmation = new Confirmation() { Content = conf.Lowerthirds, Title = "" };
            StartUpDialogRequest.Raise(confirmation);
            if (!confirmation.Confirmed)
                conf.Lowerthirds.Clear();
        }
    }
}
