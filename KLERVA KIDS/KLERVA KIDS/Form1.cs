﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KLERVA_KIDS
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-CJ0V0EH\\KWAKU_SQLSERVER;Initial Catalog=HEROES_ACADEMY_DB;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            InitializeFormControls();
        }

        private void InitializeFormControls()
        {
            //Set dropdown values
            comboGender.Items.AddRange(new string[] { "Male", "Female" });
            comboLevel.Items.AddRange(new string[] { "100", "200", "300", "400" });
            comboHealth.Items.AddRange(new string[] { "NILL", "SICK" });
            comboNationality.Items.AddRange(new string[] { "Ghanaian", "Japaneese", "American", "British", "Others" });
            comboBatch.Items.AddRange(new string[] { "7-9 AM", "9 - 11 AM", "11 - 1 PM", "1 - 3 PM" });
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateDOB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboNationality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Klever_Master_Table (Full_Name, Gender, Date_Of_Birth, Nationality, Level, Batch, Health_Status, Date_Registerd) VALUES (@FullName, @Gender, @DOB, @Nationality, @Level, @Batch, @HealthStatus, @DateReg)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Gender", comboGender.Text);
                cmd.Parameters.AddWithValue("DOB", dateDOB.Text);
                cmd.Parameters.AddWithValue("@Nationality", comboNationality.Text);
                cmd.Parameters.AddWithValue("@Level", comboLevel.Text);
                cmd.Parameters.AddWithValue("@Batch", comboBatch.Text);
                cmd.Parameters.AddWithValue("@HealthStatus", comboHealth.Text);
                cmd.Parameters.AddWithValue("@DateReg", dateRegistered.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Student record saved successfully.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Klever_Master_Table SET Full_Name=@FullName, Gender=@Gender, Date_Of_Birth=@DOB, Nationality=@Nationality, Level=@Level, Batch=@Batch, Health_Status=@HealthStatus, Date_Registered=DateReg WHERE Student_Id=@Id";

            using (SqlCommand cmd = new SqlCommand(@query, conn))
            {
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Gender", comboGender.Text);
                cmd.Parameters.AddWithValue("DOB", dateDOB.Text);
                cmd.Parameters.AddWithValue("@Nationality", comboNationality.Text);
                cmd.Parameters.AddWithValue("@Level", comboLevel.Text);
                cmd.Parameters.AddWithValue("@Batch", comboBatch.Text);
                cmd.Parameters.AddWithValue("@HealthStatus", comboHealth.Text);
                cmd.Parameters.AddWithValue("@Id", txtStudentId.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Student record updated");

            }

        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Klever_Master_Table WHERE Student_Id=@Id";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", txtStudentId.Text);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtFullName.Text = reader["Full_Name"].ToString();
                    comboGender.Text = reader["Gender"].ToString();
                    dateDOB.SelectedValue = Convert.ToDateTime(reader["Date_Of_Birth"]);
                    comboNationality.Text = reader["Nationality"].ToString();
                    comboLevel.Text = reader["Level"].ToString();
                    comboBatch.Text = reader["Batch"].ToString();
                    dateRegistered.SelectedValue = Convert.ToDateTime(reader["Date Registered"]);
                }
                else
                {
                    MessageBox.Show("Student not found!");
                }
                conn.Close();
            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Klever_Master_Table WHERE Student_Id=Id";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", txtStudentId.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record deleted!");
            }
        }

        private void butReport_Click(object sender, EventArgs e)
        {
            string inputId = Microsoft.VisualBasic.Interaction.InputBox("Enter Student ID", "Student Report");
            string query= "SELECT * FROM Klever_Master_Table WHERE Student_Id=@Id";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", inputId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string report = "Name: " + reader["Full_Name"] + "\nGender: " + reader["Gender"] +
                        "\nDOB: " + reader["Date_Of_Birth"] + "\nNationality: " + reader["Nationality"] +
                        "\nLevel: " + reader["Level"] + "\nBatch: " + reader["Batch"] + "\nHealth: " +
                        reader["Health_Status"] + "\nRegistered: " + reader["Date_Registered"];
                    MessageBox.Show(report, "Student Report");
                }
                else
                {
                    MessageBox.Show("Student not found!");
                }
                conn.Close();
            }

        }

        private void butExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
