using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp3
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            show();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=pharmacy;Integrated Security=True");
        private void show()
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select s.supplier_id, supplier_name, phone, address, pu.product_id, product_name from suppliers s ,purchases pu, products p where s.supplier_id = pu.supplier_id and p.product_id = pu.product_id ", con);
            SqlCommandBuilder command = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }
        public void filter()
        {
            if ((string)comboBox1.SelectedItem == "الكود")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select s.supplier_id, supplier_name, phone, address, pu.product_id, product_name from suppliers s join purchases pu on s.supplier_id = pu.supplier_id join products p on p.product_id = pu.product_id where s.supplier_id = N'" + textBox5.Text + "'", con);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if ((string)comboBox1.SelectedItem == "الاسم")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select s.supplier_id, supplier_name, phone, address, pu.product_id, product_name from suppliers s join purchases pu on s.supplier_id = pu.supplier_id join products p on p.product_id = pu.product_id where supplier_name like  N'%" + textBox5.Text + "%'", con);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if ((string)comboBox1.SelectedItem == "العنوان")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select s.supplier_id, supplier_name, phone, address, pu.product_id, product_name from suppliers s join purchases pu on s.supplier_id = pu.supplier_id join products p on p.product_id = pu.product_id  where address like N'%" + textBox5.Text + "%'", con);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if ( name.Text == "" || phone.Text == "" || address.Text == "")
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("insert into suppliers (supplier_name,phone,address) values(@name,@phone,@address)", con);
                    cmd.CommandType = CommandType.Text;
                   // cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@address", address.Text);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (id.Text == "" || name.Text == "" || phone.Text == "" || address.Text == "")
                MessageBox.Show("اختر بيان ليتم حذفه");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from suppliers where supplier_id  = @id", con);
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
                    SqlCommand cmd = new SqlCommand("update suppliers set supplier_name = @name,phone = @phone, address = @address where supplier_id = @id", con);
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

        private void button4_Click(object sender, EventArgs e)
        {
            filter();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            show();
            comboBox1.SelectedItem = null;
            id.Visible = false;
            name.Text = "";
            phone.Text = "";
            address.Text = "";
            textBox5.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                id.Visible = true;
                id.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                phone.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                address.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
