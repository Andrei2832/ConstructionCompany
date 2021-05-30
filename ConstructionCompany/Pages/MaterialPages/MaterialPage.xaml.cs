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
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        List<Entity.Material> Views = new List<Entity.Material>();
        public MaterialPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Views.AddRange(Entity.AppData.context.Material.ToList());
            View.ItemsSource = Views;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Entity.Material material = (Entity.Material)View.SelectedItem;
            if (material != null)
            {
                AppData.context.Material.Remove(AppData.context.Material.Where(i => i.idMaterial == material.idMaterial).FirstOrDefault());
                AppData.context.SaveChanges();
                MessageBox.Show("Материал удалёна!");
                Views.Remove(material);
                View.Items.Refresh();
            }
            else
                MessageBox.Show("Выберите материал!");
        }
    }
}
