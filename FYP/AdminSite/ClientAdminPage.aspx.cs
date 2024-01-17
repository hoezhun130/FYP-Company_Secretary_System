using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class ClientAdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    // Retrieve user details from the database
                    string userId = Session["UserID"].ToString();
                    GetUserDetails(userId);
                }
            }
        }

        private void GetUserDetails(string userId)
        {
            // Connection string
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            // SQL query to retrieve user details
            string query = "SELECT Name FROM ClientAdmin WHERE CA_ID = @CA_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CA_ID", userId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate user details
                        string userName = reader["Name"].ToString();
                        string userRole = "Client Admin"; // Hardcoded role based on table name

                        lblUserName.Text = userName;
                        lblUserRole.Text = userRole;
                    }

                    reader.Close();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminSite/CreateClientUser.aspx");
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