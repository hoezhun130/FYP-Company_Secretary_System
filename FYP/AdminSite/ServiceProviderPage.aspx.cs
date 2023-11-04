using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class ServiceProviderPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminSite/CreateTenantAdmin.aspx");
        }

        protected void gvServiceProvider_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                if (e.Exception is SqlException sqlEx && (sqlEx.Number == 547)) // 547 is the error number for constraint violation
                {
                    // Display a user-friendly error message
                    lblErrorMessage.Text = "This Tenant Admin is currently associated with other Client Admin or Client User, this record is not allow to delete.";
                    e.ExceptionHandled = true;
                }
            }
        }

    }
}