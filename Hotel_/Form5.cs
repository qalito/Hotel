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

namespace Hotel_
{
    public partial class Form5 : Form
    {
        string login;
        public Form5(string logi)
        {
            login = logi;
            InitializeComponent(); LoadData();
        }
        private int x = 0; private int y = 0;
        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X; y = e.Y;
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Location = new Point(Location.X + (e.X - x), Location.Y + (e.Y - y));
            }
        }
        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            string path = Application.StartupPath + @"\Hotel.mdf";
            string connectString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT Accommodation.[Код гостя], Accommodation.[Код проживания], Guests.[ФИО], Accommodation.[Статус], Accommodation.[Код номера], Guests.[Контакты], Accommodation.[Дата прибытия], Accommodation.[Дата выбытия]  FROM Accommodation, Guests WHERE Accommodation.[Код гостя] = Guests.[Код гостя] AND Accommodation.[Статус] LIKE N'П%' ";

            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[8]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
          
        }
        private void Form5_Load(object sender, EventArgs e)
        {

        }
        private void LoadDataF()
        {
            dataGridView1.Rows.Clear();
            string path = Application.StartupPath + @"\Hotel.mdf";
            string connectString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT Accommodation.[Код гостя], Accommodation.[Код проживания], Guests.[ФИО], Accommodation.[Статус], Accommodation.[Код номера], Guests.[Контакты], Accommodation.[Дата прибытия], Accommodation.[Дата выбытия]  FROM Accommodation, Guests WHERE Accommodation.[Код гостя] = Guests.[Код гостя] AND Accommodation.[Статус] LIKE N'П%' AND Accommodation.[Дата выбытия] = CONVERT (date, SYSDATETIME())";

            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[8]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);

        }
        private void LoadDataС()
        {
            dataGridView1.Rows.Clear();
            string path = Application.StartupPath + @"\Hotel.mdf";
            string connectString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT Accommodation.[Код гостя], Accommodation.[Код проживания], Guests.[ФИО], Accommodation.[Статус], Accommodation.[Код номера], Guests.[Контакты], Accommodation.[Дата прибытия], Accommodation.[Дата выбытия]  FROM Accommodation, Guests WHERE Accommodation.[Код гостя] = Guests.[Код гостя] AND Accommodation.[Статус] LIKE N'Б%'";

            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[8]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);

        }
        private void Button14_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int vib = 0;
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                      vib = 0;

                    break;
                case 1:
                    vib = 1;

                    break;
                case 2:
                    vib = 2;

                    break;
                default:
                    vib = 0;
                    break;
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (vib == 0) { LoadData(); }
            else if (vib == 1) { LoadDataF(); }
            else if (vib == 2) { LoadDataС(); }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if  (textBox1.Text != "")
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)

                {

                    dataGridView1.Rows[i].Selected = false;

                    for (int j = 0; j < dataGridView1.ColumnCount; j++)

                        if (dataGridView1.Rows[i].Cells[j].Value != null)

                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                            {

                                dataGridView1.Rows[i].Selected = true;


                            }

                }
            }
            else if (textBox1.Text == "") dataGridView1.ClearSelection();
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Hotel.mdf";
            string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection connect = new SqlConnection(myConnectionString);
            string sql = "Update [Accommodation] set [Статус]=@Статус   WHERE [Код проживания]=@Код";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);
            cmd_SQL.Parameters.AddWithValue("@Код", textBox2.Text);
            cmd_SQL.Parameters.AddWithValue("@Статус", "Закрыто");

            try
            {
                connect.Open();
                int n = cmd_SQL.ExecuteNonQuery();
                LoadData();
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                connect.Close();
            }
            
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            this.Close(); Form3 fr = new Form3(login);
            fr.Show();
        }
    }
}
