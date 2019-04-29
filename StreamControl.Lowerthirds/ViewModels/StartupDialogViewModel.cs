using Prism.Commands;
using Prism.Mvvm;
using StreamControl.Core.Services;
using StreamControl.Lowerthirds.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamControl.Lowerthirds.ViewModels
{
    public class StartupDialogViewModel : BindableBase
    {
        private readonly IPlaceholderService placeholder;

        public ICollection<Lowerthird> Lowerthirds { get; set; }
        public DelegateCommand ClosingCommand { get; set; }

        public StartupDialogViewModel(Configuration conf, IPlaceholderService placeholder)
        {
            Lowerthirds = conf.Lowerthirds;
            ClosingCommand = new DelegateCommand(Closing);
            this.placeholder = placeholder;
        }

        private void Closing()
        {
            foreach (var item in Lowerthirds)
            {
                placeholder.Placeholders[item.Title] = item.Text;
            }
        }
    }
}
