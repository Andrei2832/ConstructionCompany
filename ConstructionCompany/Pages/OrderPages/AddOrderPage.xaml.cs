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
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        List<String> TypeObject = new List<string>();
        List<String> Brigades = new List<string>();
        List<String> NameService = new List<string>();
        List<String> NameMaterial = new List<string>();
        List<ViewServise> viewServises = new List<ViewServise>();
        List<ViewMaterial> viewMaterials = new List<ViewMaterial>();

        public AddOrderPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TypeObject.AddRange(AppData.context.objectType.Select(i => i.Type).ToList());
            TypeObjectBox.ItemsSource = TypeObject;

            Brigades.AddRange(AppData.context.Brigade.Select(i => i.Name).ToList());
            BrigadeBox.ItemsSource = Brigades;

            NameService.AddRange(AppData.context.Service.Select(i => i.Name).ToList());
            foreach (var item in NameService)
                NameBox.Items.Add(item);
            NameBox.SelectedIndex = 0;

            NameMaterial.AddRange(AppData.context.Material.Select(i => i.Name).ToList());
            foreach (var item in NameMaterial)
                NameBoxMaterial.Items.Add(item);
            NameBoxMaterial.SelectedIndex = 0;

            DateBox.BlackoutDates.AddDatesInPast();
            DateBox.BlackoutDates.Add(new CalendarDateRange(DateTime.Now));
        }

        private void NameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(NameBox.SelectedValue != null)
            {
                string txt = NameBox.SelectedValue.ToString();
                CostBlock.Text = AppData.context.Service.Where(i => i.Name == txt).Select(j => j.Cost).FirstOrDefault().ToString();
                UnitBlock.Text = AppData.context.Service.Where(i => i.Name == txt).Select(j => j.unit).FirstOrDefault();
                if (QuantityBox.Text != "")
                    SumCost.Text = (Int32.Parse(CostBlock.Text) * Int32.Parse(QuantityBox.Text)).ToString();
                else
                    QuantityBox.BorderThickness = new Thickness(3);
            }
            
        }

        private void QuantityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameBox.SelectedIndex != -1 && QuantityBox.Text != "")
            {
                SumCost.Text = (Int32.Parse(CostBlock.Text) * Int32.Parse(QuantityBox.Text)).ToString();
                QuantityBox.BorderThickness = new Thickness(0);
            }
        }

        private void IsDigit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && !e.Text.Contains(' '))
                e.Handled = true;
        }
        private void IsLetter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0) && !e.Text.Contains(' '))
                e.Handled = true;
        }
        private void QuantityBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            ListService.Visibility = Visibility.Hidden;
            AddService.Visibility = Visibility.Hidden;
            DeleteService.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Visible;
            CostText.Visibility = Visibility.Visible;
            CostBlock.Visibility = Visibility.Visible;
            UnitText.Visibility = Visibility.Visible;
            UnitBlock.Visibility = Visibility.Visible;
            QuantityText.Visibility = Visibility.Visible;
            QuantityBox.Visibility = Visibility.Visible;
            SumCostText.Visibility = Visibility.Visible;
            SumCost.Visibility = Visibility.Visible;
            AddServiceList.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ListService.Visibility = Visibility.Visible;
            AddService.Visibility = Visibility.Visible;
            DeleteService.Visibility = Visibility.Visible;
            NameBox.Visibility = Visibility.Hidden;
            CostText.Visibility = Visibility.Hidden;
            CostBlock.Visibility = Visibility.Hidden;
            UnitText.Visibility = Visibility.Hidden;
            UnitBlock.Visibility = Visibility.Hidden;
            QuantityText.Visibility = Visibility.Hidden;
            QuantityBox.Visibility = Visibility.Hidden;
            SumCostText.Visibility = Visibility.Hidden;
            SumCost.Visibility = Visibility.Hidden;
            AddServiceList.Visibility = Visibility.Hidden;
            Cancel.Visibility = Visibility.Hidden;
        }

        private void AddServiceList_Click(object sender, RoutedEventArgs e)
        {
            if (QuantityBox.Text == "")
                QuantityBox.BorderThickness = new Thickness(3);
            else
            {
                ViewServise view = new ViewServise();
                view.Name = NameBox.Text;
                view.Cost = Int32.Parse(CostBlock.Text);
                view.Unit = UnitBlock.Text;
                view.Quantity = Int32.Parse(QuantityBox.Text);
                view.Sum = Int32.Parse(SumCost.Text);

                if (view.Quantity > 0)
                {
                    QuantityBox.BorderThickness = new Thickness(0);

                    NameBox.Items.Remove(view.Name);
                    NameBox.SelectedIndex = 0;
                    if (NameBox.Items.Count == 0)
                        AddServiceList.IsEnabled = false;

                    ListService.Items.Add(view);
                    viewServises.Add(view);
                    Cancel_Click(sender, e);
                    SumAllText();
                }
                else
                    QuantityBox.BorderThickness = new Thickness(2);

            }
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            ViewServise view = (ViewServise)ListService.SelectedItem;
            if (view != null)
            {
                NameBox.Items.Add(view.Name);
                if (NameBox.Items.Count != 0)
                    AddServiceList.IsEnabled = true;

                ListService.Items.Remove(view);
                viewServises.Remove(view);
                SumAllText();
            }
        }

        private void NameBoxMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NameBoxMaterial.SelectedValue != null)
            {
                string txt = NameBoxMaterial.SelectedValue.ToString();
                CostBlockMaterial.Text = AppData.context.Material.Where(i => i.Name == txt).Select(j => j.Cost).FirstOrDefault().ToString();
                UnitBlockMaterial.Text = AppData.context.Material.Where(i => i.Name == txt).Select(j => j.unit).FirstOrDefault();
                if (QuantityBoxMaterial.Text != "")
                    SumCostMaterial.Text = (Int32.Parse(CostBlockMaterial.Text) * Int32.Parse(QuantityBoxMaterial.Text)).ToString();
                else
                    QuantityBoxMaterial.BorderThickness = new Thickness(3);
            }
                
        }

        private void QuantityBoxMaterial_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameBoxMaterial.SelectedIndex != -1 && QuantityBoxMaterial.Text != "")
            {
                SumCostMaterial.Text = (Int32.Parse(CostBlockMaterial.Text) * Int32.Parse(QuantityBoxMaterial.Text)).ToString();
                QuantityBoxMaterial.BorderThickness = new Thickness(0);
            }
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            ListMaterial.Visibility = Visibility.Hidden;
            AddMaterial.Visibility = Visibility.Hidden;
            DeleteMaterial.Visibility = Visibility.Hidden;
            NameBoxMaterial.Visibility = Visibility.Visible;
            CostTextMaterial.Visibility = Visibility.Visible;
            CostBlockMaterial.Visibility = Visibility.Visible;
            UnitTextMaterial.Visibility = Visibility.Visible;
            UnitBlockMaterial.Visibility = Visibility.Visible;
            QuantityTextMaterial.Visibility = Visibility.Visible;
            QuantityBoxMaterial.Visibility = Visibility.Visible;
            SumCostTextMaterial.Visibility = Visibility.Visible;
            SumCostMaterial.Visibility = Visibility.Visible;
            AddServiceListMaterial.Visibility = Visibility.Visible;
            CancelMaterial.Visibility = Visibility.Visible;
        }

        private void CancelMaterial_Click(object sender, RoutedEventArgs e)
        {
            ListMaterial.Visibility = Visibility.Visible;
            AddMaterial.Visibility = Visibility.Visible;
            DeleteMaterial.Visibility = Visibility.Visible;
            NameBoxMaterial.Visibility = Visibility.Hidden;
            CostTextMaterial.Visibility = Visibility.Hidden;
            CostBlockMaterial.Visibility = Visibility.Hidden;
            UnitTextMaterial.Visibility = Visibility.Hidden;
            UnitBlockMaterial.Visibility = Visibility.Hidden;
            QuantityTextMaterial.Visibility = Visibility.Hidden;
            QuantityBoxMaterial.Visibility = Visibility.Hidden;
            SumCostTextMaterial.Visibility = Visibility.Hidden;
            SumCostMaterial.Visibility = Visibility.Hidden;
            AddServiceListMaterial.Visibility = Visibility.Hidden;
            CancelMaterial.Visibility = Visibility.Hidden;
        }

        private void AddServiceListMaterial_Click(object sender, RoutedEventArgs e)
        {
            if(QuantityBoxMaterial.Text == "")
                QuantityBoxMaterial.BorderThickness = new Thickness(3);
            else
            {

                ViewMaterial view = new ViewMaterial();
                view.Name = NameBoxMaterial.Text;
                view.Cost = Int32.Parse(CostBlockMaterial.Text);
                view.Unit = UnitBlockMaterial.Text;
                view.Quantity = Int32.Parse(QuantityBoxMaterial.Text);
                view.Sum = Int32.Parse(SumCostMaterial.Text);

                if (view.Quantity > 0)
                {
                    QuantityBoxMaterial.BorderThickness = new Thickness(0);

                    NameBoxMaterial.Items.Remove(view.Name);
                    NameBoxMaterial.SelectedIndex = 0;
                    if (NameBoxMaterial.Items.Count == 0)
                        AddServiceListMaterial.IsEnabled = false;


                    ListMaterial.Items.Add(view);
                    viewMaterials.Add(view);
                    CancelMaterial_Click(sender, e);
                    SumAllText();
                }
                else
                    QuantityBoxMaterial.BorderThickness = new Thickness(3);
            }
        }

        private void DeleteMaterial_Click(object sender, RoutedEventArgs e)
        {
            ViewMaterial view = (ViewMaterial)ListMaterial.SelectedItem;
            if (view != null)
            {
                NameBoxMaterial.Items.Add(view.Name);
                if (NameBoxMaterial.Items.Count != 0)
                    AddServiceListMaterial.IsEnabled = true;
                ListMaterial.Items.Remove(view);
                viewMaterials.Remove(view);
                SumAllText();
            }
        }
        public void SumAllText()
        {
            int sum = 0;
            for (int i = 0; i < viewMaterials.Count; i++)
            {
                sum += viewMaterials[i].Sum;
            }
            for (int i = 0; i < viewServises.Count; i++)
            {
                sum += viewServises[i].Sum;
            }
            SumAll.Text = sum.ToString();
        }

        private void OrderFinish_Click(object sender, RoutedEventArgs e)
        {
            Emptiness();
            if (ObjectBox.Text != "" && AdressBox.Text != "" && TypeObjectBox.Text != "" && BrigadeBox.Text != "" && viewServises.Count != 0 && viewMaterials.Count != 0 && DateBox.Text != "")
            {
                Entity.Object newobject = AppData.context.Object.Add(new Entity.Object()
                {
                    Name = ObjectBox.Text,
                    Address = AdressBox.Text,
                    idObjectType = AppData.context.objectType.Where(i => i.Type == TypeObjectBox.Text).Select(j => j.idObjectType).FirstOrDefault(),
                });

                Order order = AppData.context.Order.Add(new Order()
                {
                    idObject = newobject.idObject,
                    idBrigade = AppData.context.Brigade.Where(i => i.Name == BrigadeBox.Text).Select(j => j.idBrigade).FirstOrDefault(),
                    From = DateTime.Now,
                    To = Convert.ToDateTime(DateBox.SelectedDate),
                });

                for (int i = 0; i < viewMaterials.Count(); i++)
                {
                    string NameM = viewMaterials[i].Name;
                    UseMaterial useMaterial = AppData.context.UseMaterial.Add(new UseMaterial()
                    {
                        idOrder = order.idOrder,
                        idMaterial = AppData.context.Material.Where(j => j.Name == NameM).Select(c => c.idMaterial).FirstOrDefault(),
                        Quantity = viewMaterials[i].Quantity
                    });  
                }

                for (int i = 0; i < viewServises.Count(); i++)
                {
                    string NameS = viewServises[i].Name;
                    ServiceOrder serviceOrder = AppData.context.ServiceOrder.Add(new ServiceOrder()
                    {
                        idOrder = order.idOrder,
                        idService = AppData.context.Service.Where(j => j.Name == NameS).Select(c => c.idService).FirstOrDefault(),
                        Quantity = viewServises[i].Quantity
                    });
                }
                AppData.context.SaveChanges();
                MessageBox.Show("Заказ сформирован!");
                this.NavigationService.Navigate(new OrderPages.OrderPage());
            }
        }
        void Emptiness()
        {
            if (ObjectBox.Text == "")
                ObjectBox.BorderBrush = Brushes.Red;
            else
                ObjectBox.BorderBrush = Brushes.LightSlateGray;
            if (AdressBox.Text == "")
                AdressBox.BorderBrush = Brushes.Red;
            else
                AdressBox.BorderBrush = Brushes.LightSlateGray;
            if (TypeObjectBox.Text == "")
                TypeObjectBorder.BorderBrush = Brushes.Red;
            else
                TypeObjectBorder.BorderBrush = Brushes.LightSlateGray;
            if (BrigadeBox.Text == "")
                BrigadeBorder.BorderBrush = Brushes.Red;
            else
                BrigadeBorder.BorderBrush = Brushes.LightSlateGray;
            if (viewServises.Count == 0)
                ListServisBorder.BorderBrush = Brushes.Red;
            else
                ListServisBorder.BorderBrush = Brushes.Transparent;
            if(viewMaterials.Count == 0)
                ListMaterialBorder.BorderBrush = Brushes.Red;
            else
                ListMaterialBorder.BorderBrush = Brushes.Transparent;
            if (DateBox.Text == "")
                DateBox.BorderBrush = Brushes.Red;
            else
                DateBox.BorderBrush = Brushes.LightSlateGray;
        }

        
    }

    class ViewServise
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public int Sum { get; set; }
    }
    class ViewMaterial
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public int Sum { get; set; }
    }
}
