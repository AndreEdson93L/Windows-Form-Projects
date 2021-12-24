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

namespace SmallEnterpriseDbProject
{
    public partial class Registrer : Form
    {
        public Registrer()
        {
            InitializeComponent();
        }

        private void btnLoginPanel_Click(object sender, EventArgs e)
        {
            Hide();
            Login login = new Login();
            login.ShowDialog();
            Close();
        }
        private void btnRegistrer_Click(object sender, EventArgs e)
        {
            Hide();
            Registrer registrer = new Registrer();
            registrer.ShowDialog();
            Close();
        }
        private void btnRegistrerUser_Click(object sender, EventArgs e)
        {
            RegistrerModule();
        }
        private void RegistrerModule()
        {
            bool hasFields = (txtPasswordConfirmation.Text != string.Empty || txtPassword.Text != string.Empty || txtUsername.Text != string.Empty && txtPassword.Text == txtPasswordConfirmation.Text) ? true : false;
            bool isSamePassword = (txtPassword.Text == txtPasswordConfirmation.Text) ? true : false;
            int login = (hasFields && isSamePassword) ? 1 : 0;
            try
            {
                switch (login)
                {
                    case 1:
                        string query = "select * from login_table where username= '" + txtUsername.Text + "';";

                        using (SqlConnection connection = new SqlConnection(@"Server=DESKTOP-DL5TFII\SQLEXPRESS;Database=AddressBook;Trusted_Connection=True;"))
                        {
                            connection.Open();
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    bool isAlreadyTaken = (dr.Read()) ? true : false;
                                    if (isAlreadyTaken)
                                    {
                                        connection.Close();
                                        dr.Close();
                                        MessageBox.Show("Username has already been taken. Please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        dr.Close();

                                        query = "insert into login_table(username, psw) values (@username, @psw);";
                                        cmd.CommandText = query;

                                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = txtUsername.Text;
                                        cmd.Parameters.Add("@psw", SqlDbType.VarChar).Value = txtPassword.Text;
                                        cmd.ExecuteNonQuery();
                                        connection.Close();
                                        MessageBox.Show("Your Account has been created. Now you can login!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                        break;
                    case 0:
                        MessageBox.Show("Check the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something has gone wrong!");
            }
        }
    }
}
