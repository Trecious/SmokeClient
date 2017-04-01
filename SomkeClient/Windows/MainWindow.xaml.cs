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
using System.Windows.Interop;

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

            SetLastWidthHeight();

        }


        #region Resizing and Dragging EventHandlers
        private double _lastHeight;
        private double _lastWidth;

        private void Location_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = HwndSource.FromVisual(this) as HwndSource;
            if (source != null)
            {
                source.AddHook(new HwndSourceHook(WinProc));
            }
        }

        public const Int32 WM_EXITSIZEMOVE = 0x0232;
        private IntPtr WinProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {
            IntPtr result = IntPtr.Zero;
            switch (msg)
            {
                case WM_EXITSIZEMOVE:
                    {
                        if (this.Width != _lastWidth)
                        {
                            this.Height = this.Width / 16 * 9;
                            SetLastWidthHeight();
                        }
                        else if(this.Height != _lastHeight)
                        {
                            this.Width = this.Height / 9 * 16;
                            SetLastWidthHeight();
                        }
                    }
                    break;
            }

            return result;
        }

        public void SetLastWidthHeight()
        {
            this._lastWidth = this.Width;
            this._lastHeight = this.Height;
        }
        #endregion

    }
}
