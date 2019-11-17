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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        bool d = false, iz = false; int znach = 0;
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "hotelDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.hotelDataSet.Users);

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
        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void UsersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.hotelDataSet);

        }
        bool b = false; bool dob = false;
        private void Button3_Click(object sender, EventArgs e)
        {
            if (b == false)
            {
                usersBindingSource.AddNew();
                логинTextBox.ReadOnly = false;
                groupBox1.Visible = true; d = true;
                label8.Visible = false;
                b = true; dob = true;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            string path = Application.StartupPath + @"\Hotel.mdf";
            string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";

            SqlConnection connect = new SqlConnection(myConnectionString);
            string sql = "Delete from [Users]  WHERE Логин=@Логин;";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);
            cmd_SQL.Parameters.AddWithValue("@Логин", usersDataGridView.Rows[usersDataGridView.CurrentRow.Index].Cells[0].Value.ToString());
            try
            {
                connect.Open();
                int n = cmd_SQL.ExecuteNonQuery();

                label8.Text = "Запись успешно Удалена!";
                label8.Visible = true;
            }
            catch (SqlException ex)
            {
                label8.Text = "Ошибка! Запись не удалена";
                label8.Visible = true;

            }
            finally
            {
                connect.Close();
            }

            usersTableAdapter.Update(hotelDataSet.Users);
            this.usersTableAdapter.Fill(this.hotelDataSet.Users);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            b = false;
            if (логинTextBox.Text != "")
            {
                if (d == true)
                {
                    string path = Application.StartupPath + @"\Hotel.mdf";
                    string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
                    SqlConnection connect = new SqlConnection(myConnectionString);
                    string sql = "INSERT INTO [Users]  (Логин, ФИО, Должность, Пароль) VALUES (@Логин, @ФИО, @Должность,@Пароль );";
                    SqlCommand cmd_SQL = new SqlCommand(sql, connect);
                    cmd_SQL.Parameters.AddWithValue("@Логин", логинTextBox.Text);
                    cmd_SQL.Parameters.AddWithValue("@ФИО", фИОTextBox.Text);
                    cmd_SQL.Parameters.AddWithValue("@Должность", должностьComboBox.Text);
                    cmd_SQL.Parameters.AddWithValue("@Пароль", парольTextBox.Text);
                    try
                    {
                        connect.Open();
                        int n = cmd_SQL.ExecuteNonQuery();

                        label8.Text = "Запись успешно добавлена!";
                        label8.Visible = true;
                    }
                    catch (SqlException ex)
                    {
                        label8.Text = "Ошибка! Запись не добавлена введите данные корректно! ";
                        label8.Visible = true;

                    }
                    finally
                    {
                        connect.Close();
                        this.usersTableAdapter.Fill(this.hotelDataSet.Users);
                    }

                }
                if (iz == true)
                {


                    string path = Application.StartupPath + @"\Hotel.mdf";
                    string myConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + path + "'; Integrated Security = True";
                    SqlConnection connect = new SqlConnection(myConnectionString);
                    string sql = "Update [Users] set ФИО=@ФИО, Должность=@Должность, Пароль=@Пароль WHERE Логин=@Логин;";
                    SqlCommand cmd_SQL = new SqlCommand(sql, connect);
                    cmd_SQL.Parameters.AddWithValue("@Логин", логинTextBox.Text);
                    cmd_SQL.Parameters.AddWithValue("@ФИО", фИОTextBox.Text);
                    cmd_SQL.Parameters.AddWithValue("@Должность", должностьComboBox.Text);
                    cmd_SQL.Parameters.AddWithValue("@Пароль", парольTextBox.Text);
                    try
                    {
                        connect.Open();
                        int n = cmd_SQL.ExecuteNonQuery();

                        label8.Text = "Запись успешно измененина!";
                        label8.Visible = true;
                    }
                    catch (SqlException ex)
                    {
                        label8.Text = "Ошибка! Запись не измененина";
                        label8.Visible = true;

                    }
                    finally
                    {
                        connect.Close();
                        this.usersTableAdapter.Fill(this.hotelDataSet.Users);

                    }

                }
                логинTextBox.ReadOnly = false;
                groupBox1.Visible = false;
                Validate();
                usersBindingSource.EndEdit();
                usersTableAdapter.Update(hotelDataSet.Users);
            }
            else label8.Text = "Поле логина не может быть пустым!";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (dob != true)
            {
                b = false;
                label8.Visible = false;
                usersDataGridView.Rows[usersDataGridView.RowCount - 2].Cells[0].Value = " ";
                usersBindingSource.RemoveAt(usersDataGridView.RowCount - 2);
                Validate();
                usersBindingSource.EndEdit();
                usersTableAdapter.Update(hotelDataSet.Users);
                groupBox1.Visible = false;
            }
            groupBox1.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show(); this.Close();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    znach = 0;

                    break;
                case 1:
                    znach = 1;

                    break;
                case 2:
                    znach = 2;

                    break;
                case 3:
                    znach = 3;

                    break;
                default:
                    znach = 0;

                    break;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if ((znach == 0) && (textBox1.Text != ""))
            {
                for (int i = 0; i < usersDataGridView.RowCount; i++)

                {

                    usersDataGridView.Rows[i].Selected = false;

                    for (int j = 0; j < usersDataGridView.ColumnCount; j++)

                        if (usersDataGridView.Rows[i].Cells[j].Value != null)

                            if (usersDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                            {

                                usersDataGridView.Rows[i].Selected = true;


                            }

                }
            }
            else if (textBox1.Text != "") Search(znach);
            else if (textBox1.Text == "") usersDataGridView.ClearSelection();
        }
        private void Search(int znach)
        {
            for (int i = 0; i < usersDataGridView.RowCount; i++)

            {

                usersDataGridView.Rows[i].Selected = false;

                int j = znach - 1;

                if (usersDataGridView.Rows[i].Cells[j].Value != null)

                    if (usersDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))

                    {

                        usersDataGridView.Rows[i].Selected = true;

                        if (textBox1.Text == "") usersDataGridView.ClearSelection();

                    }

            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            List<string> filterParts = new List<string>();
            if (textBox2.Text != "")
                filterParts.Add("Логин = '" + textBox2.Text + "'");
            if (textBox3.Text != "")
                filterParts.Add("ФИО = '" + textBox3.Text + "'");
            if (comboBox2.SelectedIndex >= 0)
                filterParts.Add("Должность = '" + comboBox2.Text + "'");
            string filter = string.Join(" AND ", filterParts);
            usersBindingSource.Filter = filter;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    comboBox2.Text = "Администратор гостиницы";

                    break;

                default:
                    comboBox2.Text = "";

                    break;
            }
        }

        private void ДолжностьComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

   

        private void Button4_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            логинTextBox.ReadOnly = true;
            должностьComboBox.Text = usersDataGridView.Rows[usersDataGridView.CurrentRow.Index].Cells[2].Value.ToString();
            groupBox1.Visible = true;
            iz = true;
        }
    }
}
