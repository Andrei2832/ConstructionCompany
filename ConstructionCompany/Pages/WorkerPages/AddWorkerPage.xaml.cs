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
    /// Логика взаимодействия для AddWorkerPage.xaml
    /// </summary>
    public partial class AddWorkerPage : Page
    {
        List<string> spesboxlist = new List<string>();
        List<SpecialtyClass> speslist = new List<SpecialtyClass>();
        public AddWorkerPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            spesboxlist.AddRange(AppData.context.Specialty.Select(i => i.Name).ToList());
            foreach (var item in spesboxlist)
                SpesBox.Items.Add(item);
            SpesBox.SelectedIndex = 0;

            DateBirthBox.SelectedDate = DateTime.Now.AddYears(-18);

        }

        private void WorkerFinish_Click(object sender, RoutedEventArgs e)
        {
            Emptiness();
            if(SurnameBox.Text != "" && NameBox.Text != "" && PatronymicBox.Text != "" && DateBirthBox.Text != "" && TelephonBox.Text != "" && speslist.Count() != 0)
            {
                Worker worker = AppData.context.Worker.Add(new Worker()
                {
                    Surname = SurnameBox.Text,
                    Name = NameBox.Text,
                    Patronymic = PatronymicBox.Text,
                    DateBirth = (DateTime)DateBirthBox.SelectedDate,
                    Telephone = TelephonBox.Text,    
                    
                });
                for (int i = 0; i < speslist.Count; i++)
                {
                    string specName = speslist[i].Name;
                    specialtiesWorkers specialtiesWorkers = AppData.context.specialtiesWorkers.Add(new specialtiesWorkers()
                    {
                        idWorker = worker.idWorker,
                        idSpecialty = AppData.context.Specialty.Where(j => j.Name == specName).Select(c => c.idSpecialty).FirstOrDefault()
                    });
                }
                AppData.context.SaveChanges();
                MessageBox.Show("Рабочий добавлен!");
                this.NavigationService.Navigate(new WorkerPages.WorkerPage());
            }
        }

        private void AddSpecialtyList_Click(object sender, RoutedEventArgs e)
        {
            SpecialtyClass specialtyClass = new SpecialtyClass();
            specialtyClass.Name = SpesBox.Text;

            ListSpecialty.Items.Add(specialtyClass);
            speslist.Add(specialtyClass);

            SpesBox.Items.Remove(SpesBox.Text);
            if (SpesBox.Items.Count == 0)
                AddSpecialtyList.IsEnabled = false;
            SpesBox.SelectedIndex = 0;

            CancelSpecialty_Click(sender, e);
        }

        private void CancelSpecialty_Click(object sender, RoutedEventArgs e)
        {
            ListSpecialty.Visibility = Visibility.Visible;
            AddSpecialty.Visibility = Visibility.Visible;
            DeleteSpecialty.Visibility = Visibility.Visible;
            SpesBlock.Visibility = Visibility.Hidden;
            SpesBox.Visibility = Visibility.Hidden;
            AddSpecialtyList.Visibility = Visibility.Hidden;
            CancelSpecialty.Visibility = Visibility.Hidden;
        }

        private void AddSpecialty_Click(object sender, RoutedEventArgs e)
        {
            ListSpecialty.Visibility = Visibility.Hidden;
            AddSpecialty.Visibility = Visibility.Hidden;
            DeleteSpecialty.Visibility = Visibility.Hidden;
            SpesBlock.Visibility = Visibility.Visible;
            SpesBox.Visibility = Visibility.Visible;
            AddSpecialtyList.Visibility = Visibility.Visible;
            CancelSpecialty.Visibility = Visibility.Visible;
        }

        void Emptiness()
        {
            if (SurnameBox.Text == "")
                SurnameBox.BorderBrush = Brushes.Red;
            else
                SurnameBox.BorderBrush = Brushes.LightSlateGray;
            if (NameBox.Text == "")
                NameBox.BorderBrush = Brushes.Red;
            else
                NameBox.BorderBrush = Brushes.LightSlateGray;
            if (PatronymicBox.Text == "")
                PatronymicBox.BorderBrush = Brushes.Red;
            else
                PatronymicBox.BorderBrush = Brushes.LightSlateGray;
            if (DateBirthBox.Text == "")
                DateBirthBox.BorderBrush = Brushes.Red;
            else
                DateBirthBox.BorderBrush = Brushes.LightSlateGray;
            if (TelephonBox.Text == "")
                TelephonBox.BorderBrush = Brushes.Red;
            else
                TelephonBox.BorderBrush = Brushes.LightSlateGray;
            if (speslist.Count() == 0)
                ListServisBorder.BorderBrush = Brushes.Red;
            else
                ListServisBorder.BorderBrush = Brushes.Transparent;
        }

        private void DeleteSpecialty_Click(object sender, RoutedEventArgs e)
        {
            SpecialtyClass specialtyClass = (SpecialtyClass)ListSpecialty.SelectedItem;
            if(specialtyClass != null)
            {
                SpesBox.Items.Add(specialtyClass.Name);
                if (SpesBox.Items.Count != 0)
                    AddSpecialtyList.IsEnabled = true;
                SpesBox.SelectedIndex = 0;
                ListSpecialty.Items.Remove(specialtyClass);
                speslist.Remove(specialtyClass);
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
    }
    class SpecialtyClass
    {
        public string Name { get; set; }
    }
}
