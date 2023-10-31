using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class CategoryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string categoryID = Request.QueryString["CategoryID"];
            if (string.IsNullOrEmpty(categoryID))
            {
                // Handle error: category ID is not provided
                lblMessage.Text = "Category ID is not provided.";
            }
            else
            {
                LoadCategoryDetail(categoryID);
            }
        }

        private void LoadCategoryDetail(string categoryID)
        {
            // Query database to get subcategories or documents of the selected category
            // Display them on the page
            // If there are no subcategories or documents, display a message that says there is no content in this category
            lblMessage.Text = "No content in this category.";
        }
    }
}