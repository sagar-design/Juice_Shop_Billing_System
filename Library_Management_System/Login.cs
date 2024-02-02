using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Login : Form
    {
        static string s = "server=SAGAR-SS;database=SAGAR;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {//login button
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from loginTable where username ='" + textBox1.Text + "' and pass='" + textBox2.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.Hide();
                Dashboard dsa = new Dashboard();
                dsa.Show();

            }
            else
            {
                MessageBox.Show("Wrong Username or Password", "Wrong Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {

            if (textBox1.Text == "Username")
            {
                textBox1.Clear();
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {

            if (textBox2.Text == "Password")
            {
                textBox2.Clear();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
