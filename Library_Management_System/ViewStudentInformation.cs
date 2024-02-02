using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class ViewStudentInformation : Form
    {
        static string s = "server=SAGAR-SS;database=library;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public ViewStudentInformation()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewStudentInformation_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from NewStudent";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                label1.Visible = false;
                Image image = Image.FromFile("C:\\Users\\sagar\\Pictures\\Saved Pictures\\Library Management System\\search1.gif");
                pictureBox1.Image = image;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from NewStudent where enroll LIKE '" + textBox1.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from NewStudent";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                label1.Visible = true;
                Image image = Image.FromFile("C:\\Users\\sagar\\Pictures\\Saved Pictures\\Library Management System\\search.gif");
                pictureBox1.Image = image;
            }
                    con.Close();
                }

        int bid;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());  
            }
            panel2.Visible = true;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from NewStudent where studid="+bid+"";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

            textBox2.Text = ds.Tables[0].Rows[0][1].ToString();
            textBox3.Text = ds.Tables[0].Rows[0][2].ToString();
            textBox4.Text = ds.Tables[0].Rows[0][3].ToString();
            textBox5.Text = ds.Tables[0].Rows[0][4].ToString();
            textBox6.Text = ds.Tables[0].Rows[0][5].ToString();
            textBox7.Text = ds.Tables[0].Rows[0][6].ToString();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {//update student
            if (MessageBox.Show("Do you want to Update Student ", "Update Student", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                String sname = textBox2.Text;
                String enroll = textBox3.Text;
                String dep = textBox4.Text;
                String sem = textBox5.Text;
                String contact = textBox6.Text;
                String semail = textBox7.Text;
                cmd.CommandText = "update NewStudent set sname='" + sname + "',enroll='" + enroll + "',dep='" + dep + "',sem='" + sem + "',contact='" + contact + "',email='" + semail + "' where studid=" + rowid + "";
                cmd.ExecuteNonQuery();
                con.Close();
                ViewStudentInformation_Load(this, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            panel2.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Close Application","Closed Application", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
                Dashboard db= new Dashboard();
                db.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {//delete

            if (MessageBox.Show("Do you want to Delete Student ", "Deleted Student", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                String sname = textBox2.Text;
                String enroll = textBox3.Text;
                String dep = textBox4.Text;
                String sem = textBox5.Text;
                String contact = textBox6.Text;
                String semail = textBox7.Text;
                cmd.CommandText = "delete from NewStudent where studid="+rowid+"";
                cmd.ExecuteNonQuery();
                con.Close();
                ViewStudentInformation_Load(this, null);
            }
        }
    }
}