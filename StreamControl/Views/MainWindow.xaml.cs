using System.Windows;

namespace StreamControl.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MinHeight = this.MaxHeight = SystemParameters.WorkArea.Height;
        }
    }
}
