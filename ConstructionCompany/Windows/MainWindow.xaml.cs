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
using System.Windows.Shapes;

namespace ConstructionCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OrderBut_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Pages.OrderPages.OrderPage());
            ButGray();
            OrderBut.Background = Brushes.WhiteSmoke;
            OrderBut.Margin = new Thickness(0, 0, 0, 0);
        }

        public void WorkerBut_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Pages.WorkerPages.WorkerPage());
            ButGray();
            WorkerBut.Background = Brushes.WhiteSmoke;
            WorkerBut.Margin = new Thickness(0, 10, 0, 0);
        }

        public void BrigadeBut_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Pages.BrigadePages.BrigadePage());
            ButGray();
            BrigadeBut.Background = Brushes.WhiteSmoke;
            BrigadeBut.Margin = new Thickness(0, 10, 0, 0);
        }


        public void MaterialBut_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Pages.MaterialPages.MaterialPage());
            ButGray();
            MaterialBut.Background = Brushes.WhiteSmoke;
            MaterialBut.Margin = new Thickness(0, 10, 0, 0);
        }

        public void ServicelBut_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Pages.ServicePages.ServicePage());
            ButGray();
            ServicelBut.Background = Brushes.WhiteSmoke;
            ServicelBut.Margin = new Thickness(0, 10, 0, 0);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButGray()
        {
            Welcome.Visibility = Visibility.Hidden;
            OrderBut.Background = Brushes.LightGray;
            WorkerBut.Background = Brushes.LightGray;
            BrigadeBut.Background = Brushes.LightGray;
            MaterialBut.Background = Brushes.LightGray;
            ServicelBut.Background = Brushes.LightGray;

            OrderBut.Margin = new Thickness(0, 10, 0, 0);
            WorkerBut.Margin = new Thickness(0, 20, 0, 0);
            BrigadeBut.Margin = new Thickness(0, 20, 0, 0);
            MaterialBut.Margin = new Thickness(0, 20, 0, 0);
            ServicelBut.Margin = new Thickness(0, 20, 0, 0);
        }

        public void ButBlock()
        {
            OrderBut.IsEnabled = false;
            WorkerBut.IsEnabled = false;
            BrigadeBut.IsEnabled = false;
            MaterialBut.IsEnabled = false;
            ServicelBut.IsEnabled = false;
        }
        public void ButNotBlock()
        {
            OrderBut.IsEnabled = true;
            WorkerBut.IsEnabled = true;
            BrigadeBut.IsEnabled = true;
            MaterialBut.IsEnabled = true;
            ServicelBut.IsEnabled = true;
        }


        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            if (OrderBut.Background == Brushes.LightGray &&
            WorkerBut.Background == Brushes.LightGray &&
            BrigadeBut.Background == Brushes.LightGray &&
            MaterialBut.Background == Brushes.LightGray &&
            ServicelBut.Background == Brushes.LightGray)
            {
                MessageBox.Show("Выбирите раздел!");
            }
            else
            {
                if (OrderBut.Background == Brushes.WhiteSmoke)
                {
                    Main.Navigate(new Pages.OrderPages.AddOrderPage());
                    ButBlock();
                    CancelBut.Visibility = Visibility.Visible;
                }
                else if (WorkerBut.Background == Brushes.WhiteSmoke)
                {
                    Main.Navigate(new Pages.WorkerPages.AddWorkerPage());
                    ButBlock();
                    CancelBut.Visibility = Visibility.Visible;
                }
                else if (BrigadeBut.Background == Brushes.WhiteSmoke)
                {
                    Main.Navigate(new Pages.BrigadePages.AddBrigadePage());
                    ButBlock();
                    CancelBut.Visibility = Visibility.Visible;
                }
                else if (MaterialBut.Background == Brushes.WhiteSmoke)
                {
                    Main.Navigate(new Pages.MaterialPages.AddMaterialPage());
                    ButBlock();
                    CancelBut.Visibility = Visibility.Visible;
                }
                else if (ServicelBut.Background == Brushes.WhiteSmoke)
                {
                    Main.Navigate(new Pages.ServicePages.AddServicePage());
                    ButBlock();
                    CancelBut.Visibility = Visibility.Visible;
                }
            }
        }

        private void CancelBut_Click(object sender, RoutedEventArgs e)
        {
            if (OrderBut.Background == Brushes.WhiteSmoke)
            {
                OrderBut_Click(sender,e);
                ButNotBlock();
                CancelBut.Visibility = Visibility.Hidden;
            }
            else if (WorkerBut.Background == Brushes.WhiteSmoke)
            {
                WorkerBut_Click(sender, e);
                ButNotBlock();
                CancelBut.Visibility = Visibility.Hidden;
            }
            else if (BrigadeBut.Background == Brushes.WhiteSmoke)
            {
                BrigadeBut_Click(sender, e);
                ButNotBlock();
                CancelBut.Visibility = Visibility.Hidden;
            }
            else if (MaterialBut.Background == Brushes.WhiteSmoke)
            {
                MaterialBut_Click(sender, e);
                ButNotBlock();
                CancelBut.Visibility = Visibility.Hidden;
            }
            else if (ServicelBut.Background == Brushes.WhiteSmoke)
            {
                ServicelBut_Click(sender, e);
                ButNotBlock();
                CancelBut.Visibility = Visibility.Hidden;
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitAkkBut_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Login = "0";
            Properties.Settings.Default.Save();
            EnteranceWindow enteranceWindow = new EnteranceWindow();
            enteranceWindow.Show();
            this.Close();
        }
    }
}
