using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FYP.AdminSite
{
    public partial class CreateClientAdmin : System.Web.UI.Page
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
            string companyName = txtCompanyName.Text;
            string registrationNumber = txtRegistrationNumber.Text;
            string physicalAddress = txtPhysicalAddress.Text;
            string PICName = txtPICName.Text;
            string PICContactNumber = txtPICContactNumber.Text;
            string totalNumberofBOD = txtTotalNumberOfBOD.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            int tenantAdminId = Convert.ToInt32(Session["UserID"]);



            // Create SQL connection and insert command
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertCommand = "INSERT INTO [ClientAdmin] (CompanyName, RegistrationNumber, PhysicalAddress, Name, ContactNumber, TotalNumberOfBOD, Email, Password, TA_ID) VALUES (@CompanyName, @RegistrationNumber, @PhysicalAddress, @Name, @ContactNumber, @TotalNumberOfBOD, @Email, @Password, @TA_ID)";
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@CompanyName", companyName);
                    command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                    command.Parameters.AddWithValue("@PhysicalAddress", physicalAddress);
                    command.Parameters.AddWithValue("@Name", PICName);
                    command.Parameters.AddWithValue("@ContactNumber", PICContactNumber);
                    command.Parameters.AddWithValue("@TotalNumberOfBOD", totalNumberofBOD);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@TA_ID", tenantAdminId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Redirect to another page or show success message.
            Response.Redirect("TenantAdminPage.aspx");
        }
    }
}