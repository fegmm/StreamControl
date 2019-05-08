using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using StreamControl.Core.Events;
using System;

namespace StreamControl.ViewModels
{
    public class StartupDialogViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator ea;

        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        public StartupDialogViewModel(IEventAggregator ea)
        {
            this.ea = ea;
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Ok()
        {
            ea.GetEvent<Core.Events.StartupDialogClosing>().Publish(true);
            FinishInteraction(); 
        }

        private void Cancel()
        {
            ea.GetEvent<Core.Events.StartupDialogClosing>().Publish(false);
            FinishInteraction();
        }
    }
}
