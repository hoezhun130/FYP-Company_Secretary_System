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
            string department = txtDepartment.Text;
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
                string insertCommand = "INSERT INTO [ClientUser] (Name, Position, Department, CompanyName, ICNumber, ContactNumber, Email, Password, CA_ID) VALUES (@Name, @Position, @Department, @CompanyName, @ICNumber, @ContactNumber, @Email, @Password, @CA_ID)";
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@Department", department);
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

            int clientUserId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectCommand = "SELECT CU_ID FROM ClientUser WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(selectCommand, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    object result = command.ExecuteScalar();

                    clientUserId = Convert.ToInt32(result);

                }
            }

                    using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertSubscriptionCommand = "INSERT INTO [Subscription] (TierName, TierPrice, StartDate, EndDate, CU_ID) VALUES (@TierName, @TierPrice, @StartDate, @EndDate, @CU_ID)";
                using (SqlCommand command = new SqlCommand(insertSubscriptionCommand, connection))
                {
                    // Set values for Subscription table
                    command.Parameters.AddWithValue("@TierName", "Gold Package");
                    command.Parameters.AddWithValue("@TierPrice", 0);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@EndDate", DateTime.Now.AddYears(1));
                    command.Parameters.AddWithValue("@CU_ID", clientUserId);

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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear user session
            Session.Clear();
            Session.Abandon();

            // Clear authentication cookie
            HttpCookie cookie = new HttpCookie("RememberMeCookie");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            // Redirect to the login page
            Response.Redirect("~/AdminSite/Login.aspx");
        }
    }
}