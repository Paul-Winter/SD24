using System;
using System.Data;
using System.Windows;
using Npgsql;

namespace TeacherScheduleApp
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Host=localhost;Port=5432;Database=schedule_db;Username=postgres;Password=0000;";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            s.id AS ""ID"", 
                            t.full_name AS ""Преподаватель"", 
                            sub.name AS ""Предмет"", 
                            s.auditorium_number AS ""Аудитория"", 
                            s.day_of_week AS ""День"", 
                            s.start_time AS ""Начало"", 
                            s.status AS ""Статус"", 
                            s.hours_count AS ""Часы""
                        FROM schedule s
                        JOIN teachers t ON s.teacher_id = t.id
                        JOIN subjects sub ON s.subject_id = sub.id
                        ORDER BY s.day_of_week, s.start_time;";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ScheduleDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке: " + ex.Message);
            }
        }

        private void btnMarkDone_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Сначала выбери пару в таблице!");
                return;
            }
            DataRowView row = (DataRowView)ScheduleDataGrid.SelectedItem;
            int scheduleId = Convert.ToInt32(row["ID"]);
            ChangeStatus(scheduleId, "проведено");
        }

        private void btnMarkCancelled_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Сначала выбери пару в таблице!");
                return;
            }
            DataRowView row = (DataRowView)ScheduleDataGrid.SelectedItem;
            int scheduleId = Convert.ToInt32(row["ID"]);
            ChangeStatus(scheduleId, "отменено");
        }

        private void ChangeStatus(int id, string newStatus)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE schedule SET status = @status WHERE id = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("status", newStatus);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении: " + ex.Message);
            }
        }
    }
}