using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;


namespace test_task_1
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e) // Суммарную зарплату в разрезе департаментов без руководителей
        {
            string sqlExpression = "SELECT SUM(salary) FROM employee WHERE chief_id != 'NULL' GROUP BY department_id";
            using (var connection = new SqliteConnection("Data Source=test_database.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var SUMsalary = reader.GetValue(0);
                            MessageBox.Show($"{SUMsalary} \t");
                        }
                    }
                }
            }
            Console.Read();
        }

        private void metroButton2_Click(object sender, EventArgs e) // Департамент, в котором у сотрудника зарплата максимальна
        {
            string sqlExpression = "SELECT department.name, employee.department_id FROM department LEFT JOIN employee ON ( employee.department_id = department.id ) WHERE salary = (SELECT MAX(salary) FROM employee)";
            using (var connection = new SqliteConnection("Data Source=test_database.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var department = reader.GetValue(0);
                            MessageBox.Show($"{department} \t");
                        }
                    }
                }
            }
            Console.Read();
        }

        private void metroButton3_Click(object sender, EventArgs e) //Зарплаты руководителей департаментов (по убыванию)
        {
            string sqlExpression = "SELECT salary FROM employee WHERE chief_id IS NULL GROUP BY department_id ORDER BY salary DESC";
            using (var connection = new SqliteConnection("Data Source=test_database.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var salary = reader.GetValue(0);
                            MessageBox.Show($"{salary} \t");
                        }
                    }
                }
            }
            Console.Read();
        }

        private void metroButton4_Click(object sender, EventArgs e) //Суммарную зарплату в разрезе департаментов c руководителями
        {
            string sqlExpression = "SELECT SUM(salary) FROM employee GROUP BY department_id";
            using (var connection = new SqliteConnection("Data Source=test_database.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var SUMsalary = reader.GetValue(0);
                            MessageBox.Show($"{SUMsalary} \t");
                        }
                    }
                }
            }
            Console.Read();
        }
    }
}
