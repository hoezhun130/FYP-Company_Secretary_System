using System;
using System.Collections.Generic;
using System.Configuration;
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
                    string categoryId = Request.QueryString["CategoryID"];
                    // Ensure that both CategoryID and CompanyName are present
                    if (Request.QueryString["CategoryID"] != null && Request.QueryString["CompanyName"] != null)
                    {
                        string categoryName = GetCategoryName(categoryId);
                        lblCategoryName.Text = categoryName;
                        PopulateYearDropdowns();
                        int currentYear = DateTime.Now.Year;
                        // Call BindDocuments with the full range by default
                        BindDocuments(currentYear, currentYear);
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

        private string GetCategoryName(string categoryId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT CategoryName FROM Category WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                        object result = cmd.ExecuteScalar();
                        return result != null ? result.ToString() : string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, possibly log the error and/or display a message to the user
                    return string.Empty;
                }
            }
        }
        private void PopulateYearDropdowns()
        {
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year > currentYear - 5; year--)
            {
                ddlFromYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                ddlToYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }

            // Set the default selected year to 2023 for both dropdowns
            ddlFromYear.SelectedValue = currentYear.ToString();
            ddlToYear.SelectedValue = currentYear.ToString();
        }
        private void BindDocuments(int fromYear, int toYear)
        {
            DataTable dt = GetDocuments(fromYear, toYear);
            RepeaterDocuments.DataSource = dt;
            RepeaterDocuments.DataBind();
            lblNoDocuments.Visible = dt.Rows.Count == 0;
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            int fromYear = int.Parse(ddlFromYear.SelectedValue);
            int toYear = int.Parse(ddlToYear.SelectedValue);

            BindDocuments(fromYear, toYear);
        }

        private DataTable GetDocuments(int fromYear, int toYear)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                                    SELECT 
                                d.DocumentID, 
                                d.DocumentName, 
                                d.DocumentPath, 
                                d.Date,
                                MAX(ISNULL(cd.LastUpdate, d.LastUpdate)) AS LastUpdate,
                                COALESCE(tu.Name, cu.Name) AS UploadedByName,
                                MAX(ISNULL(cd.Date, d.Date)) AS DocumentDate
                            FROM Document AS d
                            LEFT JOIN TenantUser AS tu ON d.TU_ID = tu.TU_ID
                            LEFT JOIN ClientUser AS cu ON d.CU_ID = cu.CU_ID
                            LEFT JOIN Document AS cd ON d.DocumentID = cd.ParentDocumentID
                            WHERE d.Company = @CompanyName
                            AND d.CategoryID = @CategoryID
                            AND YEAR(d.Date) BETWEEN @FromYear AND @ToYear
                            GROUP BY d.DocumentID, d.DocumentName, d.DocumentPath, COALESCE(tu.Name, cu.Name), d.LastUpdate, d.Date
                            ";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@CategoryID", Request.QueryString["CategoryID"]);
                cmd.Parameters.AddWithValue("@CompanyName", Request.QueryString["CompanyName"]);
                cmd.Parameters.AddWithValue("@FromYear", fromYear);
                cmd.Parameters.AddWithValue("@ToYear", toYear);

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

        protected void btnEditDocument_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "UpdateDocument")
            {
                string documentId = e.CommandArgument.ToString();
                string companyName = Request.QueryString["CompanyName"];
                string categoryId = Request.QueryString["CategoryID"];
                if (!string.IsNullOrEmpty(documentId) && !string.IsNullOrEmpty(companyName) && !string.IsNullOrEmpty(categoryId))
                {
                    string encodedCompanyName = Server.UrlEncode(companyName);
                    string encodedCategoryId = Server.UrlEncode(categoryId);
                    Response.Redirect($"TenantUpdateDocument.aspx?DocumentID={documentId}&CompanyName={encodedCompanyName}&CategoryID={encodedCategoryId}");
                }
                else
                {
                    // Handle the error scenario
                }
            }
        }

        protected void btnDeleteDocument_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteDocument")
            {
                // Set the DocumentID to be deleted
                hdnDocumentIDToDelete.Value = e.CommandArgument.ToString();

                // Directly call the confirmation click event
                btnConfirmDelete_Click(sender, e);
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {

            int documentID = Convert.ToInt32(hdnDocumentIDToDelete.Value);

            // Call the method to delete the document by ID
            DeleteDocumentByID(documentID);

            int currentYear = DateTime.Now.Year;
            // Call BindDocuments with the full range by default
            BindDocuments(currentYear, currentYear);
        }

        private void DeleteDocumentByID(int documentID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Begin a transaction to ensure atomicity
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Delete comments associated with the document
                        DeleteCommentsForDocument(con, transaction, documentID);

                        // Delete the document
                        string documentQuery = "DELETE FROM Document WHERE DocumentID = @DocumentID";
                        using (SqlCommand documentCmd = new SqlCommand(documentQuery, con, transaction))
                        {
                            documentCmd.Parameters.AddWithValue("@DocumentID", documentID);
                            documentCmd.ExecuteNonQuery();
                        }

                        // Commit the transaction if all operations are successful
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of any exception
                        transaction.Rollback();
                        throw; // Re-throw the exception after rollback
                    }
                }
            }
        }

        private void DeleteCommentsForDocument(SqlConnection con, SqlTransaction transaction, int documentID)
        {
            // Delete comments associated with the document
            string commentQuery = "DELETE FROM Comment WHERE DocumentID = @DocumentID";
            using (SqlCommand commentCmd = new SqlCommand(commentQuery, con, transaction))
            {
                commentCmd.Parameters.AddWithValue("@DocumentID", documentID);
                commentCmd.ExecuteNonQuery();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string categoryId = Request.QueryString["CategoryID"];
            Response.Redirect($"~/UserSite/TenantCompanyList.aspx?CategoryID={categoryId}");
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
            Response.Redirect("../AdminSite/Login.aspx");
        }
    }
}