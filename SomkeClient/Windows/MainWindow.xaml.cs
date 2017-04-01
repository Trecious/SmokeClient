using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SomkeClient.BackEnd;

namespace SomkeClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _smokeMainWindow_Height;
        private double _smokeMainWindow_Width;

        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Width = SystemParameters.WorkArea.Width - 100;
            this.Height = this.Width / 16 * 9;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;

            this._smokeMainWindow_Height = this.Height;
            this._smokeMainWindow_Width = this.Width;
  
        }

        private void Location_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (sizeInfo.WidthChanged)
                this.Height = sizeInfo.NewSize.Width / 16 * 9;
            else
                this.Width = sizeInfo.NewSize.Height / 9 * 16;
        }
    }
}
