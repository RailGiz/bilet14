using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bilet14
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Server=localhost;Database=SLAUDatabase;Uid=root;Pwd=";

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            double[] coefficients = GetCoefficients();
            double[] results = SolveSLAU(coefficients);
            SaveToDatabase(coefficients, results);
            DisplayResults(results);
        }

        private double[] GetCoefficients()
        {
            double[] coefficients = new double[12];
            coefficients[0] = double.Parse(textBox1.Text);
            coefficients[1] = double.Parse(textBox2.Text);
            coefficients[2] = double.Parse(textBox3.Text);
            coefficients[3] = double.Parse(textBox4.Text);
            coefficients[4] = double.Parse(textBox5.Text);
            coefficients[5] = double.Parse(textBox6.Text);
            coefficients[6] = double.Parse(textBox7.Text);
            coefficients[7] = double.Parse(textBox8.Text);
            coefficients[8] = double.Parse(textBox9.Text);
            coefficients[9] = double.Parse(textBox10.Text);
            coefficients[10] = double.Parse(textBox11.Text);
            coefficients[11] = double.Parse(textBox12.Text);
            return coefficients;
        }

        private double[] SolveSLAU(double[] coefficients)
        {
            double[,] matrix = new double[3, 4];

            // Заполнение матрицы коэффициентов
            for (int i = 0; i < 3; i++)
            {
                matrix[i, 0] = coefficients[i];
                matrix[i, 1] = coefficients[i + 3];
                matrix[i, 2] = coefficients[i + 6];
                matrix[i, 3] = coefficients[i + 9];
            }

            // Приведение матрицы к треугольному виду методом Гаусса
            for (int k = 0; k < 3; k++)
            {
                double pivot = matrix[k, k];

                if (pivot == 0)
                {
                    throw new Exception("Ошибка: деление на ноль");
                }

                for (int j = k; j < 4; j++)
                {
                    matrix[k, j] /= pivot;
                }

                for (int i = k + 1; i < 3; i++)
                {
                    double factor = matrix[i, k];

                    for (int j = k; j < 4; j++)
                    {
                        matrix[i, j] -= factor * matrix[k, j];
                    }
                }
            }

            // Обратный ход метода Гаусса
            double[] results = new double[3];
            results[2] = matrix[2, 3] / matrix[2, 2];
            results[1] = (matrix[1, 3] - matrix[1, 2] * results[2]) / matrix[1, 1];
            results[0] = (matrix[0, 3] - matrix[0, 2] * results[2] - matrix[0, 1] * results[1]) / matrix[0, 0];

            return results;
        }

        private void SaveToDatabase(double[] coefficients, double[] results)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO SLAUTable (a, b, c, d, e, f, g, h, i, j, k, l, x1, x2, x3) " +
                               "VALUES (@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k, @l, @x1, @x2, @x3)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@a", coefficients[0]);
                    command.Parameters.AddWithValue("@b", coefficients[1]);
                    command.Parameters.AddWithValue("@c", coefficients[2]);
                    command.Parameters.AddWithValue("@d", coefficients[3]);
                    command.Parameters.AddWithValue("@e", coefficients[4]);
                    command.Parameters.AddWithValue("@f", coefficients[5]);
                    command.Parameters.AddWithValue("@g", coefficients[6]);
                    command.Parameters.AddWithValue("@h", coefficients[7]);
                    command.Parameters.AddWithValue("@i", coefficients[8]);
                    command.Parameters.AddWithValue("@j", coefficients[9]);
                    command.Parameters.AddWithValue("@k", coefficients[10]);
                    command.Parameters.AddWithValue("@l", coefficients[11]);
                    command.Parameters.AddWithValue("@x1", results[0]);
                    command.Parameters.AddWithValue("@x2", results[1]);
                    command.Parameters.AddWithValue("@x3", results[2]);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void DisplayResults(double[] results)
        {
            textBox13.Text = results[0].ToString();
            textBox14.Text = results[1].ToString();
            textBox15.Text = results[2].ToString();
        }


    }
}
