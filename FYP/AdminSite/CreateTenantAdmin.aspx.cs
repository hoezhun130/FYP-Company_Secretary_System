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
    public partial class CreateTenantAdmin : System.Web.UI.Page
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
            string role = txtRole.Text;
            string contactNumber = txtContactNumber.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            int serviceProviderId = Convert.ToInt32(Session["UserID"]);



            // Create SQL connection and insert command
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertCommand = "INSERT INTO [TenantAdmin] (Name, Position, Role, ContactNumber, Email, Password, SP_ID) VALUES (@Name, @Position, @Role, @ContactNumber, @Email, @Password, @SP_ID)";
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@SP_ID", serviceProviderId);



                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Redirect to another page or show success message.
            Response.Redirect("ServiceProviderPage.aspx");
        }
    }
}