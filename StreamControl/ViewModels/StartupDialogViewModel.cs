using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using StreamControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamControl.ViewModels
{
    public class StartupDialogViewModel : BindableBase, IInteractionRequestAware
    {
        public IConfigurationService Conf { get; }
        public ICollection<Lowerthird> Lowerthirds { get { return (ICollection<Lowerthird>)notification?.Content; } }
        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }


        private Confirmation notification;
        public INotification Notification
        {
            get { return notification; }
            set
            {
                if (value is Confirmation c && c.Content is ICollection<Lowerthird>)
                {
                    SetProperty(ref notification, c);
                    RaisePropertyChanged(nameof(Lowerthirds));
                }
            }
        }
        public Action FinishInteraction { get; set; }

        public StartupDialogViewModel(IConfigurationService conf)
        {
            Conf = conf;
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Ok()
        {
            if (notification != null)
                notification.Confirmed = true;
            FinishInteraction();
        }

        private void Cancel()
        {
            if (notification != null)
                notification.Confirmed = false;
            FinishInteraction();
        }
    }
}
