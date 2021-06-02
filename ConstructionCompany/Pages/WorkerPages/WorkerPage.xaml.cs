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

namespace ConstructionCompany.Pages.WorkerPages
{
    /// <summary>
    /// Логика взаимодействия для Worker.xaml
    /// </summary>
    public partial class WorkerPage : Page
    {
        
        public WorkerPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Entity.WorkerView> Views = Entity.AppData.context.WorkerView.ToList();
            LoadView(Views);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Entity.WorkerView workerView = (Entity.WorkerView)View.SelectedItem;
            if (workerView != null)
            {
                AppData.context.Worker.Remove(AppData.context.Worker.Where(i => i.idWorker == workerView.idWorker).FirstOrDefault());
                AppData.context.specialtiesWorkers.RemoveRange(AppData.context.specialtiesWorkers.Where(i => i.idWorker == workerView.idWorker).ToList());
                AppData.context.SaveChanges();
                MessageBox.Show("Рабочий удалён!");
                View.Items.Remove(workerView);
            }
            else
                MessageBox.Show("Выберите рабочего!");
            
        }
        private void Searchbut_Click(object sender, RoutedEventArgs e)
        {

            List<Entity.WorkerView> view = AppData.context.WorkerView.ToList();
            if (SearchSurName.Text != "")
                view = view.FindAll(i => i.Surname == SearchSurName.Text);
            LoadView(view);

        }
        public void LoadView(List<Entity.WorkerView> views)
        {
            View.Items.Clear();
            foreach (var item in views)
                View.Items.Add(item);
        }

        private void SearchClear_Click(object sender, RoutedEventArgs e)
        {
            SearchSurName.Text = "";
            List<Entity.WorkerView> Views = Entity.AppData.context.WorkerView.ToList();
            LoadView(Views);
        }
    }
}
