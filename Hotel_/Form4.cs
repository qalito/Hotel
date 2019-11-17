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
    public partial class Form4 : Form
    {
        int znach = 0;
        string login;
        public Form4(string logi)
        {
            login = logi;
            InitializeComponent(); 
        }

        private void AccommodationBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.accommodationBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.hotelDataSet);

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
        private void Form4_Load(object sender, EventArgs e)
        {
            this.accommodationTableAdapter.Fill(this.hotelDataSet.Accommodation);
            this.guestsTableAdapter.Fill(this.hotelDataSet.Guests);
            this.roomTableAdapter.Fill(this.hotelDataSet.Room);
            this.servicesTableAdapter.Fill(this.hotelDataSet.Services);
            this.comfortTableAdapter.Fill(this.hotelDataSet.Comfort);
        }

     
        static bool Search()
        {
            bool b = false;

            return b;
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            bool b = false;
            groupBox3.Visible = true;
            if (код_гостяTextBox.Text != "")
            {
                for (int i = 0; i < guestsDataGridView.RowCount; i++)
                {

                    guestsDataGridView.Rows[i].Selected = false;

                    int j = 0;

                    if (guestsDataGridView.Rows[i].Cells[j].Value != null)

                        if ((guestsDataGridView.Rows[i].Cells[j].Value.ToString().Contains(код_гостяTextBox.Text)) && (b != true))

                        {

                            guestsDataGridView.CurrentCell = guestsDataGridView.Rows[i].Cells[j];
                            groupBox3.Visible = true; b = true;
                            button13.Visible = false; button9.Visible = false;

                        }

                }

            }


            if (b == false)
            {
                guestsBindingSource.AddNew();
                код_гостяTextBox1.ReadOnly = false;
                код_гостяTextBox1.Text = Convert.ToString(guestsDataGridView.RowCount - 1);
                код_гостяTextBox.Text = код_гостяTextBox1.Text;
                код_гостяTextBox1.ReadOnly = true;
                groupBox3.Visible = true;


            }
        }
        string pol = "-";
        private void Button9_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Hotel.mdf";
            string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection connect = new SqlConnection(myConnectionString);
            string sql = "INSERT INTO [Guests]  ([Код гостя], [ФИО], [Пол], [Документ], [Серия, Номер], [Гражданство], [Дата рождения], [Контакты]) VALUES (@Код, @ФИО, @Пол,@Документ,@Серия,@Гражданство,@Дата,@Контакты);";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);
            cmd_SQL.Parameters.AddWithValue("@Код", код_гостяTextBox1.Text);
            cmd_SQL.Parameters.AddWithValue("@ФИО", фИОTextBox.Text);
            cmd_SQL.Parameters.AddWithValue("@Пол", pol);
            cmd_SQL.Parameters.AddWithValue("@Документ", документTextBox.Text);
            cmd_SQL.Parameters.AddWithValue("@Серия", серия__НомерTextBox.Text);
            cmd_SQL.Parameters.AddWithValue("@Гражданство", гражданствоTextBox.Text);
            cmd_SQL.Parameters.AddWithValue("@Дата", дата_рожденияDateTimePicker.Value);
            cmd_SQL.Parameters.AddWithValue("@Контакты", контактыTextBox.Text);
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
            }
            Validate();
            this.guestsTableAdapter.Fill(this.hotelDataSet.Guests);
            groupBox3.Visible = false;
        }

        private void ПолComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (полComboBox.SelectedIndex)
            {
                case 0:
                    pol = "Мужской";

                    break;
                case 1:
                    pol = "Женский";

                    break;
                default:
                    pol = "-";

                    break;
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {

            guestsBindingSource.RemoveAt(guestsDataGridView.RowCount - 2);

            Validate();
            guestsBindingSource.EndEdit();
            guestsTableAdapter.Update(hotelDataSet.Guests);
            groupBox3.Visible = false;
        }

        private void Код_номераTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 46)
            {
                e.Handled = true;
            }
        }

        private void СуммаTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 46)
            {
                e.Handled = true;
            }
        }

        private void ОплатаTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 46)
            {
                e.Handled = true;
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {

            string path = Application.StartupPath + @"\Hotel.mdf";
            string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
            SqlConnection connect = new SqlConnection(myConnectionString); string sql = "INSERT INTO [Accommodation]  ([Код проживания], [Код гостя], [Дата прибытия], [Дата выбытия], [Код номера], [Доп услуги], [Сумма], [Оплата], [Статус]) VALUES (@Кодп, @Кодг, @Датап,@Датав, @Кодн, @Доп,@Сумма,@Оплата, @Статус);";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);
            код_проживанияTextBox.ReadOnly = false;
            string kod = код_проживанияTextBox.Text;
            код_проживанияTextBox.ReadOnly = true;
            cmd_SQL.Parameters.AddWithValue("@Кодп", Convert.ToInt32(kod));
            cmd_SQL.Parameters.AddWithValue("@Кодг", Convert.ToInt32(код_гостяTextBox.Text));
            cmd_SQL.Parameters.AddWithValue("@Датап", дата_прибытияDateTimePicker.Value);
            cmd_SQL.Parameters.AddWithValue("@Датав", дата_выбытияDateTimePicker.Value);
            cmd_SQL.Parameters.AddWithValue("@Кодн", Convert.ToInt32(код_номераTextBox.Text));
            cmd_SQL.Parameters.AddWithValue("@Доп", доп_услугиTextBox.Text);
            cmd_SQL.Parameters.AddWithValue("@Сумма", Convert.ToDouble(суммаTextBox.Text));
            cmd_SQL.Parameters.AddWithValue("@Оплата", Convert.ToDouble(оплатаTextBox.Text));
            cmd_SQL.Parameters.AddWithValue("@Статус", статусTextBox.Text);
            try
            {
                connect.Open();
                int n = cmd_SQL.ExecuteNonQuery();
                this.accommodationTableAdapter.Fill(this.hotelDataSet.Accommodation);
                this.Width = 747;
                this.Height = 632;
                button14.Visible = true;
                button15.Visible = true;
            }
            catch (SqlException ex)
            {


            }
            finally
            {
                connect.Close();
            }
            
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            this.Width = 747;
            this.Height = 632;
            button14.Visible = true;
            button15.Visible = true;
            this.accommodationTableAdapter.Fill(this.hotelDataSet.Accommodation);

        }

        private void Button4_Click(object sender, EventArgs e)
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
            znach = 1;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = false;
            roomDataGridView.Visible = true;
            comfortDataGridView.Visible = false;
            servicesDataGridView.Visible = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            znach = 2;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = false;
            roomDataGridView.Visible = false;
            comfortDataGridView.Visible = true;
            servicesDataGridView.Visible = false;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            znach = 3;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = true;
            roomDataGridView.Visible = false;
            comfortDataGridView.Visible = false;
            servicesDataGridView.Visible = false;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            znach = 4;
            accommodationDataGridView.Visible = false;
            guestsDataGridView.Visible = false;
            roomDataGridView.Visible = false;
            comfortDataGridView.Visible = false;
            servicesDataGridView.Visible = true;
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            this.Close(); Form3 fr = new Form3(login);
            fr.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        { this.Width =1076; 
            this.Height =632;
            button14.Visible = false;
            button15.Visible = false;
            accommodationBindingSource.AddNew();
            дата_прибытияDateTimePicker.MinDate = DateTime.Today.Date;
            дата_выбытияDateTimePicker.MinDate = дата_прибытияDateTimePicker.Value;
            статусTextBox.Text = "Бронь";
            статусTextBox.ReadOnly = true;
            код_проживанияTextBox.ReadOnly = false;
            код_проживанияTextBox.Text = Convert.ToString(accommodationDataGridView.RowCount - 1);
            код_проживанияTextBox.ReadOnly = true;
            groupBox1.Visible = true;
           
        }

        private void Дата_выбытияDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            дата_выбытияDateTimePicker.MinDate = дата_прибытияDateTimePicker.Value;
        }

        private void Дата_прибытияDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            дата_выбытияDateTimePicker.MinDate = дата_прибытияDateTimePicker.Value;
        }

        private void Button16_Click_1(object sender, EventArgs e)
        {
            this.Close(); Form3 fr = new Form3(login);
            fr.Show();
        }
       
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if ((znach == 0) && (textBox1.Text != ""))
            {
                for (int i = 0; i < accommodationDataGridView.RowCount; i++)

                {

                    accommodationDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < accommodationDataGridView.ColumnCount; j++)

                        if (accommodationDataGridView.Rows[i].Cells[j].Value != null)

                            if (accommodationDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                            {

                                accommodationDataGridView.Rows[i].Selected = true;


                            }

                }

            }
            else if ((znach == 1) && (textBox1.Text != ""))
            {
                for (int i = 0; i < roomDataGridView.RowCount; i++)

                {

                    roomDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < roomDataGridView.ColumnCount; j++)

                        if (roomDataGridView.Rows[i].Cells[j].Value != null)

                            if (roomDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                            {

                                roomDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            else if ((znach == 2) && (textBox1.Text != ""))
            {
                for (int i = 0; i < comfortDataGridView.RowCount; i++)

                {

                    comfortDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < comfortDataGridView.ColumnCount; j++)

                        if (comfortDataGridView.Rows[i].Cells[j].Value != null)

                            if (comfortDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                            {

                                comfortDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            else if ((znach == 3) && (textBox1.Text != ""))
            {
                for (int i = 0; i < guestsDataGridView.RowCount; i++)

                {

                    guestsDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < guestsDataGridView.ColumnCount; j++)

                        if (guestsDataGridView.Rows[i].Cells[j].Value != null)

                            if (guestsDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                            {

                                guestsDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            else if ((znach == 4) && (textBox1.Text != ""))
            {
                for (int i = 0; i < servicesDataGridView.RowCount; i++)

                {

                    servicesDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < servicesDataGridView.ColumnCount; j++)

                        if (servicesDataGridView.Rows[i].Cells[j].Value != null)

                            if (servicesDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                            {

                                servicesDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            else if (textBox1.Text == "")
            {
                accommodationDataGridView.ClearSelection();
                guestsDataGridView.ClearSelection();
                roomDataGridView.ClearSelection();
                comfortDataGridView.ClearSelection();
                servicesDataGridView.ClearSelection();
            }

        }
      
        private void GroupBox2_Enter_1(object sender, EventArgs e)
        {

        }
    }
}
