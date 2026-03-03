using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ClassConnection;

namespace PhoneBook_Bartova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Connection connect;
        public static Pages.Main main;

        public MainWindow()
        {
            InitializeComponent();
            connect = new Connection();
            connect.LoadData(Connection.tabels.users);
            connect.LoadData(Connection.tabels.calls);
            main = new Pages.Main();
            OpenPageMain();
        }
        public void OpenPageMain()
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation();
            fadeOutAnimation.From = 1;
            fadeOutAnimation.To = 0;
            fadeOutAnimation.Duration = TimeSpan.FromSeconds(0.6);
            fadeOutAnimation.Completed += delegate
            {
                frame.Navigate(main);
                DoubleAnimation fadeInAnimation = new DoubleAnimation();
                fadeInAnimation.From = 0;
                fadeInAnimation.To = 1;
                fadeInAnimation.Duration = TimeSpan.FromSeconds(1.2);
                frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);
            };
            frame.BeginAnimation(Frame.OpacityProperty, fadeOutAnimation);
        }
    }
}
