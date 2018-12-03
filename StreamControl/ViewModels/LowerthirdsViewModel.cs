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
    public class LowerthirdsViewModel : BindableBase
    {
        public string LowerthirdsHeader => Settings.Default.LowerthirdsHeader;

        private ObservableCollection<Lowerthird> lowerthirds;
        public ObservableCollection<Lowerthird> Lowerthirds
        {
            get { return lowerthirds; }
            set { SetProperty(ref lowerthirds, value); }
        }

        public LowerthirdsViewModel()
        {
            lowerthirds = new ObservableCollection<Lowerthird>();
        }
    }
}
