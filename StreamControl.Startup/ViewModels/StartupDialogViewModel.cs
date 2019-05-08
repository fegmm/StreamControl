using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using StreamControl.Core.Services;
using StreamControl.Startup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamControl.Startup.ViewModels
{
    public class StartupDialogViewModel : BindableBase
    {
        private readonly StartupConfiguration startupConfiguration;
        private readonly IPlaceholderService placeholderService;
        private readonly ICasparCGService casparService;

        public Configuration[] Configurations { get; set; }
        public Configuration SelectedConfiguration { get; set; }

        public StartupDialogViewModel(StartupConfiguration configuration, IPlaceholderService placeholderService, ICasparCGService casparService, IEventAggregator ea)
        {
            this.startupConfiguration = configuration;
            this.placeholderService = placeholderService;
            this.casparService = casparService;

            Configurations = configuration.Configurations;
            SelectedConfiguration = Configurations?.FirstOrDefault();

            ea.GetEvent<Core.Events.StartupDialogClosing>().Subscribe(Closing);
        }

        private async void Closing(bool isConfirmed)
        {
            if (SelectedConfiguration != null)
                placeholderService.AddAll(SelectedConfiguration.Values);
            await casparService.SendCommandsAsync(placeholderService.ReplacePlaceholders(startupConfiguration.InitCommands));
        }
    }
}
