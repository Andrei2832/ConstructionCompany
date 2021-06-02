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
        public MaterialPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Entity.Material> Views = Entity.AppData.context.Material.ToList();
            LoadView(Views);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Entity.Material material = (Entity.Material)View.SelectedItem;
            if (material != null)
            {
                AppData.context.Material.Remove(AppData.context.Material.Where(i => i.idMaterial == material.idMaterial).FirstOrDefault());
                AppData.context.SaveChanges();
                MessageBox.Show("Материал удалёна!");
                View.Items.Remove(material);
            }
            else
                MessageBox.Show("Выберите материал!");
        }
        private void Searchbut_Click(object sender, RoutedEventArgs e)
        {

            List<Entity.Material> view = AppData.context.Material.ToList();
            if (SearchName.Text != "")
                view = view.FindAll(i => i.Name == SearchName.Text);
            LoadView(view);

        }
        public void LoadView(List<Entity.Material> views)
        {
            View.Items.Clear();
            foreach (var item in views)
                View.Items.Add(item);
        }

        private void SearchClear_Click(object sender, RoutedEventArgs e)
        {
            SearchName.Text = "";
            List<Entity.Material> Views = Entity.AppData.context.Material.ToList();
            LoadView(Views);
        }
    }
}
