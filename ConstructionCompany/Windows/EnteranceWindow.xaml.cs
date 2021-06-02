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

namespace ConstructionCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class EnteranceWindow : Window
    {
        public EnteranceWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Login != "0") {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            } 
        }
        private void PasswodBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswodBox.Text == "Введите пароль")
            {
                PasswodBox.Text = "";
                PasswodBox.Foreground = Brushes.DarkSlateGray;
            }
        }

        private void PasswodBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswodBox.Text == "")
            {
                PasswodBox.Text = "Введите пароль";
                PasswodBox.Foreground = Brushes.Gray;
            }
        }
        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "Введите логин")
            {
                LoginBox.Text = "";
                LoginBox.Foreground = Brushes.DarkSlateGray;
            }
        }

        private void LoginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "")
            {
                LoginBox.Text = "Введите логин";
                LoginBox.Foreground = Brushes.Gray;
            }
        }

        private void Avtores_Click(object sender, RoutedEventArgs e)
        {
            int a = Entity.AppData.context.Entrance.Where(i => i.Login == LoginBox.Text).Select(j => j.idEntrance).FirstOrDefault();
            if (Entity.AppData.context.Entrance.Where(i => i.Login == LoginBox.Text && i.Password == PasswodBox.Text).Select(j => j.idEntrance).FirstOrDefault() != 0 )
            {
                if (Check.IsChecked == true)
                {
                    Properties.Settings.Default.Login = LoginBox.Text;
                    Properties.Settings.Default.Save();
                }
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Логин или Пароль не верны","Ошибка!");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
