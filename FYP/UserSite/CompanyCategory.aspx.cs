using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.UserSite
{
    public partial class CompanyCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userID = Convert.ToInt32(Session["UserID"]); // Assuming you store UserID in session when the user logs in
                BindCompanyName(userID);
                BindCategories(userID);
            }
        }

        private void BindCompanyName(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CompanyName FROM ClientUser WHERE CU_ID = @UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                object companyName = cmd.ExecuteScalar();
                if (companyName != null)
                {
                    ltCompanyName.Text = companyName.ToString();
                }
                else
                {
                    ltCompanyName.Text = "Company Not Found";
                }
                con.Close();
            }
        }

        private void BindCategories(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryName FROM Category WHERE CU_ID = @UserID OR TU_ID = @UserID";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptCategories.DataSource = dt;
                rptCategories.DataBind();
            }
        }
    }
}