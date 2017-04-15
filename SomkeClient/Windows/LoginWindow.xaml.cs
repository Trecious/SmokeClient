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

            setSize();
            
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;

            MainBtn.IsEnabled = false;

        }

        private void setSize()
        {
            this.Width = SystemParameters.WorkArea.Width / 3.3;
            this.Height = this.Width / 16 * 9;
        }

        #region Resizing and Dragging Event Handlers
        private double _lastHeight;
        private double _lastWidth;

        private void Location_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_maximize)
            {
                this.Width = _maximizeWidth;
                this.Height = _maximizeHeight;

                this.Left = PointToScreen(Mouse.GetPosition(this)).X;
                this.Top = PointToScreen(Mouse.GetPosition(this)).Y;

                _maximize = true;
            }
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

        #region Control Button Event Handlers

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private bool _maximize = true;
        private double _maximizeWidth;
        private double _maximizeHeight;
        private double _maximizeLeft;
        private double _maximizeTop;

        private void MultiBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_maximize)
            {
                _maximizeWidth = this.Width;
                _maximizeHeight = this.Height;
                _maximizeLeft = this.Left;
                _maximizeTop = this.Top;

                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                this.Left = 0;
                this.Top = 0;
                _maximize = false;
            }
            else
            {
                this.Width = _maximizeWidth;
                this.Height = _maximizeHeight;
                this.Left = _maximizeLeft;
                this.Top = _maximizeTop;

                _maximize = true;
            }

        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        private void PwTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PwTxt.Password.Length != 0)
            {
                MainBtn.IsEnabled = true;
            }
            else
            {
                MainBtn.IsEnabled = false;
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
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
