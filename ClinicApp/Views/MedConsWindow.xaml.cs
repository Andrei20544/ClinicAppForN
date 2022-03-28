using ClinicApp.ViewModel;
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
using System.Windows.Shapes;

namespace ClinicApp.Views
{
    public partial class MedConsWindow : Window
    {
        private List<DopMedConsumption> medConsumptions = new List<DopMedConsumption>();

        private static MedConsWindow instance;
        public MedConsWindow()
        {
            InitializeComponent();

            Update();
            SetComboBox();
        }

        public static MedConsWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new MedConsWindow();
            }
            return instance;
        }

        public static void NullInst()
        {
            instance = null;
        }

        private void SetComboBox()
        {
            using (ModelBD model = new ModelBD())
            {
                var services = from s in model.Services
                               select new
                               {
                                   nameService = s.name + "(id: " + s.id_service + " )"
                               };

                var pills = from c in model.Pills
                              select new
                              {
                                  namePill = c.name + "(id: " + c.id_pills + " )"
                              };

                foreach (var item in services.Distinct())
                {
                    nameServicesCombo.Items.Add(item.nameService);
                }
                foreach (var item in pills)
                {
                    namePillsCombo.Items.Add(item.namePill);
                }
            }
        }

        private void Update()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    medConsumptions.Clear();
                    GridClient.SelectedItem = null;
                    GridClient.ItemsSource = null;

                    var querry = from m in model.Medication_consumption
                                 join s in model.Services on m.id_services equals s.id_service
                                 join p in model.Pills on m.id_pills equals p.id_pills
                                 select new
                                 {
                                     id = m.id,
                                     serviceName = s.name,
                                     pillName = p.name,
                                     cost = m.cost,
                                     qty = m.qty,
                                     idService = s.id_service,
                                     idPill = p.id_pills,
                                     Date = m.date
                                 };

                    foreach (var item in querry)
                    {
                        DopMedConsumption dopMedConsumption = new DopMedConsumption(item.id, item.qty, item.cost, item.pillName, item.serviceName, item.idService, item.idPill, item.Date);
                        medConsumptions.Add(dopMedConsumption);
                    }

                    GridClient.ItemsSource = medConsumptions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {
                    var ServiceID = int.Parse(nameServicesCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", ""));
                    var PillID = int.Parse(namePillsCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", ""));


                    var pill = model.Pills.Where(p => p.id_pills.Equals(PillID)).FirstOrDefault();
                    var service = model.Services.Where(p => p.id_service.Equals(ServiceID)).FirstOrDefault();

                    Medication_consumption medication_ = new Medication_consumption
                    {
                        id_pills = pill.id_pills,
                        id_services = service.id_service,
                        cost = int.Parse(costText.Text),
                        qty = int.Parse(qtyText.Text),
                        date = Date.DisplayDate
                    };

                    model.Medication_consumption.Add(medication_);
                    model.SaveChanges();

                    MessageBox.Show("Запись успешно добавлена");

                    Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возможно не все поля были заполненны!");
            }
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as DopMedConsumption;

                using (ModelBD model = new ModelBD())
                {
                    var ServiceID = int.Parse(nameServicesCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", ""));
                    var PillID = int.Parse(namePillsCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", ""));


                    var pill = model.Pills.Where(p => p.id_pills.Equals(PillID)).FirstOrDefault();
                    var service = model.Services.Where(p => p.id_service.Equals(ServiceID)).FirstOrDefault();

                    Medication_consumption medication = model.Medication_consumption.Where(n => n.id.Equals(selected.ID)).FirstOrDefault();
                    medication.id_pills = pill.id_pills;
                    medication.id_services = service.id_service;
                    medication.qty = int.Parse(qtyText.Text);
                    medication.cost = int.Parse(costText.Text);
                    medication.date = Date.DisplayDate;

                    model.Entry(medication).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Запись успешно изменена");

                    Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возможно не все поля были заполненны!");
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as DopMedConsumption;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Medication_consumption register = model.Medication_consumption.Where(n => n.id.Equals(selected.ID)).FirstOrDefault();
                        model.Medication_consumption.Remove(register);
                        model.SaveChanges();

                        MessageBox.Show("Запись успешно удалена");
                        ClearTextBox();

                        Update();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NullInst();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void GridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (medConsumptions.Count != 0)
            {
                var selected = GridClient.SelectedItem as DopMedConsumption;

                try
                {
                    if (selected != null)
                    {
                        namePillsCombo.SelectedItem = selected.Pill + "(id: " + selected.IDPill + " )";
                        nameServicesCombo.SelectedItem = selected.Service + "(id: " + selected.IDService + " )";
                        qtyText.Text = selected.QTY.ToString();
                        costText.Text = selected.Cost.ToString();
                        Date.SelectedDate = selected.Date;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearTextBox();
            GridClient.SelectedItem = null;
            try
            {
                List<DopMedConsumption> items = new List<DopMedConsumption>();
                foreach (DopMedConsumption item in medConsumptions)
                {
                    if (item.Pill.Contains(search.Text) || item.Service.Contains(search.Text))
                    {
                        items.Add(item);
                    }
                }
                GridClient.ItemsSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextBox()
        {
            namePillsCombo.SelectedItem = null;
            nameServicesCombo.SelectedItem = null;
            qtyText.Text = "";
            costText.Text = "";
            Date.SelectedDate = null;
        }

        private void ComputingCost()
        {
            using (ModelBD model = new ModelBD())
            {
                if (namePillsCombo.SelectedItem != null && qtyText.Text != "")
                {
                    try
                    {
                        var idPill = int.Parse(namePillsCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", ""));
                        var pill = model.Pills.Where(p => p.id_pills.Equals(idPill)).FirstOrDefault();

                        var cost = (int)pill.price * int.Parse(qtyText.Text);

                        costText.Text = cost.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void qtyText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComputingCost();
        }

        private void namePillsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComputingCost();
        }

        private void qtyText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void costText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }
    }
}
