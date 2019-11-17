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
    public partial class Form6 : Form
    { string login;
        public Form6(string logi)
        {
            InitializeComponent();
            login = logi;
            LoadData();
          
        }
        private void LoadDataF()
        {
            dataGridView1.Rows.Clear(); 
            string path = Application.StartupPath + @"\Hotel.mdf";
            string connectString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT * FROM Room WHERE Room.[Код номера] NOT IN (SELECT Accommodation.[Код номера] FROM Accommodation WHERE Accommodation.[Дата прибытия] BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") +"' ) ";
            //SELECT  *FROM Call WHERE phone_number NOT IN(SELECT phone_number FROM Phone_book)
            //CONVERT (date, SYSDATETIME()).
            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
               
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);

        }
        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            string path = Application.StartupPath + @"\Hotel.mdf";
            string connectString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT * FROM Room";
            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();

            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);

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
        private void RoomBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.roomBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.hotelDataSet);

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "hotelDataSet.Room". При необходимости она может быть перемещена или удалена.
            this.roomTableAdapter.Fill(this.hotelDataSet.Room);
            dateTimePicker1.MinDate= DateTime.Today.Date;
            dateTimePicker2.MinDate = DateTime.Today.Date;
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            LoadDataF();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            this.Close(); Form3 fr = new Form3(login);
            fr.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Hotel.mdf";
            string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection connect = new SqlConnection(myConnectionString); string sql = "INSERT INTO [Room]  ([Код номера], [Вместимость], [Класс], [Стоимость], [Тип удобства]) VALUES (@Код, @Вместимость, @Класс,@Стоимость, @Тип);";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);
          
            cmd_SQL.Parameters.AddWithValue("@Код", textBox2.Text);
            cmd_SQL.Parameters.AddWithValue("@Вместимость", numericUpDown1.Value);
            cmd_SQL.Parameters.AddWithValue("@Класс", ti);
            cmd_SQL.Parameters.AddWithValue("@Стоимость", textBox3.Text);
            cmd_SQL.Parameters.AddWithValue("@Тип", textBox4.Text);
          
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
        string ti;
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    ti = "Double";

                    break;
                case 1:
                    ti = "Quadriple";

                    break;
                case 2:
                    ti = "Twin";
                    break;
                case 3:
                    ti = "Triple";

                    break;
                case 4:
                    ti = "Duplex";

                    break;
                case 5:
                    ti = "Apartment";

                    break;
                case 6:
                    ti = "Sibgle";

                    break;
                case 7:
                    ti = "Executive";

                    break;
                default:
                    ti = comboBox1.Text;
                    break;
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
