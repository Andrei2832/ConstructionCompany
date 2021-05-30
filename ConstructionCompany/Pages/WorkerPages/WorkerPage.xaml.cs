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
        List<Entity.WorkerView> Views = new List<Entity.WorkerView>();
        public WorkerPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Views = Entity.AppData.context.WorkerView.ToList();
            View.ItemsSource = Views;
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
                Views.Remove(workerView);
                View.Items.Refresh();
            }
            else
                MessageBox.Show("Выберите рабочего!");
            
        }
    }
}
