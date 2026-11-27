using System;
using System.Data;
using System.Windows;
using Npgsql;

namespace TeacherScheduleApp
{
    public partial class GroupsWindow : Window
    {
        private string connectionString = "Host=localhost;Port=5432;Database=schedule_db;Username=postgres;Password=0000;";

        public GroupsWindow()
        {
            InitializeComponent();
            LoadGroups();
        }

        private void LoadGroups()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT name AS \"Группа\", student_count AS \"Студентов\", course AS \"Курс\" FROM groups ORDER BY name", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GroupsGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void btnCalcLoad_Click(object sender, RoutedEventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        g.name AS ""Группа"",
                        sub.name AS ""Дисциплина"",
                        s.lesson_type AS ""Тип пары"",
                        SUM(s.hours_count) AS ""Всего часов за семестр"",
                        ROUND(SUM(s.hours_count) / 18, 1) AS ""Часов в неделю""
                    FROM schedule s
                    JOIN groups g ON s.group_id = g.id
                    JOIN subjects sub ON s.subject_id = sub.id
                    GROUP BY g.name, sub.name, s.lesson_type
                    ORDER BY g.name, sub.name";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                LoadGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        g.name AS ""Группа"",
                        g.student_count AS ""Студентов"",
                        SUM(s.hours_count) AS ""Часов за семестр"",
                        ROUND(SUM(s.hours_count) / 18, 1) AS ""Нагрузка в неделю"",
                        COUNT(CASE WHEN sub.assessment_type = 'Экзамен' THEN 1 END) AS ""Экзаменов"",
                        COUNT(CASE WHEN sub.assessment_type = 'Зачёт' THEN 1 END) AS ""Зачётов"",
                        CASE 
                            WHEN ROUND(SUM(s.hours_count) / 18, 1) > 30 THEN 'ПРЕВЫШЕНИЕ!'
                            ELSE 'Норма'
                        END AS ""Контроль нагрузки""
                    FROM groups g
                    LEFT JOIN schedule s ON g.id = s.group_id
                    LEFT JOIN subjects sub ON s.subject_id = sub.id
                    GROUP BY g.id, g.name, g.student_count
                    ORDER BY g.name";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                CompareGrid.ItemsSource = dt.DefaultView;
            }
        }
    }
}