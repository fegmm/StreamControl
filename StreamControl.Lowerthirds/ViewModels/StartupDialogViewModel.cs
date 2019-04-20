using Prism.Commands;
using Prism.Mvvm;
using StreamControl.Lowerthirds.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamControl.Lowerthirds.ViewModels
{
    public class StartupDialogViewModel : BindableBase
    {
        public ICollection<Lowerthird> Lowerthirds { get; set; }

        public StartupDialogViewModel(Prism.Events.IEventAggregator events, Configuration conf)
        {
        }
    }
}
