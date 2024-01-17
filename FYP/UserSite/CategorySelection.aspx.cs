using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Web.Configuration;

namespace FYP.UserSite
{
    public partial class CategorySelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in and has a role
                if (Session["UserRole"] != null)
                {
                    string userRole = Session["UserRole"].ToString();
                    // Check if it's the user's first login
                    bool isFirstLogin = CheckFirstLoginStatus();

                    if (isFirstLogin)
                    {
                        // Show the popup for the user to register a new password
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "showPasswordPopup();", true);
                    }
                    BindDefaultCategories();
                }
                else
                {
                    // If the user is not logged in, redirect to the login page
                    Response.Redirect("../AdminSite/Login.aspx");
                }
            }
        }


        private bool CheckFirstLoginStatus()
        {
            // Retrieve the user's first login status from the database
            int userId = Convert.ToInt32(Session["UserID"]);
            bool isFirstLogin = false;
            isFirstLogin = GetFirstLoginStatusFromDatabase(userId);

            return isFirstLogin;
        }

        private bool GetFirstLoginStatusFromDatabase(int userId)
        {
            bool isFirstLogin = true;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT IsFirstLogin FROM ClientUser WHERE CU_ID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            isFirstLogin = Convert.ToBoolean(result);
                        }




                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log error, and show message to user if needed
                    // For simplicity, let's just assume the default value (true)
                }
            }

            return isFirstLogin;
        }

        private void UpdateUserPassword(int userId, string newPassword)
        {
            // Add your logic to update the password in the database
            // You should hash and salt the password before storing it in the database

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Hash and salt the password before storing it
                    //string hashedPassword = HashPassword(newPassword);

                    string updatePasswordQuery = "UPDATE ClientUser SET Password = @Password WHERE CU_ID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(updatePasswordQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log error, and show message to user if needed
                    ShowErrorMessage("An error occurred while updating the password." + ex.Message);
                }
            }
        }

        private void SetIsFirstLoginFalse(int userId)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Update the IsFirstLogin column to false
                    string updateIsFirstLoginQuery = "UPDATE ClientUser SET IsFirstLogin = 0 WHERE CU_ID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(updateIsFirstLoginQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log error, and show message to user if needed
                    ShowErrorMessage("An error occurred while updating the IsFirstLogin status.");
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
                    int userID = GetUserID();

                    string query = @"
                SELECT CategoryID, CategoryName, IconPath, AllowPermission, CU_ID, IsDefault
                FROM Category 
                 WHERE (IsDefault = 1 OR (CU_ID IS NOT NULL AND (CU_ID = @UserID OR AllowPermission LIKE @UserName OR AllowPermission IS NULL)))
                ORDER BY IsDefault DESC, CategoryName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@UserName", "%" + GetUserName() + "%");

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            // Filter categories based on AllowPermission
                            var filteredCategories = dt.AsEnumerable().Where(row =>
                            {
                                bool isDefault = Convert.ToBoolean(row["IsDefault"]);
                                int cuId = row["CU_ID"] == DBNull.Value ? -1 : Convert.ToInt32(row["CU_ID"]);
                                string allowPermission = row["AllowPermission"].ToString();
                                string userName = GetUserName();

                                return isDefault || cuId == userID ||
                                    allowPermission.Contains(userName);
                            });

                            rptCategories.DataSource = filteredCategories.CopyToDataTable();
                            rptCategories.DataBind();

                            PopulateCategoryDropdownForModal(filteredCategories);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, possibly log the error and/or display a message to the user
                }
            }
        }

        private string GetUserName()
        {
            // Check if Session["UserID"] is available
            if (Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                return GetUserNameById(userID);
            }
            else
            {
                // If Session["UserID"] is not available, check for RememberMeCookie
                HttpCookie rememberMeCookie = Request.Cookies["RememberMeCookie"];

                if (rememberMeCookie != null)
                {
                    string email = GetEmailFromRememberMeCookie(rememberMeCookie);
                    return GetUserNameByEmail(email);
                }
            }

            // Return an empty string if neither Session["UserID"] nor RememberMeCookie is available
            return string.Empty;
        }

        private string GetUserNameById(int userID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Name FROM ClientUser WHERE TU_ID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
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

        private string GetUserNameByEmail(string email)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT UserName FROM ClientUser WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
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

        private int GetUserID()
        {
            // Check if Session["UserID"] is available
            if (Session["UserID"] != null && !string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                return Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                // If Session["UserID"] is not available, check for RememberMeCookie
                HttpCookie rememberMeCookie = Request.Cookies["RememberMeCookie"];

                if (rememberMeCookie != null)
                {
                    string email = GetEmailFromRememberMeCookie(rememberMeCookie);
                    return GetUserIDByEmail(email);
                }
            }

            // Return -1 if neither Session["UserID"] nor RememberMeCookie is available
            return 0;
        }


        private int GetUserIDByEmail(string email)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT CU_ID FROM ClientUser WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        object result = cmd.ExecuteScalar();

                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, possibly log the error and/or display a message to the user
                    return -1;
                }
            }
        }

        private string GetUserDepartment()
        {
            // Check if Session["UserID"] is available
            if (Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                return GetUserDepartmentById(userID);
            }
            else
            {
                // If Session["UserID"] is not available, check for RememberMeCookie
                HttpCookie rememberMeCookie = Request.Cookies["RememberMeCookie"];

                if (rememberMeCookie != null)
                {
                    string email = GetEmailFromRememberMeCookie(rememberMeCookie);
                    return GetUserDepartmentByEmail(email);
                }
            }

            // Return an empty string if neither Session["UserID"] nor RememberMeCookie is available
            return string.Empty;
        }

        private string GetUserDepartmentById(int userID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Department FROM ClientUser WHERE CU_ID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
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


        private string GetEmailFromRememberMeCookie(HttpCookie rememberMeCookie)
        {
            string cookieValue = rememberMeCookie.Value;
            string[] cookieParts = cookieValue.Split(',');

            foreach (string part in cookieParts)
            {
                if (part.StartsWith("Email="))
                {
                    return part.Substring("Email=".Length);
                }
            }

            return string.Empty;
        }

        private string GetUserDepartmentByEmail(string email)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Department FROM ClientUser WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
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

        protected void Category_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "SelectCategory")
            {
                string categoryId = e.CommandArgument.ToString();
                string userRole = Session["UserRole"] != null ? Session["UserRole"].ToString() : string.Empty;

                string companyName = GetUserCompanyName();

                if (userRole.Equals("ClientUser", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect($"DocumentList.aspx?CategoryID={categoryId}&CompanyName={companyName}");
                }
            }
        }

        private string GetUserCompanyName()
        {
            // Check if Session["UserID"] is available
            if (Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                return GetUserCompanyNameById(userID);
            }
            else
            {
                // If Session["UserID"] is not available, check for RememberMeCookie
                HttpCookie rememberMeCookie = Request.Cookies["RememberMeCookie"];

                if (rememberMeCookie != null)
                {
                    string email = GetEmailFromRememberMeCookie(rememberMeCookie);
                    return GetUserCompanyNameByEmail(email);
                }
            }

            // Return an empty string if neither Session["UserID"] nor RememberMeCookie is available
            return string.Empty;
        }

        private string GetUserCompanyNameById(int userID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT CompanyName FROM ClientUser WHERE CU_ID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
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

        private string GetUserCompanyNameByEmail(string email)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT CompanyName FROM ClientUser WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
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

        protected void btnCreateCategoryPage_Click(object sender, EventArgs e)
        {

            int userId = Convert.ToInt32(Session["UserID"]);
            int categoryLimit = GetCategoryLimit(userId);
            int existingCategoriesCount = GetExistingCategoriesCount(userId);

            Response.Redirect("CreateCategory.aspx");

            // Check if the user has reached the category limit
            if (existingCategoriesCount >= categoryLimit)
            {
                ShowErrorMessage("You have reached the maximum number of allowed categories.");
            }

            Response.Redirect("CreateCategory.aspx");
        }

        private int GetExistingCategoriesCount(int userId)
        {
            int count = 0;

            // Use the Category table to count the existing categories for the user
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) FROM Category WHERE CU_ID = @UserId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    try
                    {
                        conn.Open();
                        count = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log error, and show message to user if needed
                    }
                }
            }

            return count;
        }

        private int GetCategoryLimit(int userId)
        {
            int tier = GetSubscriptionTier(userId);

            // Define category limits based on tiers
            switch (tier)
            {
                case 1:
                    return 0; // Tier 1 can't create custom categories
                case 2:
                    return 5; // Tier 2 can create 5 custom categories
                case 3:
                    return 10; // Tier 3 can create 10 custom categories
                default:
                    return 0; // Default to 0 for unknown tier or errors
            }
        }

        private int GetSubscriptionTier(int userId)
        {
            int tier = 1;

            // Use the Subscription table to get the user's subscription tier
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT TierName FROM Subscription WHERE CU_ID = @UserId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Parse the tier name and determine the tier number
                            string tierName = result.ToString();
                            switch (tierName)
                            {
                                case "Gold Package":
                                    tier = 1;
                                    break;
                                case "Diamond Package":
                                    tier = 2;
                                    break;
                                case "Platinum Package":
                                    tier = 3;
                                    break;
                                    // Add more cases for other tiers if needed
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log error, and show message to user if needed
                    }
                }
            }

            return tier;
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            // Get the category ID from the hidden field
            int categoryId = Convert.ToInt32(ddlDeleteCategory.SelectedValue);


            // Call a method to delete the category (implement this method)
            DeleteCategory(categoryId);

            // Refresh the category list after deletion
            BindDefaultCategories();
        }

        private void DeleteCategory(int categoryId)
        {
            // Implement the logic to delete the category from the database based on the category ID
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Begin a SQL transaction for atomicity
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // Delete category
                        string deleteCategoryQuery = "DELETE FROM Category WHERE CategoryID = @CategoryId";

                        using (SqlCommand deleteCategoryCommand = new SqlCommand(deleteCategoryQuery, con, transaction))
                        {
                            deleteCategoryCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                            deleteCategoryCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // An error occurred, rollback the transaction
                        transaction.Rollback();
                        ShowErrorMessage("An error occurred while deleting the category.");
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while trying to delete the category.");
                }
            }
        }

        private void PopulateCategoryDropdownForModal(IEnumerable<DataRow> categories)
        {
            // Assuming you have a list of ListItem objects, adjust this based on your actual data structure
            List<ListItem> categoryList = new List<ListItem>();

            foreach (DataRow row in categories)
            {
                bool isDefault = Convert.ToBoolean(row["IsDefault"]);
                int categoryId = Convert.ToInt32(row["CategoryID"]);
                int cuId = row["CU_ID"] == DBNull.Value ? -1 : Convert.ToInt32(row["CU_ID"]);
                int currentUserId = Convert.ToInt32(Session["UserID"]);

                // Only add categories where IsDefault is true and TU_ID is equal to the current user's ID
                if (!isDefault && cuId == currentUserId)
                {
                    string categoryName = row["CategoryName"].ToString();

                    // Create a ListItem and add it to the list
                    ListItem listItem = new ListItem(categoryName, categoryId.ToString());
                    categoryList.Add(listItem);
                }

            }

            // Populate the DropDownList with the categories
            ddlDeleteCategory.DataSource = categoryList;
            ddlDeleteCategory.DataTextField = "Text";
            ddlDeleteCategory.DataValueField = "Value";
            ddlDeleteCategory.DataBind();

            // Add a default option
            ddlDeleteCategory.Items.Insert(0, new ListItem("Select an Option", ""));
        }

        private void ShowErrorMessage(string message)
        {
            // Implement logic to display an error message to the user
            // This could be done using JavaScript to show an alert or any other UI method
            // For simplicity, you can display a MessageBox (JavaScript alert) using ScriptManager
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{message}');", true);
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            // Check if passwords match
            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                // Continue with the password update and database changes
                int userId = Convert.ToInt32(Session["UserID"]);
                UpdateUserPassword(userId, txtNewPassword.Text);
                SetIsFirstLoginFalse(userId);

                // Close the modal if the update is successful
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePasswordModal", "$('#passwordModal').modal('hide');", true);
            }
            else
            {
                // Display an error message (you can use a label or another method)
                ShowErrorMessage("Passwords do not match. Please try again.");
            }
        }
    }
}