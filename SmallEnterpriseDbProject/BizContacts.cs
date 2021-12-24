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
    public partial class BizContacts : Form
    {
        private const string strConnection = @"Server=DESKTOP-DL5TFII\SQLEXPRESS;Database=AddressBook;Trusted_Connection=True;";
        private SqlConnection sqlConnection = new SqlConnection(strConnection);
        public BizContacts()
        {
            InitializeComponent();
            RefreshGrid();
        }
        public void RefreshGrid()
        {
            try
            {
                sqlConnection.Open();

                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from biz_contact";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd.CommandText, sqlConnection);
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    cboSearch.Items.Add(dataTable.Columns[i].ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"You should check the string connection!", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                txtCompany.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtWebsite.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtCity.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtState.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtPostalCode.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                txtMobile.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                txtNotes.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Select an item from the data grid view", "Invalid item", MessageBoxButtons.OK);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
            txtCompany.Text = "";
            txtWebsite.Text = "";
            txtTitle.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtPostalCode.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtNotes.Text = "";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == DateTimePicker.MinimumDateTime)
            {
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Short;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "insert into dbo.biz_contact(date_added, company, website, title, first_name, last_name, address, city, state, postal_code, email, mobile, notes) " +
                                               "values(@date_added, @company, @website, @title, @first_name, @last_name, @address, @city, @state, @postal_code, @email, @mobile, @notes);";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@date_added", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
                    cmd.Parameters.Add("@company", SqlDbType.NVarChar).Value = txtCompany.Text;
                    cmd.Parameters.Add("@website", SqlDbType.NVarChar).Value = txtWebsite.Text;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = txtTitle.Text;
                    cmd.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = txtFirstName.Text;
                    cmd.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = txtLastName.Text;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = txtAddress.Text;
                    cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = txtCity.Text;
                    cmd.Parameters.Add("@state", SqlDbType.NVarChar).Value = txtState.Text;
                    cmd.Parameters.Add("@postal_code", SqlDbType.NVarChar).Value = txtPostalCode.Text;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                    cmd.Parameters.Add("@mobile", SqlDbType.VarChar).Value = txtMobile.Text;
                    cmd.Parameters.Add("@notes", SqlDbType.NVarChar).Value = txtNotes.Text;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    RefreshGrid();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something's gone wrong!", "Bug", MessageBoxButtons.OK);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string query = "update dbo.biz_contact " +
                "set date_added= @date_added, company = @company, website = @website, title = @title, first_name = @first_name," +
                    " last_name = @last_name, address = @address, city = @city, state @state, postal_code= @postal_code, email = @email, mobile @mobile, notes = @notes" +
                " where id=@id";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = txtID.Text;
                    cmd.Parameters.Add("@date_added", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
                    cmd.Parameters.Add("@company", SqlDbType.NVarChar).Value = txtCompany.Text;
                    cmd.Parameters.Add("@website", SqlDbType.NVarChar).Value = txtWebsite.Text;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = txtTitle.Text;
                    cmd.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = txtFirstName.Text;
                    cmd.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = txtLastName.Text;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = txtAddress.Text;
                    cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = txtCity.Text;
                    cmd.Parameters.Add("@state", SqlDbType.NVarChar).Value = txtState.Text;
                    cmd.Parameters.Add("@postal_code", SqlDbType.NVarChar).Value = txtPostalCode.Text;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                    cmd.Parameters.Add("@mobile", SqlDbType.VarChar).Value = txtMobile.Text;
                    cmd.Parameters.Add("@notes", SqlDbType.NVarChar).Value = txtNotes.Text;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    RefreshGrid();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something's gone wrong!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "delete from dbo.biz_contact where id=@id";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = txtID.Text;

                    sqlConnection.Open();

                    //cmd.ExecuteNonQuery();
                    AreYouSure(cmd);
                    sqlConnection.Close();
                    RefreshGrid();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Check the format of the values in the text boxes");
            }
        }
        private void AreYouSure(SqlCommand cmd)
        {
            DialogResult answer = MessageBox.Show("Delete this record?", "Confirmation", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var column = cboSearch.SelectedIndex;
                DataGridViewCellEventArgs cellEventArgs;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (txtSearch.Text == dataGridView1.Rows[i].Cells[column].Value.ToString())
                    {
                        cellEventArgs = new DataGridViewCellEventArgs(column, i);
                        dataGridView1_CellClick(sender, cellEventArgs);
                        RefreshGrid();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Check if the value in the search bar is correct.", "Bug", MessageBoxButtons.OK);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            Login login = new Login();
            login.ShowDialog();
            Close();
        }
    }
}
