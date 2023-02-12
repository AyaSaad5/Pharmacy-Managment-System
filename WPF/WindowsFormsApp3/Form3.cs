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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            show1();
        }
           SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=pharmacy;Integrated Security=True");
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void show1()
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select *from products ", con);
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
            { con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from products where product_id like N'%" + textBox2.Text + "%' ", con);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if ((string)comboBox1.SelectedItem == "الاسم")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from products where product_name like N'%" + textBox2.Text + "%'", con);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if ((string)comboBox1.SelectedItem == "السعر")
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from products where price between 0 and '"+ textBox2.Text + "'", con);
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void id_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || price.Text == "" || quantity.Text == "" || date1.Text.Length == 0 || date2.Text.Length == 0)
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("insert into products (product_name,chemical_name,quantity,type,production_date,expire_date,price,discount) values(@name,@chem,@quantity,@type,@date1,@date2,@price,@discount)", con);
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@chem", chem.Text);
                    cmd.Parameters.AddWithValue("@price", price.Text);
                    cmd.Parameters.AddWithValue("@type", type.Text);
                    cmd.Parameters.AddWithValue("@quantity", quantity.Text);
                    cmd.Parameters.AddWithValue("@discount", discount.Text);
                    cmd.Parameters.AddWithValue("@date1", date1.Value);
                    cmd.Parameters.AddWithValue("@date2", date2.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الاضافة بنجاح");
                    con.Close();
                    show1();
                    
                    name.Text = "";
                    chem.Text = "";
                    quantity.Text = "";
                    type.Text = "";
                    discount.Text = "";
                    price.Text = "";
                    date1.Text = null;
                    date2.Text = null;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                id.Visible = true;
                id.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                chem.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                quantity.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                type.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                price.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                discount.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            if (name.Text == "" || price.Text == "" || quantity.Text == "" || date1.Text.Length == 0 || date2.Text.Length == 0)
                MessageBox.Show("اختر بيان ليتم حذفه");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from products where product_id  = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الحذف");
                    con.Close();
                    show1();
                    id.Text = "";
                    name.Text = "";
                    price.Text = "";
                    type.Text = "";
                    chem.Text = "";
                    discount.Text = "";
                    quantity.Text = "";
                    date1.Text = null;
                    date2.Text = null;

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            show1();
            id.Text = "";
            name.Text = "";
            price.Text = "";
            type.Text = "";
            chem.Text = "";
            discount.Text = "";
            quantity.Text = "";
            textBox2.Text = "";
            date1.Text= null;
            date2.Text= null;
            id.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (name.Text == "" || price.Text == "" || quantity.Text == "" || date1.Text.Length == 0 || date2.Text.Length == 0)
                MessageBox.Show("من فضلك ادخل البيانات");
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update products set product_name = @name,chemical_name = @chem, quantity = @quantity,type = @type ,production_date = @date1,expire_date = @date2 ,price = @price,discount = @discount where product_id = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@chem", chem.Text);
                    cmd.Parameters.AddWithValue("@price", price.Text);
                    cmd.Parameters.AddWithValue("@type", type.Text);
                    cmd.Parameters.AddWithValue("@quantity", quantity.Text);
                    cmd.Parameters.AddWithValue("@discount", discount.Text);
                    cmd.Parameters.AddWithValue("@date1", date1.Value);
                    cmd.Parameters.AddWithValue("@date2", date2.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم التعديل بنجاح");
                    con.Close();
                    show1();
                    id.Text = "";
                    name.Text = "";
                    price.Text = "";
                    type.Text = "";
                    chem.Text = "";
                    discount.Text = "";
                    quantity.Text = "";
                    date1.Text = null;
                    date2.Text= null;
                   
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
            //textBox2.Text = "";
            
            //comboBox1.SelectedItem = null;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
