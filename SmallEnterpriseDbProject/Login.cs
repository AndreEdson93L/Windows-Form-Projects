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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool hasField = (txtPassword.Text != string.Empty || txtUsername.Text != string.Empty) ? true : false;
            if (hasField)
            {
                string query = "select * from login_table where username = '" + txtUsername.Text + "' and psw = '" + txtPassword.Text + "';";

                using (SqlConnection connection = new SqlConnection(@"Server=DESKTOP-DL5TFII\SQLEXPRESS;Database=AddressBook;Trusted_Connection=True;"))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                connection.Close();
                                dr.Close();
                                this.Hide();

                                BizContacts bizContacts = new BizContacts();
                                bizContacts.ShowDialog();
                            }
                            else
                            {
                                connection.Close();
                                dr.Close();
                                MessageBox.Show("No Account avilable with this username and password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
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
    }
}
