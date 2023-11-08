using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.UserSite
{
    public partial class TenantDocumentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Session["UserRole"] != null)
                {
                    // Ensure that both CategoryID and CompanyName are present
                    if (Request.QueryString["CategoryID"] != null && Request.QueryString["CompanyName"] != null)
                    {
                        BindDocuments();
                    }
                    else
                    {
                        // Redirect to an error page or show a message
                    }
                }
                else
                {
                    Response.Redirect("../AdminSite/Login.aspx");
                }
            }
        }

        private void BindDocuments()
        {
            DataTable dt = GetDocuments();

            RepeaterDocuments.DataSource = dt;
            RepeaterDocuments.DataBind();

            lblNoDocuments.Visible = dt.Rows.Count == 0;
        }

        private DataTable GetDocuments()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT DocumentID, DocumentName, DocumentPath 
            FROM Document
            WHERE Company = @CompanyName
            AND CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(query, con);

                // Retrieve the Category ID and Company Name from the query string
                cmd.Parameters.AddWithValue("@CategoryID", Request.QueryString["CategoryID"]);
                cmd.Parameters.AddWithValue("@CompanyName", Request.QueryString["CompanyName"]);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }



        protected void btnUploadDocument_Click(object sender, EventArgs e)
        {
            string categoryId = Request.QueryString["CategoryID"];
            string companyName = Request.QueryString["CompanyName"];
            if (!string.IsNullOrEmpty(categoryId) && !string.IsNullOrEmpty(companyName))
            {
                // Ensure that the company name is URL encoded when redirecting
                string encodedCompanyName = Server.UrlEncode(companyName);
                Response.Redirect($"TenantUploadDocument.aspx?CategoryID={categoryId}&CompanyName={encodedCompanyName}");
            }
            else
            {
                // Handle the error scenario where CategoryID or CompanyName is not found
                // Redirect back to the category selection or show an error message
            }
        }

        protected string GetDocumentUrl(string fileName)
        {
            string path = Server.MapPath("~/Documents/") + fileName;
            if (File.Exists(path))
            {
                return ResolveUrl("~/Documents/" + fileName);
            }
            else
            {
                // Log error - file not found, or handle accordingly
                return "#"; // Returning "#" for now, but should handle this appropriately
            }
        }
    }
}