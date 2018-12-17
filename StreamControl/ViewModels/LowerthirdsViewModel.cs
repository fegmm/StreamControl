using Prism.Commands;
using Prism.Mvvm;
using StreamControl.Models;
using System.Collections.ObjectModel;

namespace StreamControl.ViewModels
{
    public class LowerthirdsViewModel : BindableBase
    {
        private readonly IConfigurationService confService;
        private readonly ICasparCGService casparCGService;
        private Lowerthird currentlyActive;

        public string LowerthirdsHeader { get; }
        public string LowerthirdDeactivateText { get; }
        public ObservableCollection<Lowerthird> Lowerthirds { get; set; }
        public DelegateCommand<Lowerthird> EditCommand { get; set; }
        public DelegateCommand<Lowerthird> ActivateCommand { get; set; }
        public DelegateCommand DeactivateCommand { get; set; }


        public LowerthirdsViewModel(IConfigurationService confService, ICasparCGService casparCGService)
        {
            this.confService = confService;
            this.casparCGService = casparCGService;
            Lowerthirds = new ObservableCollection<Lowerthird>(confService.Lowerthirds);
            LowerthirdsHeader = confService.LowerthirdsHeader;
            LowerthirdDeactivateText = confService.LowerthirdDeactivateText;
            ActivateCommand = new DelegateCommand<Lowerthird>(ActivateLowerthird, i => !i.IsActive);
            DeactivateCommand = new DelegateCommand(DeactivateLowerthird, () => currentlyActive != null);
        }

        public async void ActivateLowerthird(Lowerthird lowerthird)
        {
            bool worked;
            if (currentlyActive != null)
            {
                currentlyActive.IsActive = false;
                worked = await casparCGService.SendCommandsAsync(confService.LowerthirdsChangeCommands);
            }
            else
                worked = await casparCGService.SendCommandsAsync(confService.LowerthirdsActivateCommands);

            if (worked)
            {
                currentlyActive = lowerthird;
                lowerthird.IsActive = true;
                DeactivateCommand.RaiseCanExecuteChanged();
                ActivateCommand.RaiseCanExecuteChanged();
            }
        }

        public async void DeactivateLowerthird()
        {
            if(await casparCGService.SendCommandsAsync(confService.LowerthirdsDeactivateCommands))
            {
                currentlyActive.IsActive = false;
                currentlyActive = null;
                DeactivateCommand.RaiseCanExecuteChanged();
                ActivateCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
