using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp3
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            show();
        }
           SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=pharmacy;Integrated Security=True");

        private void show()
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select s.product_id,p.product_name,product_num,date,s.client_id,client_name from products p join sales s on p.product_id = s.product_id join Clients c on c.client_id = s.client_id ", con);
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
                SqlDataAdapter adapter = new SqlDataAdapter("select s.product_id,p.product_name,product_num,date ,s.client_id,client_name from sales s join products p on p.product_id = s.product_id  join Clients c on c.client_id = s.client_id where product_name like N'%" + textBox1.Text + "%'", con);

                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
            if (id1.Text == "" || id2.Text == "" || num.Text == "" || dateTimePicker1.Text == "")
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("insert into sales (product_num,client_id,product_id,date) values(@num,@id1,@id2,@date1)", con);
                   // cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id1", id1.Text);
                    cmd.Parameters.AddWithValue("@id2", id2.Text);
                    cmd.Parameters.AddWithValue("@num", num.Text);
                    cmd.Parameters.AddWithValue("@date1", dateTimePicker1.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الاضافة بنجاح");
                    con.Close();
                    show();
                    id1.Text = "";
                    id2.Text = "";
                    num.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                id1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                id2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                num.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
               dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id1.Text == "" || id2.Text == "" || num.Text == "" || dateTimePicker1.Text == "")
                MessageBox.Show("اختر بيان ليتم حذفه");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from sales where client_id  = @id1", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id1", id1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الحذف");
                    con.Close();
                    show();
                    id1.Text = "";
                    id2.Text = "";
                    num.Text = "";

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(id2.Text == "" || num.Text == "" || dateTimePicker1.Text == "" || id1.Text == "")
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update sales set product_num = @num, product_id = @id2 , date = @date1 where client_id = @id1", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id1", id1.Text);
                    cmd.Parameters.AddWithValue("@id2", id2.Text);
                    cmd.Parameters.AddWithValue("@num", num.Text);
                    cmd.Parameters.AddWithValue("@date1", dateTimePicker1.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم التعديل بنجاح");
                    con.Close();
                    show();
                    id1.Text = "";
                    id2.Text = "";
                    num.Text = "";
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

            id1.Text = "";
            id2.Text = "";
            num.Text = "";
            textBox1.Text = "";
            comboBox1.SelectedItem = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
