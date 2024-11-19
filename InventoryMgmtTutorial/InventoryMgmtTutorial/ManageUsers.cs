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
using System.IO;
using System.Text.RegularExpressions;

namespace InventoryMgmtTutorial
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryMdb;Integrated Security=True;");
        private void Form1_Load(object sender, EventArgs e)
        {
            displayManageUsersData();
            
        }



        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
        public void displayManageUsersData()
        {

            SqlCommand cmd = new SqlCommand("Select *From userttbl", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }



        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;

                displayManageUsersData();
            }
        }

  
        public void clearField()
        {
            
            TxtUsername   .Text = "";
            TxtFullname.Text = "";
            TxtTelephone .Text = "";
            TxtPassword.Text = "";
            


        }

        private void btnAdduser_Click(object sender, EventArgs e)
        {
            if (TxtUsername.Text == "" ||
                TxtFullname.Text == "" ||
                
                TxtTelephone.Text == "" || TxtPassword.Text =="" 
                )
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {


                if (conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                        { 


                            string insertData = "INSERT INTO userttbl (Uid,Uname,Ufullname,Upassword,Uphone) values (@Uid,@uName,@uFullname,@Upassword,@Uphone)";

                            using (SqlCommand insertEm = new SqlCommand(insertData, conn))
                            {

                                insertEm.Parameters.AddWithValue("@Uid", TxtUserId.Text.Trim());

                                insertEm.Parameters.AddWithValue("@Uname", TxtUsername.Text.Trim());

                                insertEm.Parameters.AddWithValue("@uFullName", TxtFullname.Text.Trim());
                                insertEm.Parameters.AddWithValue("@Upassword", TxtPassword.Text.Trim());
                                insertEm.Parameters.AddWithValue("@Uphone", TxtTelephone.Text.Trim());



                                insertEm.ExecuteNonQuery();

                                displayManageUsersData();

                                MessageBox.Show("Added succesfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearField();
                            }



                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        }

      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
              
                TxtUsername.Text = row.Cells[1].Value.ToString();
                TxtFullname.Text = row.Cells[2].Value.ToString();
                TxtPassword.Text = row.Cells[3].Value.ToString();
                TxtTelephone.Text = row.Cells[4].Value.ToString();
               
            }
        }

    
        private void btnclearemployee_Click(object sender, EventArgs e)
        {
            clearField();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            clearField();
        }

        private void btnedit(object sender, EventArgs e)
        {
            if (TxtUsername.Text == "" ||
                 TxtFullname.Text == "" ||

                 TxtTelephone.Text == "" || TxtPassword.Text == ""
                 )
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Update " +
                    "  ", "Confirmation Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (check == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        DateTime today = DateTime.Today;

                        string updateData = "UPDATE userttbl SET Uname=@uName ,Ufullname=@Ufullname,Upassword=@Upassword,Uphone=@Uphone where Uid = @Uid";

                        using (SqlCommand cmd = new SqlCommand(updateData, conn))
                        {

                            cmd.Parameters.AddWithValue("@uName", TxtUsername.Text.Trim());
                            cmd.Parameters.AddWithValue("Ufullname", TxtFullname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Upassword", TxtPassword.Text.Trim());
                            cmd.Parameters.AddWithValue("@Uphone", TxtTelephone.Text.Trim());

                            cmd.Parameters.AddWithValue("@Uid", TxtUserId.Text.Trim());

                            cmd.ExecuteNonQuery();
                            displayManageUsersData();

                            MessageBox.Show("Update succesfully. ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            clearField();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally { conn.Close(); }
                }
                else
                {
                    MessageBox.Show(" Cancelled. ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (TxtUsername.Text == "" ||
                TxtFullname.Text == "" ||

                TxtTelephone.Text == "" || TxtPassword.Text == ""
                )
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Delete " +
                    "Employee ID : " + TxtUserId.Text.Trim() + "?", "Confirmation Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (check == DialogResult.Yes)
                {

                    try
                    {
                        conn.Open();
                        DateTime today = DateTime.Today;

                        string updateData = "DELETE userttbl  where Uid = @Uid";

                        using (SqlCommand cmd = new SqlCommand(updateData, conn))
                        {
                        

                            cmd.Parameters.AddWithValue("@Uid", TxtUserId.Text.Trim());

                            cmd.ExecuteNonQuery();
                            displayManageUsersData();

                            MessageBox.Show("Delete succesfully. ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            clearField();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally { conn.Close(); }
                }
                else
                {
                    MessageBox.Show(" Cancelled. ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
        }
    }
}
