using System;
using System.Data;
using System.Windows;
using Npgsql;

namespace TeacherScheduleApp
{
    public partial class TeacherWindow : Window
    {
        private string connectionString = "Host=localhost;Port=5432;Database=schedule_db;Username=postgres;Password=0000;";
        private int currentTeacherId;

        public TeacherWindow(int teacherId)
        {
            InitializeComponent();
            currentTeacherId = teacherId;
            LoadTeacherInfo();
        }

        private void LoadTeacherInfo()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlInfo = "SELECT full_name, email, birth_year, experience_years, specialization FROM teachers WHERE id = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlInfo, conn))
                    {
                        cmd.Parameters.AddWithValue("id", currentTeacherId);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["full_name"].ToString();
                                txtEmail.Text = "Email: " + reader["email"].ToString();
                                txtBirthYear.Text = reader["birth_year"].ToString() + " г.";
                                txtExperience.Text = reader["experience_years"].ToString() + " лет";

                                string specs = reader["specialization"].ToString();
                                string[] specsArray = specs.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                                SpecializationsList.ItemsSource = specsArray;
                            }
                        }
                    }

                    string sqlSubjects = @"
                        SELECT DISTINCT sub.name AS ""Предмет""
                        FROM schedule s
                        JOIN subjects sub ON s.subject_id = sub.id
                        WHERE s.teacher_id = @id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlSubjects, conn))
                    {
                        cmd.Parameters.AddWithValue("id", currentTeacherId);
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        SubjectsGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных препода: " + ex.Message);
            }
        }
    }
}