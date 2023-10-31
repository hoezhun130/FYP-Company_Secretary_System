using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();

            if (string.IsNullOrEmpty(categoryName))
            {
                lblMessage.Text = "Category name cannot be empty.";
                return;
            }

            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Category (CategoryID, CategoryName) VALUES (NEWID(), @CategoryName)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            BindGrid();
            txtCategoryName.Text = string.Empty;
            lblMessage.Text = "Category added successfully.";
        }

        protected void BindGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Category";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        sda.Fill(dt);
                        gvCategory.DataSource = dt;
                        gvCategory.DataBind();
                    }
                }
            }
        }

        protected void gvCategory_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // Implement Edit and Delete functionality
        }

        protected void lnkCategory_Command(object sender, CommandEventArgs e)
        {
            string categoryID = e.CommandArgument.ToString();
            Response.Redirect("CategoryDetail.aspx?CategoryID=" + categoryID);
        }

        private void CreateCategory(string categoryName, string allowPermission, string denyPermission, string parentFolderID, string userID, string userType)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Category (CategoryID, CategoryName, AllowPermission, DenyPermission, ParentFolderID, TU_ID, CU_ID) VALUES (@CategoryID, @CategoryName, @AllowPermission, @DenyPermission, @ParentFolderID, @TU_ID, @CU_ID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", Guid.NewGuid().ToString());
                    command.Parameters.AddWithValue("@CategoryName", categoryName);
                    command.Parameters.AddWithValue("@AllowPermission", allowPermission ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DenyPermission", denyPermission ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ParentFolderID", parentFolderID ?? (object)DBNull.Value);

                    if (userType == "TenantUser")
                    {
                        command.Parameters.AddWithValue("@TU_ID", userID);
                        command.Parameters.AddWithValue("@CU_ID", DBNull.Value);
                    }
                    else if (userType == "ClientUser")
                    {
                        command.Parameters.AddWithValue("@TU_ID", DBNull.Value);
                        command.Parameters.AddWithValue("@CU_ID", userID);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid user type");
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}