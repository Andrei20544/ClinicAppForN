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
    public partial class Clients : Window
    {
        private List<Client> clients = new List<Client>();

        private static Clients instance;
        public Clients()
        {
            InitializeComponent();

            Update();

        }

        public static Clients GetInstance()
        {
            if (instance == null)
            {
                instance = new Clients();
            }
            return instance;
        }

        public static void NullInst()
        {
            instance = null;
        }

        private void Update()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    clients.Clear();
                    GridClient.SelectedItem = null;
                    GridClient.ItemsSource = null;

                    var querry = from c in model.Client
                                 select c;

                    foreach (var item in querry)
                    {
                        clients.Add(item);
                    }

                    GridClient.ItemsSource = clients;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NullInst();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clients.Count != 0)
            {
                try
                {
                    var selected = GridClient.SelectedItem as Client;

                    if (selected != null)
                    {
                        nameText.Text = selected.name;
                        surnameText.Text = selected.surname;
                        middleNameText.Text = selected.middle_name;
                        dateBirthText.Text = selected.birthday.ToString();
                        genderText.Text = selected.gender;
                        phoneText.Text = selected.phone;
                        emailText.Text = selected.email;
                        policyTypeText.Text = selected.type_policy;
                        policyNumText.Text = selected.policy_number;
                        passDataText.Text = selected.passport_data;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as Client;

                using (ModelBD model = new ModelBD())
                {
                    Client client = new Client
                    {
                        name = nameText.Text,
                        surname = surnameText.Text,
                        middle_name = middleNameText.Text,
                        birthday = DateTime.Parse(dateBirthText.Text),
                        gender = genderText.Text,
                        phone = phoneText.Text,
                        email = emailText.Text,
                        type_policy = policyTypeText.Text,
                        policy_number = policyNumText.Text,
                        passport_data = passDataText.Text
                    };

                    model.Client.Add(client);
                    model.SaveChanges();

                    MessageBox.Show("Клиент успешно добавлен");

                    Update();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as Client;

                using (ModelBD model = new ModelBD())
                {
                    Client client = model.Client.Where(n => n.id_client.Equals(selected.id_client)).FirstOrDefault();
                    client.name = nameText.Text;
                    client.surname = surnameText.Text;
                    client.middle_name = middleNameText.Text;
                    client.birthday = DateTime.Parse(dateBirthText.Text);
                    client.gender = genderText.Text;
                    client.phone = phoneText.Text;
                    client.email = emailText.Text;
                    client.type_policy = policyTypeText.Text;
                    client.policy_number = policyNumText.Text;
                    client.passport_data = passDataText.Text;

                    model.Entry(client).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Клиент успешно изменен");

                    Update();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as Client;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Client client = model.Client.Where(n => n.id_client.Equals(selected.id_client)).FirstOrDefault();
                        model.Client.Remove(client);
                        model.SaveChanges();

                        MessageBox.Show("Клиент успешно удален");
                        ClearTextBox();

                        Update();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextBox()
        {
            nameText.Text = "";
            surnameText.Text = "";
            middleNameText.Text = "";
            dateBirthText.Text = "";
            genderText.Text = "";
            phoneText.Text = "";
            emailText.Text = "";
            policyTypeText.Text = "";
            policyNumText.Text = "";
            passDataText.Text = "";
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearTextBox();
            GridClient.SelectedItem = null;
            try
            {
                List<Client> items = new List<Client>();
                foreach (Client item in clients)
                {
                    if (item.name.Contains(search.Text) || item.middle_name.Contains(search.Text) || item.gender.Contains(search.Text))
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

        private void phoneText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void policyNumText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void passDataText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }
    }
}
