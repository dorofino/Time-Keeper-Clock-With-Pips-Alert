using Microsoft.Win32;
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
using System.Windows.Threading;

namespace TimeKeeperClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer = new DispatcherTimer();
        private MediaPlayer mediaPlayer = new MediaPlayer();
        Uri BBCHourPips = new Uri(@"BBCHourPips.wav", UriKind.RelativeOrAbsolute);
        Uri BBCHalfHourPips = new Uri(@"BBCHalfHourPips.wav", UriKind.RelativeOrAbsolute); 

        public MainWindow()
        {
            InitializeComponent();
             
            Timer.Tick += new EventHandler(t_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1); 
            Timer.Start();
             
        }

        private void t_Tick(object sender, EventArgs e)
        {
            label1.Content = DateTime.Now.ToLongTimeString();

            if (DateTime.Now.Minute == 59 && DateTime.Now.Second == 53)
            {
                mediaPlayer.Open(BBCHourPips);
                mediaPlayer.Play();
                Console.WriteLine("=========NEW HOUR");
                Console.WriteLine("========={0}", DateTime.Now);
            }

            if (DateTime.Now.Minute == 29 && DateTime.Now.Second == 58)
            { 
                    mediaPlayer.Open(BBCHalfHourPips);
                    mediaPlayer.Play();
                    Console.WriteLine("=========HALF HOUR");
                    Console.WriteLine("========={0}", DateTime.Now); 
            }
        }

        private void Window_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        { 
            var window = (Window)sender;
            window.Topmost = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ClickCount != 2)
                if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        { 
            Application.Current.Shutdown();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (TitleBar.Visibility == Visibility.Visible)
                    TitleBar.Visibility = Visibility.Collapsed;
                else
                    TitleBar.Visibility = Visibility.Visible;
            }
        }
    }
}
