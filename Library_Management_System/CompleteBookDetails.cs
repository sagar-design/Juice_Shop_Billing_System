using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class CompleteBookDetails : Form
    {
        static string s = "server=SAGAR-SS;database=library;integrated security=true";
        SqlConnection con=new SqlConnection(s);
        public CompleteBookDetails()
        {
            InitializeComponent();
        }

        private void CompleteBookDetails_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from IRBook where book_return_date is null";
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds=new DataSet();
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            cmd.CommandText = "select * from IRBook where book_return_date is not null";
            SqlDataAdapter adp1=new SqlDataAdapter(cmd);
            DataSet dss=new DataSet();
            adp1.Fill(dss);
            dataGridView2.DataSource= dss.Tables[0];
       
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
