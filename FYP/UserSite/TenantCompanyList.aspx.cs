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
    public partial class CompanyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in and has a role
                if (Session["UserRole"] != null && Session["UserRole"].ToString() == "TenantUser")
                {
                    BindCompanies();
                }
                else
                {
                    // If the user is not logged in or not a TenantUser, redirect to the login page
                    Response.Redirect("../AdminSite/Login.aspx");
                }
            }
        }

        private void BindCompanies()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Select distinct company names from the ClientUser table
                    string query = "SELECT DISTINCT CompanyName FROM ClientUser";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            rptCompanies.DataSource = dt;
                            rptCompanies.DataBind();
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


        protected void Company_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "SelectCompany")
            {
                string companyName = e.CommandArgument.ToString(); // This is the CompanyName
                string categoryId = Request.QueryString["CategoryID"]; // Retrieve the CategoryID from the query string

                // Redirect to the TenantDocumentList page with both CategoryID and CompanyName
                // Encode the companyName to handle spaces or special characters
                string encodedCompanyName = HttpUtility.UrlEncode(companyName);

                // Redirect with both CategoryID and encoded CompanyName
                Response.Redirect($"TenantDocumentList.aspx?CategoryID={categoryId}&CompanyName={encodedCompanyName}");
            }
        }

    }
}