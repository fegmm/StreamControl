using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using StreamControl.Core.Services;
using StreamControl.Lowerthirds.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StreamControl.Lowerthirds.ViewModels
{
    public class LowerthirdsViewModel : BindableBase
    {
        private readonly Configuration configuration;
        private readonly ICasparCGService casparCGService;
        private readonly IPlaceholderService placeholder;
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

        public LowerthirdsViewModel(Configuration conf, ICasparCGService casparCGService, IPlaceholderService placeholder)
        {
            this.configuration = conf;
            this.casparCGService = casparCGService;
            this.placeholder = placeholder;

            Lowerthirds = new ObservableCollection<Lowerthird>(conf.Lowerthirds);
            LowerthirdsHeader = conf.LowerthirdsHeader;
            LowerthirdDeactivateText = conf.LowerthirdDeactivateText;

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
                if (worked = await casparCGService.SendCommandsAsync(placeholder.ReplacePlaceholders(configuration.LowerthirdsChangeCommands, lowerthird)))
                    currentlyActive.IsActive = false;
            }
            else
                worked = await casparCGService.SendCommandsAsync(placeholder.ReplacePlaceholders(configuration.LowerthirdsActivateCommands, lowerthird));

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
            {
                Lowerthirds.Add(lowerthird);
                placeholder.Placeholders[lowerthird.Title] = lowerthird.Text;
            }
        }

        public async void Deactivate()
        {
            if (await casparCGService.SendCommandsAsync(placeholder.ReplacePlaceholders(configuration.LowerthirdsDeactivateCommands, currentlyActive)))
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
            placeholder.Placeholders[lowerthird.Title] = lowerthird.Text;
            if (lowerthird.IsActive)
                Activate(lowerthird);

        }
    }
}
