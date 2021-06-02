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
    /// Логика взаимодействия для AddBrigadePage.xaml
    /// </summary>
    public partial class AddBrigadePage : Page
    {
        List<workerClass> workerClasses = new List<workerClass>();
        List<int> idWorker = AppData.context.Worker.Select(i => i.idWorker).ToList();
        List<String> NameWorker = AppData.context.Worker.Select(i => i.Name).ToList();
        List<String> SurNameWorker = AppData.context.Worker.Select(i => i.Surname).ToList();
        List<String> PatronymicWorker = AppData.context.Worker.Select(i => i.Patronymic).ToList();
        List<String> SpecialityWorker = AppData.context.WorkerView.Select(i => i.Expr1).ToList();
        public AddBrigadePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> FIO = new List<string>();
            List<String> FIOBrigadier = new List<string>();
            for (int i = 0; i < SpecialityWorker.Count; i++)
            {
                if (SpecialityWorker[i].Contains("Бригадир"))
                    FIOBrigadier.Add(idWorker[i] + "  " + NameWorker[i] + ' ' + SurNameWorker[i] + ' ' + PatronymicWorker[i] + " (" + SpecialityWorker[i] + ')');
                else
                    FIO.Add(idWorker[i] + "  " + NameWorker[i] + ' ' + SurNameWorker[i] + ' ' + PatronymicWorker[i] + " (" + SpecialityWorker[i] + ')');
            }
            foreach (var item in FIO)
                WorkerBox.Items.Add(item);
            foreach (var item in FIOBrigadier)
                BrigadierBox.Items.Add(item);
            WorkerBox.SelectedIndex = 0;
        }

        private void BrigadeFinish_Click(object sender, RoutedEventArgs e)
        {
            Emptiness();
            if (NameBox.Text != "" && BrigadierBox.Text != "" && workerClasses.Count != 0)
            {
                String[] split = BrigadierBox.Text.Split(' ');
                int id = Int32.Parse(split[0]);

                Brigade brigade = AppData.context.Brigade.Add(new Brigade()
                {
                    Name = NameBox.Text,
                    idBrigadier = id
                });
                for (int i = 0; i < workerClasses.Count; i++)
                {
                    WorkersBrigade workersBrigade = AppData.context.WorkersBrigade.Add(new WorkersBrigade()
                    {
                        idBrigade = brigade.idBrigade,
                        idWorker = workerClasses[i].idWorker
                    });
                }
                AppData.context.SaveChanges();
                MessageBox.Show("Бригада создана!");
                this.NavigationService.Navigate(new BrigadePages.BrigadePage());
            }
        }

        private void AddWokerList_Click(object sender, RoutedEventArgs e)
        {
                String[] split = WorkerBox.Text.Split(' ');
                int id = Int32.Parse(split[0]);

                workerClass worker = new workerClass();
                worker.idWorker = id;
                worker.Name = AppData.context.Worker.Where(i => i.idWorker == id).Select(j => j.Name).FirstOrDefault();
                worker.SurName = AppData.context.Worker.Where(i => i.idWorker == id).Select(j => j.Surname).FirstOrDefault();
                worker.Patronymic = AppData.context.Worker.Where(i => i.idWorker == id).Select(j => j.Patronymic).FirstOrDefault();
                worker.Speciality = AppData.context.WorkerView.Where(i => i.idWorker == id).Select(j => j.Expr1).FirstOrDefault();

                workerClasses.Add(worker);
                ListWoker.Items.Add(worker);

                WorkerBox.Items.Remove(WorkerBox.Text);
                WorkerBox.SelectedIndex = 0;
                if (WorkerBox.Items.Count == 0)
                    AddWokerList.IsEnabled = false;
                CancelWoker_Click(sender, e);
            
        }

        private void CancelWoker_Click(object sender, RoutedEventArgs e)
        {
            ListWoker.Visibility = Visibility.Visible;
            AddWoker.Visibility = Visibility.Visible;
            DeleteWoker.Visibility = Visibility.Visible;
            WorkerText.Visibility = Visibility.Hidden;
            WorkerBox.Visibility = Visibility.Hidden;
            AddWokerList.Visibility = Visibility.Hidden;
            CancelWoker.Visibility = Visibility.Hidden;
        }

        private void AddWoker_Click(object sender, RoutedEventArgs e)
        {
            ListWoker.Visibility = Visibility.Hidden;
            AddWoker.Visibility = Visibility.Hidden;
            DeleteWoker.Visibility = Visibility.Hidden;
            WorkerText.Visibility = Visibility.Visible;
            WorkerBox.Visibility = Visibility.Visible;
            AddWokerList.Visibility = Visibility.Visible;
            CancelWoker.Visibility = Visibility.Visible;
        }

        private void DeleteWoker_Click(object sender, RoutedEventArgs e)
        {
            workerClass workerClass = (workerClass)ListWoker.SelectedItem;
            WorkerBox.Items.Add(workerClass.idWorker + "  " + workerClass.Name + ' ' + workerClass.SurName + ' ' + workerClass.Patronymic + " (" + workerClass.Speciality + ')');
            if (WorkerBox.Items.Count != 0)
                AddWokerList.IsEnabled = true;
            workerClasses.Remove(workerClass);
            ListWoker.Items.Remove(workerClass);
        }
        void Emptiness()
        {
            if (NameBox.Text == "")
                NameBox.BorderBrush = Brushes.Red;
            else
                NameBox.BorderBrush = Brushes.LightSlateGray;
            if (BrigadierBox.Text == "")
                BrigadierBoxBorder.BorderBrush = Brushes.Red;
            else
                BrigadierBoxBorder.BorderBrush = Brushes.LightSlateGray;
            if (workerClasses.Count == 0)
                ListWokerBorder.BorderBrush = Brushes.Red;
            else
                ListWokerBorder.BorderBrush = Brushes.Transparent;
        }
        private void IsDigitIsLetter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && !e.Text.Contains(' ') && !Char.IsLetter(e.Text, 0))
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
    class workerClass
    {
        public int idWorker { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public string Speciality { get; set; }

    }
}
