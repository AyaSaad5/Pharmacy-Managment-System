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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            show();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=pharmacy;Integrated Security=True");

        private void show()
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select p.product_name,s.product_id,quantity,price,type,supplier_name from products p join store s on s.product_id = p.product_id join purchases pu on pu.product_id = p.product_id join suppliers su on pu.supplier_id = su.supplier_id", con);
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
                SqlDataAdapter adapter = new SqlDataAdapter("select * from products where product_name like N'" + textBox3.Text + "%'", con);

                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if ((string)comboBox1.SelectedItem == "الكود")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select product_name,chemical_name,quantity,type,production_date,expire_date,price , supplier_name from products p , suppliers s, purchases pu where p.product_id = pu.product_id and pu.supplier_id = s.supplier_id", con);
               // cmd.CommandType = CommandType.Text; 

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
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            filter();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            show();
            id.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedItem = null;
        }

        private void save_Click_1(object sender, EventArgs e)
        {
            if (id.Text == "")
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("insert into store (product_id) values(@id)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الاضافة بنجاح");
                    con.Close();
                    show();
                    id.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
