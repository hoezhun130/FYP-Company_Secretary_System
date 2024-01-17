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
    public partial class CreateTenantUserPage : System.Web.UI.Page
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
            string Department = txtDepartment.Text;
            string contactNumber = txtContactNumber.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            int tenantUserId = Convert.ToInt32(Session["UserID"]);



            // Create SQL connection and insert command
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertCommand = "INSERT INTO [TenantUser] (Name, Position, Department, ContactNumber, Email, Password, TA_ID) VALUES (@Name, @Position, @Department, @ContactNumber, @Email, @Password, @TA_ID)";
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@Department", Department);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@TA_ID", tenantUserId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            int tenantUserId2;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectCommand = "SELECT TU_ID FROM TenantUser WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(selectCommand, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    object result = command.ExecuteScalar();

                    tenantUserId2 = Convert.ToInt32(result);

                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertSubscriptionCommand = "INSERT INTO [Subscription] (TierName, TierPrice, StartDate, EndDate, TU_ID) VALUES (@TierName, @TierPrice, @StartDate, @EndDate, @TU_ID)";
                using (SqlCommand command = new SqlCommand(insertSubscriptionCommand, connection))
                {
                    // Set values for Subscription table
                    command.Parameters.AddWithValue("@TierName", "Gold Package");
                    command.Parameters.AddWithValue("@TierPrice", 0);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@EndDate", DateTime.Now.AddYears(1));
                    command.Parameters.AddWithValue("@TU_ID", tenantUserId2);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Redirect to another page or show success message.
            Response.Redirect("TenantAdminPage.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TenantAdminPage.aspx");
        }
    }
}