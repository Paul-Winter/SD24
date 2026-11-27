using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
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
                            t.id AS ""TeacherID"",
                            t.full_name AS ""Преподаватель"", 
                            sub.name AS ""Предмет"",
                            g.name AS ""Группа"",
                            s.auditorium_number AS ""Аудитория"", 
                            s.lesson_date AS ""Дата"",
                            TO_CHAR(s.start_time, 'HH24:MI') || ' - ' || TO_CHAR(s.end_time, 'HH24:MI') AS ""Время"",
                            s.status AS ""Статус"", 
                            s.hours_count AS ""Часы""
                        FROM schedule s
                        JOIN teachers t ON s.teacher_id = t.id
                        JOIN subjects sub ON s.subject_id = sub.id
                        JOIN groups g ON s.group_id = g.id
                        ORDER BY s.lesson_date, s.start_time;";

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

        private void btnOpenTeachers_Click(object sender, RoutedEventArgs e)
        {
            TeachersListWindow listWindow = new TeachersListWindow();
            listWindow.ShowDialog();
        }

        private void btnMarkAll_Click(object sender, RoutedEventArgs e)
        {
            ChangeAllStatus("проведено");
        }

        private void btnCancelAll_Click(object sender, RoutedEventArgs e)
        {
            ChangeAllStatus("отменено");
        }

        private void btnRowInfo_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = btn.DataContext as DataRowView;
            if (row != null)
            {
                int teacherId = Convert.ToInt32(row["TeacherID"]);
                TeacherWindow infoWindow = new TeacherWindow(teacherId);
                infoWindow.ShowDialog();
            }
        }

        private void btnRowDone_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = btn.DataContext as DataRowView;
            if (row != null)
            {
                int scheduleId = Convert.ToInt32(row["ID"]);
                ChangeStatus(scheduleId, "проведено");
            }
        }

        private void btnRowCancel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = btn.DataContext as DataRowView;
            if (row != null)
            {
                int scheduleId = Convert.ToInt32(row["ID"]);
                ChangeStatus(scheduleId, "отменено");
            }
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

        private void ChangeAllStatus(string newStatus)
        {
            if (MessageBox.Show("Вы уверены, что хотите изменить статус у ВСЕХ пар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        string sql = "UPDATE schedule SET status = @status";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("status", newStatus);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при массовом обновлении: " + ex.Message);
                }
            }
        }

        private void btnLoadWorkload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            t.full_name AS ""Преподаватель"",
                            COUNT(s.id) AS ""Всего пар"",
                            COALESCE(SUM(s.hours_count), 0) AS ""Всего часов"",
                            COUNT(CASE WHEN s.status = 'проведено' THEN 1 END) AS ""Проведено пар"",
                            COALESCE(SUM(CASE WHEN s.status = 'проведено' THEN s.hours_count ELSE 0 END), 0) AS ""Отработано часов""
                        FROM teachers t
                        LEFT JOIN schedule s ON t.id = s.teacher_id
                        GROUP BY t.id, t.full_name
                        ORDER BY ""Отработано часов"" DESC;";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    WorkloadDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке загруженности: " + ex.Message);
            }
        }
    }
}