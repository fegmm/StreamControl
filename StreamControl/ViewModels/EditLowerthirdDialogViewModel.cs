using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using StreamControl.Models;
using System;

namespace StreamControl.ViewModels
{
    public class EditLowerthirdDialogViewModel : BindableBase, IInteractionRequestAware
    {
        private Lowerthird oldValues;

        public IConfigurationService Conf { get; }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public Lowerthird Lowerthird { get { return notification?.Content as Lowerthird; } }

        private Confirmation notification;
        public INotification Notification
        {
            get { return notification; }
            set
            {
                if (value is Confirmation c && c.Content is Lowerthird l)
                {
                    SetProperty(ref notification, c);
                    oldValues = new Lowerthird() { Title = l.Title, Text = l.Text };
                    RaisePropertyChanged(nameof(Lowerthird));
                }
            }
        }
        public Action FinishInteraction { get; set; }

        public EditLowerthirdDialogViewModel(IConfigurationService conf)
        {
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
            Conf = conf;
        }

        private void Cancel()
        {
            if (notification != null)
            {
                Lowerthird.Title = oldValues.Title;
                Lowerthird.Text = oldValues.Text;
                notification.Confirmed = false;
            }

            this.FinishInteraction();
        }

        private void Ok()
        {
            if (notification != null)
                notification.Confirmed = true;

            this.FinishInteraction();
        }
    }
}
