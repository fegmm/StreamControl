using Prism.Commands;
using Prism.Mvvm;
using StreamControl.Core;
using StreamControl.Core.Services;
using StreamControl.Streams.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StreamControl.Streams.ViewModels
{
    public class StreamsViewModel : BindableBase
    {
        private readonly ICasparCGService casparCGService;
        private readonly IPlaceholderService placeholderService;

        public string StreamsHeader { get; }
        public ObservableCollection<Stream> Streams { get; set; }
        public DelegateCommand<Stream> ActivateCommand { get; set; }
        public DelegateCommand<Stream> DeactivateCommand { get; set; }
        

        public StreamsViewModel(Configuration conf, ICasparCGService casparCGService, IPlaceholderService placeholderService)
        {
            this.casparCGService = casparCGService;
            this.placeholderService = placeholderService;
            Streams = new ObservableCollection<Stream>(conf.Streams);
            StreamsHeader = conf.StreamsHeader;
            ActivateCommand = new DelegateCommand<Stream>(ActivateStream);
            DeactivateCommand = new DelegateCommand<Stream>(DeactivateStream);
        }

        private async void ActivateStream(Stream str)
        {
            if (!await casparCGService.SendCommandsAsync(placeholderService.ReplacePlaceholders(str.ActivateCommands, str)))
                str.IsActive = false;
        }

        private async void DeactivateStream(Stream str)
        {
            if (!await casparCGService.SendCommandsAsync(placeholderService.ReplacePlaceholders(str.DeactivateCommands)))
                str.IsActive = true;
        }
    }
}
