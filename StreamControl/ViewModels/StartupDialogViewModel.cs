using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;

namespace StreamControl.ViewModels
{
    public class StartupDialogViewModel : BindableBase, IInteractionRequestAware
    {
        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        public StartupDialogViewModel()
        {
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Ok()
        {
            FinishInteraction();
        }

        private void Cancel()
        {
            FinishInteraction();
        }
    }
}
