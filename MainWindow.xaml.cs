using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            windowForm.Height = 65;
            Thickness padding = lbTime.Padding;
            padding.Top = 6;
        }

        private void t_Tick(object sender, EventArgs e)
        {
            lbTime.Content = DateTime.Now.ToLongTimeString();

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
            if (windowForm.Height == 65)
                windowForm.Height = 150;
            else
                windowForm.Height = 65;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thickness padding = lbTime.Padding;
            if (e.ClickCount == 2)
            {
                if (TitleBar.Visibility == Visibility.Visible)
                {
                    TitleBar.Visibility = Visibility.Collapsed;
                    windowForm.Height = 40;
                    padding.Top = 15;
                }
                else
                {
                    TitleBar.Visibility = Visibility.Visible;
                    windowForm.Height = 65;
                    padding.Top = 6;
                }
            }
        }
         

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            mediaPlayer.Open(BBCHalfHourPips);
            mediaPlayer.Play();
            //PlayBBCHourPips().ConfigureAwait(false);
        }

        public Task PlayBBCHourPips()
        {
            return Task.Run(() => Play(BBCHalfHourPips));
        }

        public void Play(Uri pipsAudio)
        {
            MediaPlayer mp = new MediaPlayer();
            mp.Open(pipsAudio);
            mp.Play();
            //Thread.Sleep((int)waitDuration);
        }

        private void btnPlayHour_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(BBCHourPips);
            mediaPlayer.Play();
        }
        private void btnPlayHalfHour_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(BBCHalfHourPips);
            mediaPlayer.Play();
        }

    }
}
