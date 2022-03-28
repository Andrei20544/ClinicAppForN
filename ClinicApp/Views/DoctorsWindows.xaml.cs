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
    public partial class DoctorsWindows : Window
    {
        private List<Doctor> doctors = new List<Doctor>();

        private static DoctorsWindows instance;
        public DoctorsWindows()
        {
            InitializeComponent();

            Update();

        }

        public static DoctorsWindows GetInstance()
        {
            if (instance == null)
            {
                instance = new DoctorsWindows();
            }
            return instance;
        }

        public static void NullInst()
        {
            instance = null;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Update()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    doctors.Clear();
                    GridDoctor.SelectedItem = null;
                    GridDoctor.ItemsSource = null;

                    var querry = from d in model.Doctor
                                 select d;

                    foreach (var item in querry)
                    {
                        doctors.Add(item);
                    }

                    GridDoctor.ItemsSource = doctors;
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
                var selected = GridDoctor.SelectedItem as Doctor;

                using (ModelBD model = new ModelBD())
                {
                    Doctor doctor = new Doctor
                    {
                        name = nameText.Text,
                        surname = surnameText.Text,
                        middle_name = middleNameText.Text,
                        role = roleText.Text,
                        gender = genderText.Text,

                    };

                    model.Doctor.Add(doctor);
                    model.SaveChanges();

                    MessageBox.Show("Сотрудник успешно добавлен");

                    Update();

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
                var selected = GridDoctor.SelectedItem as Doctor;

                using (ModelBD model = new ModelBD())
                {
                    Doctor doctor = model.Doctor.Where(n => n.id_doctor.Equals(selected.id_doctor)).FirstOrDefault();
                    doctor.name = nameText.Text;
                    doctor.surname = surnameText.Text;
                    doctor.middle_name = middleNameText.Text;
                    doctor.role = roleText.Text;
                    doctor.gender = genderText.Text;

                    model.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Сотрудник успешно изменен");

                    Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridDoctor.SelectedItem as Doctor;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Doctor doctor = model.Doctor.Where(n => n.id_doctor.Equals(selected.id_doctor)).FirstOrDefault();
                        model.Doctor.Remove(doctor);
                        model.SaveChanges();

                        MessageBox.Show("Сотрудник успешно удален");
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

        private void ClearTextBox()
        {
            nameText.Text = "";
            surnameText.Text = "";
            middleNameText.Text = "";
            roleText.Text = "";
            genderText.Text = "";
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

        private void GridDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doctors.Count != 0)
            {
                try
                {
                    var selected = GridDoctor.SelectedItem as Doctor;

                   if (selected != null)
                    {
                        nameText.Text = selected.name;
                        surnameText.Text = selected.surname;
                        middleNameText.Text = selected.middle_name;
                        roleText.Text = selected.role;
                        genderText.Text = selected.gender;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearTextBox();
            GridDoctor.SelectedItem = null;
            try
            {
                List<Doctor> items = new List<Doctor>();
                foreach (Doctor item in doctors)
                {
                    if (item.name.Contains(search.Text) || item.gender.Contains(search.Text) || item.middle_name.Contains(search.Text))
                    {
                        items.Add(item);
                    }
                }
                GridDoctor.ItemsSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
