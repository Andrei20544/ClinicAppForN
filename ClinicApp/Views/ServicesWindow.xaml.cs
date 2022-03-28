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
    public partial class ServicesWindow : Window
    {
        private List<DopServices> services = new List<DopServices>();

        private static ServicesWindow instance;
        public ServicesWindow()
        {
            InitializeComponent();

            SetComboBox();
            Update();

        }

        public static ServicesWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new ServicesWindow();
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
                var query = from p in model.Pills
                            select p;
                foreach (var item in query)
                {
                    idPillsText.Items.Add(item.name);
                }
            }
        }

        private void Update()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    services.Clear();
                    GridClient.SelectedItem = null;
                    GridClient.ItemsSource = null;

                    var querry = from s in model.Services
                                 join p in model.Pills on s.id_pills equals p.id_pills
                                 select new {
                                     ID = s.id_service,
                                     Name = s.name,
                                     Name_Pills = p.name,
                                     Limit_Age = s.limit_age,
                                     Value = s.value,
                                     Desc = s.description,
                                     qtyPills = s.qty_pills
                                 };

                    foreach (var item in querry)
                    {
                        DopServices dopServices = new DopServices(item.ID, item.Name, item.Name_Pills, item.Limit_Age, item.Value, item.Desc, item.qtyPills);
                        services.Add(dopServices);
                    }

                    GridClient.ItemsSource = services;
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

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as DopServices;

                using (ModelBD model = new ModelBD())
                {
                    var Id_pills = model.Pills.Where(p => p.name.Equals(idPillsText.SelectedItem.ToString())).FirstOrDefault();

                    Services services = new Services
                    {
                        name = nameText.Text,
                        id_pills = Id_pills.id_pills,
                        value = valueText.Text,
                        limit_age = int.Parse(limitText.Text),
                        description = descriptionText.Text,
                        qty_pills = int.Parse(qtyPills.Text)
                    };

                    model.Services.Add(services);
                    model.SaveChanges();

                    MessageBox.Show("Услуга успешно добавлен");

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
                var selected = GridClient.SelectedItem as DopServices;

                using (ModelBD model = new ModelBD())
                {
                    var Id_pills = model.Pills.Where(p => p.name.Equals(idPillsText.SelectedItem.ToString())).FirstOrDefault();

                    Services services = model.Services.Where(n => n.id_service.Equals(selected.ID)).FirstOrDefault();
                    services.name = nameText.Text;
                    services.limit_age = int.Parse(limitText.Text);
                    services.value = valueText.Text;
                    services.description = descriptionText.Text;
                    services.id_pills = Id_pills.id_pills;
                    services.qty_pills = int.Parse(qtyPills.Text);

                    model.Entry(services).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Услуга успешно изменен");

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
                var selected = GridClient.SelectedItem as DopServices;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Services services = model.Services.Where(n => n.id_service.Equals(selected.ID)).FirstOrDefault();
                        model.Services.Remove(services);
                        model.SaveChanges();

                        MessageBox.Show("Услуга успешно удален");
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

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearTextBox();
            GridClient.SelectedItem = null;
            try
            {
                List<DopServices> items = new List<DopServices>();
                foreach (DopServices item in services)
                {
                    if (item.NameServices.Contains(search.Text) || item.Description.Contains(search.Text))
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
            nameText.Text = "";
            limitText.Text = "";
            valueText.Text = "";
            descriptionText.Text = "";
            idPillsText.Text = "";
        }

        private void GridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (services.Count != 0)
            {
                try
                {
                    var selected = GridClient.SelectedItem as DopServices;

                    if (selected != null)
                    {
                        nameText.Text = selected.NameServices;
                        limitText.Text = selected.LimitAge.ToString();
                        valueText.Text = selected.Value;
                        descriptionText.Text = selected.Description;
                        idPillsText.SelectedItem = selected.NamePills;
                        qtyPills.Text = selected.QtyPills.ToString();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
