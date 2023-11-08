using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.AdminSite
{
    public partial class CreateClientUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] != null)
                {
                    txtCreatedBy.Text = Session["UserName"].ToString();
                    txtCreatedBy.ReadOnly = true; // This will make the TextBox read-only so the user can't change it
                }
                else
                {
                    // If ServiceProviderName is not in session, it means the user is not logged in. Redirect them to the login page.
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            // Get user input from TextBox controls
            string name = txtName.Text;
            string position = txtPosition.Text;
            string companyName = txtCompanyName.Text;
            string icNumber = txtICNumber.Text; 
            string contactNumber = txtContactNumber.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            int tenantAdminId = Convert.ToInt32(Session["UserID"]);

            // Create SQL connection and insert command
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertCommand = "INSERT INTO [ClientUser] (Name, Position, CompanyName, ICNumber, ContactNumber, Email, Password, CA_ID) VALUES (@Name, @Position, @CompanyName, @ICNumber, @ContactNumber, @Email, @Password, @CA_ID)";
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@CompanyName", companyName);
                    command.Parameters.AddWithValue("@ICNumber", icNumber);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@CA_ID", tenantAdminId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Redirect to another page or show success message.
            Response.Redirect("ClientAdminPage.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientAdminPage.aspx");
        }
    }
}