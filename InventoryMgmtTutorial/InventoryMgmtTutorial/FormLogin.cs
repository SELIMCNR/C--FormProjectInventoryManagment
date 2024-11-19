using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace InventoryMgmtTutorial
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        SqlConnection Conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryMdb;"); 

        private void ChckShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ChckShowPass.Checked == true)
            {
           
                TxtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                TxtPassword.UseSystemPasswordChar = true;
            
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = true;
            
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            TxtUsername.Text = "";
            TxtPassword.Text = "";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
         



            if (TxtUsername .Text == "" || TxtPassword .Text == "")
            {
                MessageBox.Show("Please fill all blank fields",
                  "Error Message", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            }
            else
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        Conn.Open();
                        string selectData = "SELECT * FROM userttbl Where Uname =@username AND Upassword =@password";
                        using (SqlCommand cmd = new SqlCommand(selectData, Conn))
                        {
                            cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim());
                            cmd.Parameters.AddWithValue("@password", TxtPassword.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Login succesfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ManageUsers frmManageUsers = new ManageUsers();
                                frmManageUsers.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username/Password", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex,
                 "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        Conn.Close();
                    }
                }


            }
        }
    }
}
