using MahApps.Metro.Controls;
using Prism.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StreamControl
{
    public class PopupMetroWindowAction : PopupWindowAction
    {
        protected override Window CreateWindow()
        {           
            return new MetroWindow();
        }
    }
}
