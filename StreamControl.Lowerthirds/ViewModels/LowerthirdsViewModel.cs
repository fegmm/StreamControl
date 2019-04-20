using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using StreamControl.Core;
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

        public LowerthirdsViewModel(Configuration conf, ICasparCGService casparCGService)
        {
            this.configuration = conf;
            this.casparCGService = casparCGService;

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
                currentlyActive.IsActive = false;
                worked = await casparCGService.SendCommandsAsync(TransformCommands(configuration.LowerthirdsChangeCommands, lowerthird));
            }
            else
                worked = await casparCGService.SendCommandsAsync(TransformCommands(configuration.LowerthirdsActivateCommands, lowerthird));

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
            if (await casparCGService.SendCommandsAsync(TransformCommands(configuration.LowerthirdsDeactivateCommands, currentlyActive)))
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

        private IEnumerable<string> TransformCommands(IEnumerable<string> commands, Lowerthird lowerthird)
        {
            return commands.Select(i => i.Replace("%TITLE%", lowerthird.Title)
                                            .Replace("%TEXT%", lowerthird.Text));
        }
    }
}
