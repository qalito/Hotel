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
    public partial class Form3 : Form
    {
        int znach = 0;
        string login; 

        public Form3(string logi)
        {
            InitializeComponent();
            login = logi;
            label2.Text = "Учётная запись: " + login + "\nДолжность: Администратор гостиницы";        }
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
       

 

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "hotelDataSet.Guests". При необходимости она может быть перемещена или удалена.
            this.guestsTableAdapter.Fill(this.hotelDataSet.Guests);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "hotelDataSet.Services". При необходимости она может быть перемещена или удалена.
            this.servicesTableAdapter.Fill(this.hotelDataSet.Services);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "hotelDataSet.Room". При необходимости она может быть перемещена или удалена.
            this.roomTableAdapter.Fill(this.hotelDataSet.Room);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "hotelDataSet.Comfort". При необходимости она может быть перемещена или удалена.
            this.comfortTableAdapter.Fill(this.hotelDataSet.Comfort);
            this.accommodationTableAdapter.Fill(this.hotelDataSet.Accommodation);
            this.guestsTableAdapter.Fill(this.hotelDataSet.Guests);
            this.roomTableAdapter.Fill(this.hotelDataSet.Room);
            this.servicesTableAdapter.Fill(this.hotelDataSet.Services);
            this.comfortTableAdapter.Fill(this.hotelDataSet.Comfort);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4(login); this.Close();
            frm4.Show();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Form5 fr = new Form5(login);this.Close();
            fr.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Hotel.mdf";
            string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection connect = new SqlConnection(myConnectionString);
            string sql = "Update [Accommodation] set [Статус]=@Статус   WHERE [Код проживания]=@Код";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);
            cmd_SQL.Parameters.AddWithValue("@Код", textBox2.Text);
            cmd_SQL.Parameters.AddWithValue("@Статус", "Проживание");

            try
            {
                connect.Open();
                int n = cmd_SQL.ExecuteNonQuery();
           
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                connect.Close();
                this.accommodationTableAdapter.Fill(this.hotelDataSet.Accommodation);
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if ((znach == 0) && (textBox2.Text != ""))
            {
                for (int i = 0; i < accommodationDataGridView.RowCount; i++)

                {

                    accommodationDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < accommodationDataGridView.ColumnCount; j++)

                        if (accommodationDataGridView.Rows[i].Cells[j].Value != null)

                            if (accommodationDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))

                            {

                                accommodationDataGridView.Rows[i].Selected = true;

                            }

                }

            }
            if ((znach == 1) && (textBox2.Text != ""))
            {
                for (int i = 0; i < roomDataGridView.RowCount; i++)

                {

                    roomDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < roomDataGridView.ColumnCount; j++)

                        if (roomDataGridView.Rows[i].Cells[j].Value != null)

                            if (roomDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))

                            {

                                roomDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            if ((znach == 2) && (textBox2.Text != ""))
            {
                for (int i = 0; i < comfortDataGridView.RowCount; i++)

                {

                    comfortDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < comfortDataGridView.ColumnCount; j++)

                        if (comfortDataGridView.Rows[i].Cells[j].Value != null)

                            if (comfortDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))

                            {

                                comfortDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            if ((znach == 3) && (textBox2.Text != ""))
            {
                for (int i = 0; i < guestsDataGridView.RowCount; i++)

                {

                    guestsDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < guestsDataGridView.ColumnCount; j++)

                        if (guestsDataGridView.Rows[i].Cells[j].Value != null)

                            if (guestsDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))

                            {

                                guestsDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            if ((znach == 4) && (textBox2.Text != ""))
            {
                for (int i = 0; i < servicesDataGridView.RowCount; i++)

                {

                    servicesDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < servicesDataGridView.ColumnCount; j++)

                        if (servicesDataGridView.Rows[i].Cells[j].Value != null)

                            if (servicesDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))

                            {

                                servicesDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            if (textBox2.Text == "")
            {
                accommodationDataGridView.ClearSelection();
                guestsDataGridView.ClearSelection();
                roomDataGridView.ClearSelection();
                comfortDataGridView.ClearSelection();
                servicesDataGridView.ClearSelection();
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Form6 fr = new Form6(login);this.Close();
            fr.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            znach = 1;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = false;
            roomDataGridView.Visible = true;
            comfortDataGridView.Visible = false;
            servicesDataGridView.Visible = false;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            znach = 0;
            accommodationDataGridView.Visible = true;
            guestsDataGridView.Visible = false;
            roomDataGridView.Visible = false;
            comfortDataGridView.Visible = false;
            servicesDataGridView.Visible = false;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            znach = 3;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = true;
            roomDataGridView.Visible = false;
            comfortDataGridView.Visible = false;
            servicesDataGridView.Visible = false;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            znach = 2;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = false;
            roomDataGridView.Visible = false;
            comfortDataGridView.Visible = true;
            servicesDataGridView.Visible = false;
        }

        private void Button8_Click(object sender, EventArgs e)
        {

            znach = 4;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = false;
            roomDataGridView.Visible = false;
            comfortDataGridView.Visible = false;
            servicesDataGridView.Visible = true;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show(); this.Close();
        }
    }
}
