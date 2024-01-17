using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.UserSite
{
    public partial class TenantUploadDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserRole"] != null)
                {
                    if (Request.QueryString["CategoryID"] != null)
                    {
                        hiddenCategoryID.Value = Request.QueryString["CategoryID"];
                    }

                    // Get CompanyName from the query string and set it to the dropdown
                    string companyName = Request.QueryString["CompanyName"];
                    if (!string.IsNullOrEmpty(companyName))
                    {
                        ddlCompany.Items.Clear();
                        ddlCompany.Items.Add(new ListItem(companyName, companyName));
                        ddlCompany.DataBind();
                        ddlCompany.Enabled = false; // This ensures the dropdown is not editable
                    }

                    InitializeForm();
                }
                else
                {
                    // If the user is not logged in, redirect to the login page
                    Response.Redirect("../AdminSite/Login.aspx");
                }
            }
        }


        private void InitializeForm()
        {
            if (Session["UserID"] != null)
            {
                int.TryParse(Session["UserID"].ToString(), out int userId);

                // Fetch the user details to display the name
                FetchUserDetails(userId);

                // Set the current date
                txtDate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                // Redirect user to login page or show error because user ID session is not set.
                Response.Redirect("../AdminSite/Login.aspx");
            }
        }


        private void FetchUserDetails(int userId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            string query = @"
        SELECT [Name]
        FROM [dbo].[TenantUser]
        WHERE [TU_ID] = @TU_ID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TU_ID", userId);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the uploaded by text to the name of the current user
                            txtUploadedBy.Text = reader["Name"].ToString();
                        }
                        else
                        {
                            // Handle the case where user is not found
                            Response.Redirect("../AdminSite/Login.aspx");
                        }
                    }
                }
            }
        }



        protected void btnSaveDocument_Click(object sender, EventArgs e)
        {
            if (FileUploadDocument.HasFile)
            {
                try
                {
                    string companyName = ddlCompany.SelectedItem.Text;
                    SaveUploadedDocument(companyName);
                    ShowNotificationModal("Document uploaded successfully!", "TenantDocumentList.aspx");
                }
                catch (Exception ex)
                {
                    ShowNotificationModal("Error uploading document. ");
                }
            }
            else
            {
                ShowNotificationModal("Please select a document to upload.");
            }
        }

        private void SaveUploadedDocument(string companyName)
        {
            string fileName = FileUploadDocument.FileName;
            string filePath = Server.MapPath("~/Documents/") + fileName;
            int categoryID = int.Parse(hiddenCategoryID.Value);
            FileUploadDocument.SaveAs(filePath);

            InsertDocumentIntoDatabase(fileName, filePath, DateTime.Now, categoryID, companyName);
        }

        private void InsertDocumentIntoDatabase(string fileName, string filePath, DateTime uploadDate, int categoryID, string company)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            string query = @"
            INSERT INTO [dbo].[Document] (DocumentName, DocumentPath, Date, Company, TU_ID, CategoryID)
            VALUES (@DocumentName, @DocumentPath, @Date, @Company, @TU_ID, @CategoryID)";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DocumentName", fileName);
                    cmd.Parameters.AddWithValue("@DocumentPath", filePath);
                    cmd.Parameters.AddWithValue("@Date", uploadDate);
                    cmd.Parameters.AddWithValue("@Company", company);
                    cmd.Parameters.AddWithValue("@TU_ID", Session["UserID"]); // Use the actual session variable that stores the user's ID
                    cmd.Parameters.AddWithValue("@CategoryID", hiddenCategoryID.Value); // Include the CategoryID from the hidden field

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string categoryId = Request.QueryString["CategoryID"];
            string companyName = Request.QueryString["CompanyName"];

            if (!string.IsNullOrEmpty(categoryId) && !string.IsNullOrEmpty(companyName))
            {
                string encodedCompanyName = Server.UrlEncode(companyName);
                Response.Redirect($"TenantDocumentList.aspx?CategoryID={categoryId}&CompanyName={encodedCompanyName}");
            }
            else
            {
                // Redirect to a default page or handle the error if CategoryID or CompanyId is missing
                Response.Redirect("TenantDocumentList.aspx"); // Or another default page
            }
        }

        private void ShowNotificationModal(string message, string redirectPage = null)
        {
            string queryString = Request.QueryString.ToString();
            string redirectUrl = !string.IsNullOrEmpty(redirectPage) ? $"{redirectPage}?{queryString}" : null;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showNotification", $"showNotification('{message}', '{redirectUrl}');", true);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string categoryId = Request.QueryString["CategoryID"];
            string companyName = Request.QueryString["CompanyName"];

            if (!string.IsNullOrEmpty(categoryId) && !string.IsNullOrEmpty(companyName))
            {
                string encodedCompanyName = Server.UrlEncode(companyName);
                Response.Redirect($"TenantDocumentList.aspx?CategoryID={categoryId}&CompanyName={encodedCompanyName}");
            }
            else
            {
                // Redirect to a default page or handle the error if CategoryID or CompanyId is missing
                Response.Redirect("TenantDocumentList.aspx"); // Or another default page
            }
        }


    }
}