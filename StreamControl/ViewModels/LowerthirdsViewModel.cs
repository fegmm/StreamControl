using Prism.Commands;
using Prism.Mvvm;
using Prism.Interactivity.InteractionRequest;
using StreamControl.Models;
using System.Collections.ObjectModel;
using System;

namespace StreamControl.ViewModels
{
    public class LowerthirdsViewModel : BindableBase
    {
        private readonly IConfigurationService confService;
        private readonly ICasparCGService casparCGService;
        private Lowerthird currentlyActive;

        public ObservableCollection<Lowerthird> Lowerthirds { get; }
        public string LowerthirdsHeader { get; }
        public string LowerthirdDeactivateText { get; }

        public DelegateCommand<Lowerthird> ActivateCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeactivateCommand { get; }
        public DelegateCommand<Lowerthird> DeleteCommand { get; }
        public DelegateCommand<Lowerthird> EditCommand { get; }

        public InteractionRequest<Confirmation> EditDialogRequest { get; }


        public LowerthirdsViewModel(IConfigurationService confService, ICasparCGService casparCGService)
        {
            this.confService = confService;
            this.casparCGService = casparCGService;

            Lowerthirds = new ObservableCollection<Lowerthird>(confService.Lowerthirds);
            LowerthirdsHeader = confService.LowerthirdsHeader;
            LowerthirdDeactivateText = confService.LowerthirdDeactivateText;

            ActivateCommand = new DelegateCommand<Lowerthird>(Activate, i => !i.IsActive);
            AddCommand = new DelegateCommand(Add);
            DeactivateCommand = new DelegateCommand(Deactivate, () => currentlyActive != null);
            DeleteCommand = new DelegateCommand<Lowerthird>(Delete);
            EditCommand = new DelegateCommand<Lowerthird>(Edit);

            EditDialogRequest = new InteractionRequest<Confirmation>();
        }

        public async void Activate(Lowerthird lowerthird)
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

        private void Add()
        {
            Lowerthird lowerthird = new Lowerthird();
            Confirmation confirmation = new Confirmation() { Content = lowerthird, Title = "" };
            EditDialogRequest.Raise(confirmation);
            if (confirmation.Confirmed)
                Lowerthirds.Add(lowerthird);
        }

        public async void Deactivate()
        {
            if (await casparCGService.SendCommandsAsync(confService.LowerthirdsDeactivateCommands))
            {
                currentlyActive.IsActive = false;
                currentlyActive = null;
                DeactivateCommand.RaiseCanExecuteChanged();
                ActivateCommand.RaiseCanExecuteChanged();
            }
        }

        private void Delete(Lowerthird lowerthird)
        {
            Lowerthirds.Remove(lowerthird);
            if (lowerthird.IsActive)
                Deactivate();
        }

        private void Edit(Lowerthird lowerthird)
        {
            Confirmation confirmation = new Confirmation() { Content = lowerthird, Title = "" };
            EditDialogRequest.Raise(confirmation);
            if (lowerthird.IsActive)
                Activate(lowerthird);
        }
    }
}
