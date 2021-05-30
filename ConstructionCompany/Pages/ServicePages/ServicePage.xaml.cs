﻿using System;
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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        List<Entity.Service> Views = new List<Entity.Service>();
        public ServicePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Views.AddRange(Entity.AppData.context.Service.ToList());
            View.ItemsSource = Views;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Entity.Service service = (Entity.Service)View.SelectedItem;
            if (service != null)
            {
                AppData.context.Service.Remove(AppData.context.Service.Where(i => i.idService == service.idService).FirstOrDefault());
                AppData.context.SaveChanges();
                MessageBox.Show("Услуга удалёна!");
                Views.Remove(service);
                View.Items.Refresh();
            }
            else
                MessageBox.Show("Выберите услугу!");
        }
    }
}
