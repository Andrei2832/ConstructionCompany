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

namespace ConstructionCompany.Pages.BrigadePages
{
    /// <summary>
    /// Логика взаимодействия для Brigade.xaml
    /// </summary>
    public partial class BrigadePage : Page
    {
        public BrigadePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Entity.BrigadeView> Views = Entity.AppData.context.BrigadeView.ToList();
            LoadView(Views);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Entity.BrigadeView brigade = (Entity.BrigadeView)View.SelectedItem;
            if (brigade != null)
            {
                try
                {
                    AppData.context.WorkersBrigade.RemoveRange(AppData.context.WorkersBrigade.Where(j => j.idBrigade == brigade.idBrigade).ToList());
                    AppData.context.Brigade.Remove(AppData.context.Brigade.Where(i => i.idBrigade == brigade.idBrigade).FirstOrDefault());
                    AppData.context.SaveChanges();
                    MessageBox.Show("Бригада удалёна!");
                    View.Items.Remove(brigade);
                }
                catch (Exception)
                {
                    MessageBox.Show("Нельзя удалить бригаду выполняющую заказ!");
                }
            }
            else
                MessageBox.Show("Выберите бригаду!");
        }
        private void Searchbut_Click(object sender, RoutedEventArgs e)
        {

            List<Entity.BrigadeView> view = AppData.context.BrigadeView.ToList();
            if (SearchName.Text != "")
                view = view.FindAll(i => i.Name == SearchName.Text);
            if (SearchBrig.Text != "")
                view = view.FindAll(i => i.Brigadier == SearchBrig.Text);
            LoadView(view);

        }
        public void LoadView(List<Entity.BrigadeView> views)
        {
            View.Items.Clear();
            foreach (var item in views)
                View.Items.Add(item);
        }

        private void SearchClear_Click(object sender, RoutedEventArgs e)
        {
            SearchName.Text = "";
            SearchBrig.Text = "";
            List<Entity.BrigadeView> Views = Entity.AppData.context.BrigadeView.ToList();
            LoadView(Views);
        }
    }
}
