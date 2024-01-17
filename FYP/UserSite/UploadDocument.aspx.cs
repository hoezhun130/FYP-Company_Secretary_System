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
    public partial class UploadDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in and has a role
                if (Session["UserRole"] != null)
                {
                    if (Request.QueryString["CategoryID"] != null)
                    {
                        hiddenCategoryID.Value = Request.QueryString["CategoryID"];
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
            if (Session["Email"] != null)
            {
                string email = Session["Email"].ToString();
                FetchUserDetails(email);
            }
            else
            {
                // Redirect user to login page or show error because email session is not set.
            }

            // Set the current date
            txtDate.Text = DateTime.Now.ToShortDateString();
        }

        private void FetchUserDetails(string email)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            string query = @"
                SELECT [Name], [CompanyName]
                FROM [dbo].[ClientUser]
                WHERE [Email] = @Email";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assuming you have columns 'Name' and 'CompanyName' in your ClientUser table
                            txtUploadedBy.Text = reader["Name"].ToString();
                            ddlCompany.Items.Clear();
                            ddlCompany.Items.Add(new ListItem(reader["CompanyName"].ToString(), "CompanyID")); // You might want to replace "CompanyID" with an actual value if needed
                            ddlCompany.DataBind();
                        }
                        else
                        {
                            // Handle the case where user is not found
                            // Redirect to login or show error message
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
                    ShowNotificationModal("Document uploaded successfully!", "DocumentList.aspx");
                }
                catch (Exception ex)
                {
                    ShowNotificationModal("Error uploading document.");
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

            InsertDocumentIntoDatabase(fileName, filePath, DateTime.Now, categoryID,companyName);
        }

        private void InsertDocumentIntoDatabase(string fileName, string filePath, DateTime uploadDate, int categoryID, string company)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            string query = @"
            INSERT INTO [dbo].[Document] (DocumentName, DocumentPath, Date, Company, CU_ID, CategoryID)
            VALUES (@DocumentName, @DocumentPath, @Date, @Company, @CU_ID, @CategoryID)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DocumentName", fileName);
                    cmd.Parameters.AddWithValue("@DocumentPath", filePath);
                    cmd.Parameters.AddWithValue("@Date", uploadDate);
                    cmd.Parameters.AddWithValue("@Company", company);
                    cmd.Parameters.AddWithValue("@CU_ID", Session["UserID"]); // Use the actual session variable that stores the user's ID
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

            if (!string.IsNullOrEmpty(categoryId))
            {
                Response.Redirect($"DocumentList.aspx?CategoryID={categoryId}&CompanyName={companyName}");
            }
            else
            {
                // Redirect to a default page or handle the error if CategoryID or CompanyId is missing
                Response.Redirect("DocumentList.aspx"); // Or another default page
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

            if (!string.IsNullOrEmpty(categoryId))
            {
                Response.Redirect($"DocumentList.aspx?CategoryID={categoryId}&CompanyName={companyName}");
            }
            else
            {
                // Redirect to a default page or handle the error if CategoryID or CompanyId is missing
                Response.Redirect("DocumentList.aspx"); // Or another default page
            }
        }
    }
}