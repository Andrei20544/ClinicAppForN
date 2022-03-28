using ClinicApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClinicApp.Views
{
    public partial class ScheduleWindow : Window
    {
        private List<DopSchedule> schedule = new List<DopSchedule>();

        private static ScheduleWindow instance;
        public ScheduleWindow()
        {
            InitializeComponent();

            SetComboBox();
            Update();
        }

        public static ScheduleWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new ScheduleWindow();
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
                var doctors = from d in model.Doctor
                              select new
                              {
                                  nameDoctor = d.name + " " + d.middle_name + "(id: " + d.id_doctor + " )" + "( " + d.role + " )"
                              };

                foreach (var item in doctors)
                {
                    nameDoctorCombo.Items.Add(item.nameDoctor);
                }
            }
        }

        private void Update()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    schedule.Clear();
                    GridClient.SelectedItem = null;
                    GridClient.ItemsSource = null;

                    var query = from s in model.Schedule
                                join d in model.Doctor on s.id_doctor equals d.id_doctor
                                select new
                                {
                                    ID = s.id_schedule,
                                    Monday = s.momday,
                                    Tuesday = s.tuesday,
                                    Wednesday = s.wednesday,
                                    Thursday = s.thursday,
                                    Friday = s.friday,
                                    Sarueday = s.saturday,
                                    Sunday = s.sunday,
                                    DoctorName = d.name + " " + d.middle_name + "(id: " + d.id_doctor + " )" + "( " + d.role + " )"
                                };

                    foreach (var item in query)
                    {
                        DopSchedule scheduleD = new DopSchedule(item.ID, item.Monday, item.Tuesday, item.Wednesday,
                            item.Thursday, item.Friday, item.Sarueday, item.Sunday, item.DoctorName);
                        schedule.Add(scheduleD);
                    }

                    GridClient.ItemsSource = schedule;
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
                using (ModelBD model = new ModelBD())
                {
                    var DoctorID = nameDoctorCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", "");

                    Schedule schedule = new Schedule
                    {
                        id_doctor = int.Parse(DoctorID),
                        momday = mondayText.Text,
                        tuesday = tuesdayText.Text,
                        wednesday = wednesdayText.Text,
                        thursday = thursdayText.Text,
                        friday = fridayText.Text,
                        saturday = satuedayText.Text,
                        sunday = sundayText.Text
                    };

                    model.Schedule.Add(schedule);
                    model.SaveChanges();

                    MessageBox.Show("Расписание успешно добавлен");

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
                var selected = GridClient.SelectedItem as DopSchedule;

                using (ModelBD model = new ModelBD())
                {
                    var DoctorID = nameDoctorCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", "");

                    Schedule schedule = model.Schedule.Where(n => n.id_schedule.Equals(selected.ID)).FirstOrDefault();
                    schedule.id_doctor = int.Parse(DoctorID);
                    schedule.momday = mondayText.Text;
                    schedule.tuesday = tuesdayText.Text;
                    schedule.wednesday = wednesdayText.Text;
                    schedule.thursday = thursdayText.Text;
                    schedule.friday = fridayText.Text;
                    schedule.saturday = satuedayText.Text;
                    schedule.sunday = sundayText.Text;

                    model.Entry(schedule).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Расписание успешно изменен");

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
                var selected = GridClient.SelectedItem as DopSchedule;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Schedule schedule = model.Schedule.Where(n => n.id_schedule.Equals(selected.ID)).FirstOrDefault();
                        model.Schedule.Remove(schedule);
                        model.SaveChanges();

                        MessageBox.Show("Расписание успешно удален");
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

        private void GridClient_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (schedule.Count != 0)
            {
                var selected = GridClient.SelectedItem as DopSchedule;

                if (selected != null)
                {
                    try
                    {
                        nameDoctorCombo.SelectedItem = selected.DoctorName;
                        mondayText.Text = selected.Monday;
                        tuesdayText.Text = selected.Tuesday;
                        wednesdayText.Text = selected.Wednesday;
                        thursdayText.Text = selected.Thursday;
                        fridayText.Text = selected.Friday;
                        satuedayText.Text = selected.Saturday;
                        sundayText.Text = selected.Sunday;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ClearTextBox();
            GridClient.SelectedItem = null;
            try
            {
                List<DopSchedule> items = new List<DopSchedule>();
                foreach (DopSchedule item in schedule)
                {
                    if (item.DoctorName.Contains(search.Text))
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
            nameDoctorCombo.SelectedItem = null;
            mondayText.Text = "";
            tuesdayText.Text = "";
            wednesdayText.Text = "";
            thursdayText.Text = "";
            fridayText.Text = "";
            satuedayText.Text = "";
            sundayText.Text = "";
        }
    }
}
