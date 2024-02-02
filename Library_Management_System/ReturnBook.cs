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

namespace Library_Management_System
{
    public partial class ReturnBook : Form
    {
        static string s = "server=SAGAR-SS;database=library;integrated security=true";
        SqlConnection con=new SqlConnection(s);
        public ReturnBook()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {//search book issued to student
            con.Open();
            SqlCommand cmd=new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType=CommandType.Text;
            cmd.CommandText = "select * from IRBook where std_enroll = '" + textBox1.Text + "' and book_return_date is null";
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            DataSet ds=new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Invalid ID or No Book Issued","Issued",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                panel2.Visible = false;
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            
        }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox1.Clear();
        }
        Int64 rowid;
        String bname;
        String bdate;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowid = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bname = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bdate = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            textBox2.Text = bname;
            textBox3.Text= bdate;
        }

        private void button4_Click(object sender, EventArgs e)
        {//return book
            if(MessageBox.Show("Do You Want You Want To Return Book","Return Success", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE IRBook SET book_return_date = @ReturnDate WHERE std_enroll = @StudentEnroll AND id = @RowId";
                cmd.Parameters.AddWithValue("@ReturnDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@StudentEnroll", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@RowId", rowid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Return Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReturnBook_Load(this, null);
                con.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}