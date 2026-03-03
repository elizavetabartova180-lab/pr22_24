using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ClassConnection;
using ClassModule;


namespace PhoneBook_Bartova.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public enum page_main { users, calls, none };
        public static page_main page_select;
        public Main()
        {
            InitializeComponent();
            page_select = page_main.none;
        }
        private void Click_Phone(object sender, RoutedEventArgs e)
        {
            if (frame_main.Visibility == Visibility.Visible)
            {
                MainWindow.main.Anim_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            }
            if (page_select != page_main.users)
            {
                page_select = page_main.users;

                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parent.Children.Clear();
                    DoubleAnimation opgr1Animation = new DoubleAnimation();
                    opgr1Animation.From = 0;
                    opgr1Animation.To = 1;
                    opgr1Animation.Duration = TimeSpan.FromSeconds(0.2);
                    opgr1Animation.Completed += delegate
                    {
                        Dispatcher.InvokeAsync(async () =>
                        {
                            MainWindow.connect.LoadData(ClassConnection.Connection.tabels.users);

                            foreach (User user_itm in MainWindow.connect.users)
                            {
                                if (page_select == page_main.users)
                                {
                                    parent.Children.Add(new Elements.User_itm(user_itm));
                                    await Task.Delay(90);
                                }
                            }

                            if (page_select == page_main.users)
                            {
                                var ff = new Pages.PagesUser.User_win(new User());
                                parent.Children.Add(new Elements.Add_itm(ff));
                            }
                        });
                    };
                    parent.BeginAnimation(StackPanel.OpacityProperty, opgr1Animation);
                };
                parent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        private void Click_History(object sender, RoutedEventArgs e)
        {
            if (frame_main.Visibility == Visibility.Visible)
            {
                MainWindow.main.Anim_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            }
            if (page_select != page_main.calls)
            {
                page_select = page_main.calls;

                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parent.Children.Clear();
                    DoubleAnimation opgr1Animation = new DoubleAnimation();
                    opgr1Animation.From = 0;
                    opgr1Animation.To = 1;
                    opgr1Animation.Duration = TimeSpan.FromSeconds(0.2);
                    opgr1Animation.Completed += delegate
                    {
                        Dispatcher.InvokeAsync(async () =>
                        {
                            MainWindow.connect.LoadData(ClassConnection.Connection.tabels.calls);

                            foreach (call call_itm in MainWindow.connect.calls)
                            {
                                if (page_select == page_main.calls)
                                {
                                    parent.Children.Add(new Elements.Call_itm(call_itm));
                                    await Task.Delay(90);
                                }
                            }

                            if (page_select == page_main.calls)
                            {
                                var ff = new Pages.PagesUser.Call_win(new ClassModule.call());
                                parent.Children.Add(new Elements.Add_itm(ff));
                            }
                        });
                    };
                    parent.BeginAnimation(StackPanel.OpacityProperty, opgr1Animation);
                };
                parent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        public void Anim_move(Control control1, Control control2, Frame frame_main = null, Page pages = null, page_main page_restart = page_main.none)
        {
            if (page_restart != page_main.none)
            {
                if (page_restart == page_main.users)
                {
                    page_select = page_main.none;
                    Click_Phone(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.calls)
                {
                    page_select = page_main.none;
                    Click_History(new object(), new RoutedEventArgs());
                }
            }
            else
            {
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.3);
                opgridAnimation.Completed += delegate
                {
                    if (pages != null)
                    {
                        frame_main.Navigate(pages);
                        // if (control1 == frame_main && control2 == frame_main)
                        // if (MainWindow.actualUser.role != "admin")
                        // {
                        //     parent.Children.Clear();
                        // }
                    }

                    control1.Visibility = Visibility.Hidden;
                    control2.Visibility = Visibility.Visible;

                    DoubleAnimation opgr1Animation = new DoubleAnimation();
                    opgr1Animation.From = 0;
                    opgr1Animation.To = 1;
                    opgr1Animation.Duration = TimeSpan.FromSeconds(0.4);
                    control2.BeginAnimation(ScrollViewer.OpacityProperty, opgr1Animation);
                };
                control1.BeginAnimation(ScrollViewer.OpacityProperty, opgridAnimation);
            }
        }
    }
}
