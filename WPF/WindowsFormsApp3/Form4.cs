using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form4 : Form
    {
        DataTable dt = new DataTable();
        public Form4()
        {
            InitializeComponent();
            show();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=pharmacy;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void show()
        {
           con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from clients", con);
            SqlCommandBuilder command = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }
        public void filter()
        {
            
          if ((string)comboBox1.SelectedItem == "الاسم")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from clients where client_name like N'%" + textBox1.Text + "%'", con);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if ((string)comboBox1.SelectedItem == "الكود")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from clients where client_id like N'%" + textBox1.Text + "%' ", con);

                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if ((string)comboBox1.SelectedItem == "العنوان")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from clients where address like N'%" + textBox1.Text + "%'", con);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            con.Close();
        }
        private void save_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || address.Text == "")
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("insert into Clients (client_name,phone,address) values(@name,@phone,@address)", con);
                    cmd.CommandType = CommandType.Text;
                   // cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@address",address.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الاضافة بنجاح");
                    con.Close();
                    show();
                    id.Text = "";
                    name.Text = "";
                    phone.Text = "";
                    address.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
           if (e.RowIndex >= 0)
            {
                id.Visible= true;
                id.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                phone.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                address.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || address.Text == "")
                MessageBox.Show("اختر بيان ليتم حذفه");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Clients where client_id  = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الحذف");
                    con.Close();
                    show();
                    id.Text = "";
                    name.Text = "";
                    phone.Text = "";
                    address.Text = "";

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id.Text == "" || name.Text == "" || phone.Text == "" || address.Text == "")
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Clients set client_name = @name,phone = @phone, address = @address where client_id = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@address", address.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم التعديل بنجاح");
                    con.Close();
                    show();
                    id.Text = "";
                    name.Text = "";
                    phone.Text = "";
                    address.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            filter();
            //textBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            show();
           
            name.Text = "";
            phone.Text = "";
            address.Text = "";
            textBox1.Text = "";
            comboBox1.SelectedItem = null;
            id.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
