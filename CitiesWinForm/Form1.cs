using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitiesWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string source = "server=GALDAVA-PC\\SQLEXPRESS;" +
"integrated security=SSPI;" +
"database=Earth";


        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          

            string select = "SELECT Name FROM dbo.Cities";
            
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0]);
            }
            Controls.Add(listBox1);
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();
           
          
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Cities VALUES(@Name,@Population)", conn);
            cmd.Parameters.AddWithValue("@Name", selectedItem);
            cmd.Parameters.AddWithValue("@Population", textBox1.Text);
            cmd.CommandType = CommandType.Text;
            Console.WriteLine (cmd.ExecuteNonQuery()+" Rows");
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string select = "SELECT Name,Population FROM dbo.Cities";

            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> list = new List<string> { };
            while (reader.Read())
            {
                list.Add("\nCity: " +reader[0]+"\nPopulation: " + reader[1]);
            }
            string s = String.Join(",", list);
            MessageBox.Show(s);
            conn.Close();

        }
    }
}
