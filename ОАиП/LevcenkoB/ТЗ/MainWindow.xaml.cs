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
            LoadAssessments();
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
                            s.lesson_type AS ""Тип"",
                            g.name AS ""Группа"",
                            s.lesson_date AS ""Дата"",
                            TO_CHAR(s.start_time, 'HH24:MI') || ' - ' || TO_CHAR(s.end_time, 'HH24:MI') AS ""Время"",
                            s.status AS ""Статус""
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

        private void LoadAssessments()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            s.lesson_date AS ""Дата"",
                            g.name AS ""Группа"",
                            sub.name AS ""Дисциплина"",
                            sub.assessment_type AS ""Тип""
                        FROM schedule s
                        JOIN groups g ON s.group_id = g.id
                        JOIN subjects sub ON s.subject_id = sub.id
                        WHERE sub.assessment_type IN ('Зачёт', 'Экзамен')
                        ORDER BY s.lesson_date;";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    AssessmentsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке зачётов: " + ex.Message);
            }
        }

        private void btnRowDone_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                int scheduleId = Convert.ToInt32(btn.Tag);
                ChangeStatus(scheduleId, "проведено");
            }
        }

        private void btnRowCancel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                int scheduleId = Convert.ToInt32(btn.Tag);
                ChangeStatus(scheduleId, "отменено");
            }
        }

        private void btnMarkAll_Click(object sender, RoutedEventArgs e)
        {
            ChangeAllStatus("проведено");
        }

        private void btnCancelAll_Click(object sender, RoutedEventArgs e)
        {
            ChangeAllStatus("отменено");
        }

        private void ChangeStatus(int id, string newStatus)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE schedule SET status = @status WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("status", newStatus);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
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
                        using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE schedule SET status = @status", conn))
                        {
                            cmd.Parameters.AddWithValue("status", newStatus);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
            }
        }

        private void btnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherEditWindow editWindow = new TeacherEditWindow(0);
            editWindow.ShowDialog();
            LoadData();
        }

        private void btnOpenTeachers_Click(object sender, RoutedEventArgs e)
        {
            TeachersListWindow listWindow = new TeachersListWindow();
            listWindow.ShowDialog();
        }

        private void btnOpenGroups_Click(object sender, RoutedEventArgs e)
        {
            GroupsWindow groupsWindow = new GroupsWindow();
            groupsWindow.ShowDialog();
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
                            COALESCE(SUM(s.hours_count), 0) AS ""Всего часов""
                        FROM teachers t
                        LEFT JOIN schedule s ON t.id = s.teacher_id
                        GROUP BY t.id, t.full_name
                        ORDER BY ""Всего часов"" DESC;";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    WorkloadDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }
    }
}