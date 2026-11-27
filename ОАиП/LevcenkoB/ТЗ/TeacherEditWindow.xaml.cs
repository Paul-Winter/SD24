using System;
using System.Windows;
using Npgsql;

namespace TeacherScheduleApp
{
    public partial class TeacherEditWindow : Window
    {
        private string connectionString = "Host=localhost;Port=5432;Database=schedule_db;Username=postgres;Password=0000;";
        private int teacherId = 0;

        public TeacherEditWindow(int id)
        {
            InitializeComponent();
            teacherId = id;
            if (teacherId > 0) LoadTeacher();
        }

        private void LoadTeacher()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT full_name, email, birth_year, experience_years, specialization FROM teachers WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", teacherId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtFio.Text = reader["full_name"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            txtYear.Text = reader["birth_year"].ToString();
                            txtExp.Text = reader["experience_years"].ToString();
                            txtSpec.Text = reader["specialization"].ToString();
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = teacherId > 0
                        ? "UPDATE teachers SET full_name=@fio, email=@email, birth_year=@year, experience_years=@exp, specialization=@spec WHERE id=@id"
                        : "INSERT INTO teachers (full_name, email, birth_year, experience_years, specialization) VALUES (@fio, @email, @year, @exp, @spec)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("fio", txtFio.Text);
                        cmd.Parameters.AddWithValue("email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("year", int.Parse(txtYear.Text));
                        cmd.Parameters.AddWithValue("exp", int.Parse(txtExp.Text));
                        cmd.Parameters.AddWithValue("spec", txtSpec.Text);
                        if (teacherId > 0) cmd.Parameters.AddWithValue("id", teacherId);
                        cmd.ExecuteNonQuery();
                    }
                }
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка при сохранении: " + ex.Message); }
        }
    }
}