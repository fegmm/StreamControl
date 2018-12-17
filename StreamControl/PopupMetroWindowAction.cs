using MahApps.Metro.Controls;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace StreamControl
{
    public class PopupMetroWindowAction : PopupWindowAction
    {
        public bool DisplayOverlay { get; set; }

        public PopupMetroWindowAction() : base()
        {
            DisplayOverlay = true;
        }

        protected override Window CreateWindow()
        {
            return new MetroWindow();
        }

        protected override Window GetWindow(INotification notification)
        {
            var win = base.GetWindow(notification);

            if (DisplayOverlay)
            {
                (Application.Current.MainWindow as MetroWindow)?.ShowOverlayAsync();

                win.Closed += (sender, e) =>
                {
                    (Application.Current.MainWindow as MetroWindow)?.HideOverlayAsync();
                };
            }
            return win;
        }
    }
}
