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
    public partial class PillsWindow : Window
    {
        private List<Pills> _pills_list = new List<Pills>();
        
        private static PillsWindow instance;
        private PillsWindow()
        {
            InitializeComponent();

            Update();

        }

        public static PillsWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new PillsWindow();
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

                    _pills_list.Clear();
                    GridClient.SelectedItem = null;
                    GridClient.ItemsSource = null;

                    var querry = from p in model.Pills
                                 select p;

                    foreach (var item in querry)
                    {
                        _pills_list.Add(item);
                    }

                    GridClient.ItemsSource = _pills_list;
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

        private void ClearTextBox()
        {
            nameText.Text = "";
            priceText.Text = "";
            manufacturerText.Text = "";
            conditionText.Text = "";
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as Pills;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Pills pills = model.Pills.Where(n => n.id_pills.Equals(selected.id_pills)).FirstOrDefault();
                        model.Pills.Remove(pills);
                        model.SaveChanges();

                        MessageBox.Show("Препарат успешно удален");
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

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as Pills;

                using (ModelBD model = new ModelBD())
                {
                    Pills pills = model.Pills.Where(n => n.id_pills.Equals(selected.id_pills)).FirstOrDefault();
                    pills.name = nameText.Text;
                    pills.price = int.Parse(priceText.Text);
                    pills.manufacturer = manufacturerText.Text;
                    pills.condition = conditionText.Text;
                    pills.qty = int.Parse(qtyText.Text);

                    model.Entry(pills).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Препарат успешно изменен");

                    Update();

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
                var selected = GridClient.SelectedItem as Pills;

                using (ModelBD model = new ModelBD())
                {
                    Pills pills = new Pills
                    {
                        name = nameText.Text,
                        price = int.Parse(priceText.Text),
                        manufacturer = manufacturerText.Text,
                        condition = conditionText.Text,
                        qty = int.Parse(qtyText.Text)
                    };

                    model.Pills.Add(pills);
                    model.SaveChanges();

                    MessageBox.Show("Препарат успешно добавлен");

                    Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_pills_list.Count != 0)
            {
                try
                {
                    var selected = GridClient.SelectedItem as Pills;

                    if (selected != null)
                    {
                        nameText.Text = selected.name;
                        priceText.Text = selected.price.ToString();
                        manufacturerText.Text = selected.manufacturer;
                        conditionText.Text = selected.condition;
                        qtyText.Text = selected.qty.ToString();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NullInst();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearTextBox();
            GridClient.SelectedItem = null;
            try
            {
                List<Pills> items = new List<Pills>();
                foreach (Pills item in _pills_list)
                {
                    if (item.name.Contains(search.Text) || item.condition.Contains(search.Text) || item.manufacturer.Contains(search.Text))
                    {
                        items.Add(item);
                    }
                }
                GridClient.ItemsSource = items;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void priceText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }
    }
}
