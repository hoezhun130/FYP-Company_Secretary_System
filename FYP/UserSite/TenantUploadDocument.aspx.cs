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
                BindPermissionsDropdowns(email);
            }
            else
            {
                // Redirect user to login page or show error because email session is not set.
            }

            // Set the current date
            txtDate.Text = DateTime.Now.ToShortDateString();

            // Populate other dropdowns as needed
            // BindEditPermissionsDropdown();
            // BindViewPermissionsDropdown();
        }

        private void BindPermissionsDropdowns(string currentUserEmail)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            string query = @"
        SELECT [TU_ID], [Name]
        FROM [dbo].[TenantUser]
        WHERE [Email] != @Email";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", currentUserEmail);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlViewPermissions.Items.Clear();

                        ddlViewPermissions.Items.Add(new ListItem { Text = "Select a user", Value = "" });

                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            string tu_id = reader["TU_ID"].ToString();

                            ddlViewPermissions.Items.Add(new ListItem { Text = name, Value = tu_id });
                        }

                        ddlViewPermissions.DataBind();
                    }
                }
            }
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
                    SaveUploadedDocument();
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    lblErrorMessage.Text = ex.Message;
                }
            }
            else
            {
                // Handle the case where no file was uploaded
                // Display an error message
            }
        }

        private void SaveUploadedDocument()
        {
            string fileName = FileUploadDocument.FileName;
            string filePath = Server.MapPath("~/Documents/") + fileName;
            int categoryID = int.Parse(hiddenCategoryID.Value);
            FileUploadDocument.SaveAs(filePath);

            InsertDocumentIntoDatabase(fileName, filePath, DateTime.Now, categoryID);
        }

        private void InsertDocumentIntoDatabase(string fileName, string filePath, DateTime uploadDate, int categoryID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            string query = @"
            INSERT INTO [dbo].[Document] (DocumentName, DocumentPath, Date, ViewPermission, TU_ID, CategoryID)
            VALUES (@DocumentName, @DocumentPath, @Date, @ViewPermission, @TU_ID, @CategoryID)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DocumentName", fileName);
                    cmd.Parameters.AddWithValue("@DocumentPath", filePath);
                    cmd.Parameters.AddWithValue("@Date", uploadDate);
                    // Check if the selected value is not empty
                    if (string.IsNullOrEmpty(ddlViewPermissions.SelectedValue))
                    {
                        cmd.Parameters.AddWithValue("@ViewPermission", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ViewPermission", ddlViewPermissions.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@TU_ID", Session["UserID"]); // Use the actual session variable that stores the user's ID
                    cmd.Parameters.AddWithValue("@CategoryID", hiddenCategoryID.Value); // Include the CategoryID from the hidden field

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}