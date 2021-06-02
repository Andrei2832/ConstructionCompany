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

namespace ConstructionCompany.Pages.MaterialPages
{
    /// <summary>
    /// Логика взаимодействия для AddMaterialPage.xaml
    /// </summary>
    public partial class AddMaterialPage : Page
    {
        public AddMaterialPage()
        {
            InitializeComponent();
        }

        private void MaterialFinish_Click(object sender, RoutedEventArgs e)
        {
            Emptiness();
            if (NameBox.Text != "" && UnitBox.Text != "" && CostBox.Text != "")
            {
                Material material = AppData.context.Material.Add(new Material()
                {
                    Name = NameBox.Text,
                    unit = UnitBox.Text,
                    Cost = Int32.Parse(CostBox.Text)
                });
                AppData.context.SaveChanges();
                MessageBox.Show("Материал добавлен!");
                this.NavigationService.Navigate(new MaterialPages.MaterialPage());
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
