using System.Reflection.Emit;
using System.Text;
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

namespace FullScreenClock {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private DispatcherTimer _timer;
        private bool _isDragging = false;
        private Point _startPoint;
        public MainWindow() {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            // Update the Label with the current time
            myLabel.Content = DateTime.Now.ToString("hh:mm");
            myLabel2.Content = DateTime.Now.ToString("dd MM yyyy");
            var TargetTime = DateTime.Today.AddHours(20);
            myLabel3.Content = "-" + (TargetTime - DateTime.Now.TimeOfDay).Hour + ":" + (TargetTime - DateTime.Now.TimeOfDay).Minute.ToString("00") + ":" + (TargetTime - DateTime.Now.TimeOfDay).Second.ToString("00");
            myLabel4.Content = "-" + (5 - (int)DateTime.Now.DayOfWeek);
            myLabel5.Content = "-" + (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day);
            int daysInYear = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;
            myLabel6.Content = "-" + (daysInYear - DateTime.Now.DayOfYear);
            //myLabel7.Content = DateTime.Now.ToString();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) {
                _isDragging = true;
                _startPoint = e.GetPosition(this);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e) {
            if (_isDragging) {
                Point currentPoint = e.GetPosition(this);
                double deltaX = currentPoint.X - _startPoint.X;
                double deltaY = currentPoint.Y - _startPoint.Y;

                Left += deltaX;
                Top += deltaY;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e) {
            _isDragging = false;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) {
            // Minimize the window

            WindowState = WindowState.Minimized;
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e) {
            _isDragging = false;
        }
    }
}