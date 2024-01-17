using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.AdminSite
{
    public partial class TenantAdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
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