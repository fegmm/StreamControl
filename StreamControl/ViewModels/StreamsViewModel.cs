using Prism.Commands;
using Prism.Mvvm;
using StreamControl.Models;
using StreamControl.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StreamControl.ViewModels
{
    public class StreamsViewModel : BindableBase
    {
        private readonly ICasparCGService casparCGService;

        public string StreamsHeader { get; }
        public ObservableCollection<Stream> Streams { get; set; }
        public DelegateCommand<Stream> ActivateCommand { get; set; }
        public DelegateCommand<Stream> DeactivateCommand { get; set; }
        

        public StreamsViewModel(IConfigurationService confService, ICasparCGService casparCGService)
        {
            this.casparCGService = casparCGService;
            Streams = new ObservableCollection<Stream>(confService.Streams);
            StreamsHeader = confService.StreamsHeader;
            ActivateCommand = new DelegateCommand<Stream>(ActivateStream);
            DeactivateCommand = new DelegateCommand<Stream>(DeactivateStream);
        }

        private async void ActivateStream(Stream str)
        {
            if (!await casparCGService.SendCommandsAsync(str.ActivateCommands))
                str.IsActive = false;
        }

        private async void DeactivateStream(Stream str)
        {
            if (!await casparCGService.SendCommandsAsync(str.DeactivateCommands))
                str.IsActive = true;
        }
    }
}
