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

namespace ConstructionCompany.Pages.OrderPages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<Entity.OrderView> Views = new List<Entity.OrderView>();
        public OrderPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Views.AddRange(Entity.AppData.context.OrderView.ToList());
            View.ItemsSource = Views;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Entity.OrderView orderView = (Entity.OrderView)View.SelectedItem;
            if (orderView != null)
            {
                AppData.context.Object.Remove(AppData.context.Object.Where(i => i.Name == orderView.Name).FirstOrDefault());
                AppData.context.Order.Remove(AppData.context.Order.Where(i => i.idOrder == orderView.idOrder).FirstOrDefault());
                AppData.context.UseMaterial.RemoveRange(AppData.context.UseMaterial.Where(i => i.idOrder == orderView.idOrder).ToList());
                AppData.context.ServiceOrder.RemoveRange(AppData.context.ServiceOrder.Where(i => i.idOrder == orderView.idOrder).ToList());
                AppData.context.SaveChanges();
                MessageBox.Show("Заказ удалён!");
                Views.Remove(orderView);
                View.Items.Refresh();
            }
            else
                MessageBox.Show("Выберите заказ!");
            
        }
    }
}
