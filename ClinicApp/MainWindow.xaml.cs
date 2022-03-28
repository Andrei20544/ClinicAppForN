using ClinicApp.Views;
using System.Windows;
using System.Windows.Input;

namespace ClinicApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) { DragMove(); }
        private void Button_Click(object sender, RoutedEventArgs e) { Close(); }
        private void Button_Click_1(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Clients clients = Clients.GetInstance();
            clients.Show();
        }

        private void Doctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorsWindows doctorsWindows = DoctorsWindows.GetInstance();
            doctorsWindows.Show();
        }

        private void Med_Click(object sender, RoutedEventArgs e)
        {
            MedConsWindow medConsWindow = MedConsWindow.GetInstance();
            medConsWindow.Show();
        }

        private void Pill_Click(object sender, RoutedEventArgs e)
        {
            PillsWindow pillsWindow = PillsWindow.GetInstance();
            pillsWindow.Show();
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            ServicesWindow servicesWindow = ServicesWindow.GetInstance();
            servicesWindow.Show();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = RegisterWindow.GetInstance();
            registerWindow.Show();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleWindow scheduleWindow = ScheduleWindow.GetInstance();
            scheduleWindow.Show();
        }
    }
}
