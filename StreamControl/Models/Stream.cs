using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Models
{
    public class Stream : BindableBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { SetProperty(ref isActive, value); }
        }

        private IEnumerable<string> activateCommands;
        public IEnumerable<string> ActivateCommands
        {
            get { return activateCommands; }
            set { SetProperty(ref activateCommands, value); }
        }

        private IEnumerable<string> deactivateCommands;
        public IEnumerable<string> DeactivateCommands
        {
            get { return deactivateCommands; }
            set { SetProperty(ref deactivateCommands, value); }
        }
    }
}
