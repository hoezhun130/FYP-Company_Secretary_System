using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.UserSite
{
    public partial class TenantCategorySelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in and has a role
                if (Session["UserRole"] != null)
                {
                    string userRole = Session["UserRole"].ToString();
                    BindDefaultCategories();
                }
                else
                {
                    // If the user is not logged in, redirect to the login page
                    Response.Redirect("../AdminSite/Login.aspx");
                }
            }
        }

        private void BindDefaultCategories()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    int userID = Convert.ToInt32(Session["UserID"]); // The ID of the logged-in user
                    string userRole = Session["UserRole"].ToString();

                    // Choose the column name based on user role
                    string userColumn = userRole == "TenantUser" ? "TU_ID" : "CU_ID";

                    // Select categories that belong to the logged-in user
                    string query = $"SELECT CategoryID, CategoryName, IconPath FROM Category WHERE {userColumn} = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add the UserID parameter to the command
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            rptCategories.DataSource = dt;
                            rptCategories.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // Log the error
                    // Show a message to the user if needed
                }
            }
        }

        protected void Category_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "SelectCategory")
            {
                string categoryId = e.CommandArgument.ToString();
                string userRole = Session["UserRole"] != null ? Session["UserRole"].ToString() : string.Empty;

                // For debugging purposes
                System.Diagnostics.Debug.WriteLine($"User role: {userRole}, Category ID: {categoryId}");

                if (userRole.Equals("TenantUser", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect($"TenantCompanyList.aspx?CategoryID={categoryId}");
                }
                else
                {
                    // Log an error or handle the case where the role is neither TenantUser nor ClientUser
                    System.Diagnostics.Debug.WriteLine("Unhandled user role.");
                }
            }
        }
    }
}