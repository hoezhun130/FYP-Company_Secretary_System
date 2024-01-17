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
    public partial class DocumentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Session["UserRole"] != null)
                {
                    string categoryId = Request.QueryString["CategoryID"];
                    // Ensure that both CategoryID and CompanyName are present
                    if (categoryId != null)
                    {
                        string categoryName = GetCategoryName(categoryId);
                        lblCategoryName.Text = categoryName;
                        btnManageCategoryPermission.Visible = ShowManageCategoryPermissionButton();
                        hdnCategoryIDToDelete.Value = categoryId;
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
            if (!string.IsNullOrEmpty(categoryId))
            {
                Response.Redirect($"UploadDocument.aspx?CategoryID={categoryId}&CompanyName={companyName}");
            }
            else
            {
                // Handle the error scenario where CategoryID is not found
                // Redirect back to the category selection or show an error message
            }
        }

        protected string GetDocumentUrl(string fileName)
        {
            // Assuming the DocumentName does not contain any path, just the file name
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
                string categoryId = Request.QueryString["CategoryID"];
                string companyName = Request.QueryString["CompanyName"];
                if (!string.IsNullOrEmpty(documentId) && !string.IsNullOrEmpty(categoryId))
                {
                    Response.Redirect($"UpdateDocument.aspx?CategoryID={categoryId}&CompanyName={companyName}&DocumentID={documentId}");
                }
                else
                {
                    // Handle the error scenario where DocumentID or CompanyName is not found
                    // Redirect back to the document list or show an error message
                }
            }
        }

        protected void btnManageCategoryPermission_Click(object sender, EventArgs e)
        {
            string categoryId = Request.QueryString["CategoryID"];
            string companyName = Request.QueryString["CompanyName"];
            Response.Redirect($"SetCategoryPermission.aspx?CategoryID={categoryId}&CompanyName={companyName}");
        }

        protected void btnDeleteCategoryPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateCategory.aspx");
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




        // Method to delete a document by ID
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


        protected void btnDeleteCategory_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteCategory")
            {
                hdnCategoryIDToDelete.Value = e.CommandArgument.ToString();

                // Directly call the confirmation click event
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDeleteCategoryConfirmationModal", "showDeleteCategoryConfirmationModal();", true);
            }
        }

        protected void btnConfirmDeleteCategory_Click(object sender, EventArgs e)
        {
            string categoryId = Request.QueryString["CategoryID"];
            hdnCategoryIDToDelete.Value = categoryId;
            if (int.TryParse(hdnCategoryIDToDelete.Value, out int categoryID))
            {
                // Call the method to delete the category by ID
                DeleteCategoryByID(categoryID);

                // Redirect to the appropriate page or perform any other necessary actions
                Response.Redirect("CategorySelection.aspx");
            }

        }

        private void DeleteCategoryByID(int categoryID)
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
                        // Delete documents associated with the category
                        DeleteDocumentsForCategory(con, transaction, categoryID);

                        // Delete the category
                        string categoryQuery = "DELETE FROM Category WHERE CategoryID = @CategoryID";
                        using (SqlCommand categoryCmd = new SqlCommand(categoryQuery, con, transaction))
                        {
                            categoryCmd.Parameters.AddWithValue("@CategoryID", categoryID);
                            categoryCmd.ExecuteNonQuery();
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

        private void DeleteDocumentsForCategory(SqlConnection con, SqlTransaction transaction, int categoryID)
        {
            // Delete documents associated with the category
            string documentQuery = "DELETE FROM Document WHERE CategoryID = @CategoryID";
            using (SqlCommand documentCmd = new SqlCommand(documentQuery, con, transaction))
            {
                documentCmd.Parameters.AddWithValue("@CategoryID", categoryID);
                documentCmd.ExecuteNonQuery();
            }
        }

        protected bool ShowManageCategoryPermissionButton()
        {
            int currentUserCU_ID = Convert.ToInt32(Session["UserID"]);

            if (int.TryParse(Request.QueryString["CategoryID"], out int categoryID))
            {
                bool isCurrentUserAssociated = IsUserAssociatedWithCategory(currentUserCU_ID, categoryID);
                bool isDefaultCategory = IsDefaultCategory(categoryID);

                return isCurrentUserAssociated && !isDefaultCategory;
            }

            return false;
        }


        private bool IsUserAssociatedWithCategory(int userID, int categoryID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM Category WHERE CategoryID = @CategoryID AND CU_ID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool IsDefaultCategory(int categoryID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT IsDefault FROM Category WHERE CategoryID = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    object result = cmd.ExecuteScalar();
                    return result != null && Convert.ToBoolean(result);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserSite/CategorySelection.aspx");
        }

        protected void btnLogout_Click2(object sender, EventArgs e)
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