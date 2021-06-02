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
using ConstructionCompany.Entity;

namespace ConstructionCompany.Pages.ServicePages
{
    /// <summary>
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        public AddServicePage()
        {
            InitializeComponent();
        }

        private void ServislFinish_Click(object sender, RoutedEventArgs e)
        {
            Emptiness();
            if (NameBox.Text != "" && UnitBox.Text != "" && CostBox.Text != "")
            {
                Service service = AppData.context.Service.Add(new Service()
                {
                    Name = NameBox.Text,
                    unit = UnitBox.Text,
                    Cost = Int32.Parse(CostBox.Text)
                });
                AppData.context.SaveChanges();
                MessageBox.Show("Услуга добавлена!");
                this.NavigationService.Navigate(new ServicePages.ServicePage());
            }
        }
        void Emptiness()
        {
            if (NameBox.Text == "")
                NameBox.BorderBrush = Brushes.Red;
            else
                NameBox.BorderBrush = Brushes.LightSlateGray;
            if (UnitBox.Text == "")
                UnitBox.BorderBrush = Brushes.Red;
            else
                UnitBox.BorderBrush = Brushes.LightSlateGray;
            if (CostBox.Text == "")
                CostBox.BorderBrush = Brushes.Red;
            else
                CostBox.BorderBrush = Brushes.LightSlateGray;

        }
        private void IsDigitIsLetter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && !e.Text.Contains(' ') && !Char.IsLetter(e.Text, 0))
                e.Handled = true;
        }
        private void IsDigit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && !e.Text.Contains(' '))
                e.Handled = true;
        }
        private void IsLetterAndPoint_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0) && !e.Text.Contains(' ') && !e.Text.Contains('.'))
                e.Handled = true;
        }
        private void QuantityBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
