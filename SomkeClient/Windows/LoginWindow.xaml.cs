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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SomkeClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Width = SystemParameters.WorkArea.Width / 3.3;
            this.Height = this.Width / 16 * 9;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;

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
            SetLastWidthHeight();

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
                        else if (this.Height != _lastHeight)
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

        private void PwTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PwTxt.Password.Length != 0)
            {
                MainBtn.Content = "Login";
                MainBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF17954D"));
                MainBtn.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1C7C44"));
            }
            else
            {
                MainBtn.Content = "Register";
                MainBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3498DB"));
                MainBtn.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF216A9B"));
            }
        }

        private void FogPw_MouseEnter(object sender, MouseEventArgs e)
        {
            FogPw.Foreground = PwTxt.Foreground;
        }

        private void FogPw_MouseLeave(object sender, MouseEventArgs e)
        {
            FogPw.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D4651"));
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MainBtn.Content.ToString() == "Login")
            {
                MainWindow w = new MainWindow();
                w.Show();
               
            }
            else
            {

            }
        }
    }
}
