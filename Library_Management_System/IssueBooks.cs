using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class IssueBooks : Form
    {
        static string s = "server=SAGAR-SS;database=library;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd = new SqlCommand("select bName from NewBook", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    comboBox1.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
            con.Close();
        }
        int count;
        private void button1_Click(object sender, EventArgs e)
        {//search student button
            if (textBox1.Text != "")
            {
                String eid = textBox1.Text;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from NewStudent where enroll='" + eid + "'";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DA.Fill(ds);
                //******************************************************************************
                //code to count how many book has been issued on this enrollment number

                cmd.CommandText = "select count(std_enroll) from IRBook where std_enroll='" + eid + "' and book_return_date is null";
                SqlDataAdapter DA1 = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                DA.Fill(ds1);

                count = int.Parse(ds1.Tables[0].Rows[0][0].ToString());

                //******************************************************************************
                if (ds.Tables[0].Rows.Count != 0)
                {
                    textBox2.Text = ds.Tables[0].Rows[0][1].ToString();
                    textBox3.Text = ds.Tables[0].Rows[0][3].ToString();
                    textBox4.Text = ds.Tables[0].Rows[0][4].ToString();
                    textBox5.Text = ds.Tables[0].Rows[0][5].ToString();
                    textBox6.Text = ds.Tables[0].Rows[0][6].ToString();
                }
                else
                {
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    MessageBox.Show("Invalid Enrollment No", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Exit Applicaiton", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                IssueBooks ibb = new IssueBooks();
                ibb.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {//issue books
            if (textBox2.Text != "")
            {
                if (comboBox1.SelectedIndex != -1 && count <= 2)
                {
                    String enroll = textBox1.Text;
                    String sname = textBox2.Text;
                    String sdep = textBox3.Text;
                    String sem = textBox4.Text;
                    String contact = textBox5.Text;
                    String email = textBox6.Text;
                    String bookname = comboBox1.Text;
                    String bookIssueDate = dateTimePicker1.Text;


                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into IRBook(std_enroll,std_name,std_dep,std_sem,std_contact,std_email,book_name,book_issue_date) values('" + enroll + "','" + sname + "','" + sdep + "','" + sem + "','" + contact + "','" + email + "','" + bookname + "','" + bookIssueDate + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                }

                else
                {
                    MessageBox.Show("Select Book or Maximum Number Of Book Has Been ISSUED", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Enter Valid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
