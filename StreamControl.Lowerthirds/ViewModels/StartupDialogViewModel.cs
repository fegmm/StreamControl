using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using StreamControl.Core.Services;
using StreamControl.Lowerthirds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamControl.Lowerthirds.ViewModels
{
    public class StartupDialogViewModel : BindableBase
    {
        private readonly IPlaceholderService placeholder;
        public ICollection<Lowerthird> Lowerthirds { get; set; }

        public StartupDialogViewModel(Configuration conf, IPlaceholderService placeholder, IEventAggregator ea)
        {
            this.placeholder = placeholder;

            Lowerthirds = conf.Lowerthirds;

            ea.GetEvent<Core.Events.StartupDialogClosing>().Subscribe(Closing);
        }

        private async void Closing(bool isConfirmed)
        {
            await Task.Delay(1);
            foreach (var item in Lowerthirds)
            {
                if (item.Text == null)
                    item.Text = "";
                placeholder.Placeholders[item.Title] = item.Text;
            }
        }
    }
}
