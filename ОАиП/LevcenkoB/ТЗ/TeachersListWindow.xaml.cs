using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Npgsql;

namespace TeacherScheduleApp
{
    public partial class TeachersListWindow : Window
    {
        private string connectionString = "Host=localhost;Port=5432;Database=schedule_db;Username=postgres;Password=0000;";

        public TeachersListWindow()
        {
            InitializeComponent();
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            id AS ""ID"", 
                            full_name AS ""ФИО"", 
                            experience_years || ' лет' AS ""Стаж"", 
                            specialization AS ""Специализация""
                        FROM teachers
                        ORDER BY full_name;";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    TeachersGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке списка: " + ex.Message);
            }
        }

        private void TeachersGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TeachersGrid.SelectedItem == null) return;

            DataRowView row = (DataRowView)TeachersGrid.SelectedItem;
            int teacherId = Convert.ToInt32(row["ID"]);

            TeacherWindow profileWindow = new TeacherWindow(teacherId);
            profileWindow.ShowDialog();
        }
    }
}