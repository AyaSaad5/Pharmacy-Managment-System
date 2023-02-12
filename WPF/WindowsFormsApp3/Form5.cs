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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            show();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=pharmacy;Integrated Security=True");

        private void show()
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select pu.product_id,p.product_name,date,pu.supplier_id,supplier_name from products p join purchases pu on p.product_id = pu.product_id join suppliers s on s.supplier_id = pu.supplier_id ", con);
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
                SqlDataAdapter adapter = new SqlDataAdapter("select pu.product_id,p.product_name,date,supplier_name,s.supplier_id from purchases pu join products p on p.product_id = pu.product_id join suppliers s on s.supplier_id = pu.supplier_id where product_name like N'%" + textBox1.Text + "%'", con);
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
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (id1.Text == "" || id2.Text == "" || date1.Text.Length == 0)
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("insert into purchases (supplier_id,product_id,date) values(@id1,@id2,@date)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id1", id1.Text);
                    cmd.Parameters.AddWithValue("@id2", id2.Text);
                    cmd.Parameters.AddWithValue("@date", date1.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الاضافة بنجاح");
                    con.Close();
                    show();
                    id1.Text = "";
                    id2.Text = "";
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
            textBox1.Text = "";
            comboBox1.SelectedItem = null;
            id1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id1.Text == "" || id2.Text == "" || date1.Text == "")
                MessageBox.Show("اختر بيان ليتم حذفه");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from purchases where supplier_id  = @id1 ", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id1", id1.Text);
                    //cmd.Parameters.AddWithValue("@id2", id2.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الحذف");
                    con.Close();
                    show();
                    id1.Text = "";
                    id2.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id1.Text == "" || id2.Text == "" || date1.Text == "")
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update purchases set product_id = @id2, date = @date where supplier_id = @id1", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id1", id1.Text);
                    cmd.Parameters.AddWithValue("@id2", id2.Text);
                    cmd.Parameters.AddWithValue("@date", date1.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم التعديل بنجاح");
                    con.Close();
                    show();
                    id1.Text = "";
                    id2.Text = "";
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
                id1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                id2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                date1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
