using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using StreamControl.Properties;

namespace StreamControl.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title { get; }
        public DelegateCommand ShowStartupCommand { get; }
        public InteractionRequest<Confirmation> StartUpDialogRequest { get; }
        public MainWindowViewModel()
        {
            Title = Settings.Default.ProgramTitle;
            ShowStartupCommand = new DelegateCommand(ShowStartUpDialog);
            StartUpDialogRequest = new InteractionRequest<Confirmation>();
        }

        private void ShowStartUpDialog()
        {
            Confirmation confirmation = new Confirmation() { Content = new object(), Title = "" };
            StartUpDialogRequest.Raise(confirmation);
        }
    }
}
