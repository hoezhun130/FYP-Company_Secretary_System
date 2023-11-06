using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Authenticator;
using System.Net.Mail;
using System.Data.SqlClient;
using static QRCoder.PayloadGenerator;

namespace FYP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set active view to loginView
            multiView.ActiveViewIndex = 0;

            if (Session["Email"] != null && Session["IsValidTwoFactorAuthentication"] != null && (bool)Session["IsValidTwoFactorAuthentication"])
            {
                // Redirect user to their respective page based on their role
                string userRole = Session["UserRole"].ToString();
                switch (userRole)
                {
                    case "ServiceProvider":
                        Response.Redirect("ServiceProviderPage.aspx");
                        break;
                    case "TenantAdmin":
                        Response.Redirect("TenantAdminPage.aspx");
                        break;
                    case "TenantUser":
                        Response.Redirect("../UserSite/TenantCategorySelection.aspx");
                        break;
                    case "ClientAdmin":
                        Response.Redirect("ClientAdminPage.aspx");
                        break;
                    case "ClientUser":
                        Response.Redirect("../UserSite/CategorySelection.aspx");
                        break;
                    default:
                        // Redirect to a default page or show an error message
                        Response.Redirect("DefaultPage.aspx");
                        break;
                }
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            Session["Email"] = email;

            string password = txtPassword.Text;

            string userType = ValidateUser(email, password);
            

            if (userType != null)
            {
                Session["UserRole"] = userType;
                string googleAuthKey = WebConfigurationManager.AppSettings["GoogleAuthKey"];
                string userUniqueKey = (email + googleAuthKey);

                // Two-Factor Authentication Setup
                TwoFactorAuthenticator twoFacAuth = new TwoFactorAuthenticator();
                var setupInfo = twoFacAuth.GenerateSetupCode("Company Secretary System Login", email, ConvertSecretToBytes(userUniqueKey, false), 300);
                Session["UserUniqueKey"] = userUniqueKey;
                Session["UserType"] = userType;

                statusLabel.Text = "Please authenticate using your Google Authenticator app";
                barcodeImage.ImageUrl = setupInfo.QrCodeSetupImageUrl;
                //setupCodeLabel.Text = setupInfo.ManualEntryKey;
                txtToken.Visible = true;
                btnAuthenticate.Visible = true;

                // Set active view to twoFactorView
                multiView.SetActiveView(twoFactorView);
            }
            else
            {
                statusLabel.Text = "Invalid email or password";
            }
        }

        private string ValidateUser(string email, string password)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string[] userTables = { "ServiceProvider", "TenantAdmin", "TenantUser", "ClientAdmin", "ClientUser" };
                string query;
                SqlCommand command;

                try
                {
                    connection.Open();
                    foreach (string userTable in userTables)
                    {
                        query = $"SELECT '1' FROM [{userTable}] WHERE Email = @Email AND Password = @Password";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return userTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    statusLabel.Text = "An error occurred while connecting to the database: " + ex.Message;
                }
            }
            return null;
        }



        protected void btnAuthenticate_Click(object sender, EventArgs e)
        {
            string token = txtToken.Text;
            TwoFactorAuthenticator twoFacAuth = new TwoFactorAuthenticator();
            string userUniqueKey = Session["UserUniqueKey"].ToString();
            bool isValid = twoFacAuth.ValidateTwoFactorPIN(userUniqueKey, token, false);

            if (isValid)
            {
                string email = txtEmail.Text;
                Session["Email"] = email;
                Session["IsValidTwoFactorAuthentication"] = true;

                string userType = Session["UserType"].ToString();
                Session["UserRole"] = userType;

                string userName = GetUserName(email, userType);
                Session["UserName"] = userName;

                string userID = GetUserID(email, userType);
                Session["UserID"] = userID;


                switch (userType)
                {
                    case "ServiceProvider":
                        Response.Redirect("ServiceProviderPage.aspx");
                        break;
                    case "TenantAdmin":
                        Response.Redirect("TenantAdminPage.aspx");
                        break;
                    case "TenantUser":
                        Response.Redirect("../UserSite/TenantCategorySelection.aspx");
                        break;
                    case "ClientAdmin":
                        Response.Redirect("ClientAdminPage.aspx");
                        break;
                    case "ClientUser":
                        Response.Redirect("../UserSite/CategorySelection.aspx");
                        break;
                    default:
                        statusLabel.Text = "Invalid user type";
                        break;
                }
            }
            else
            {
                statusLabel.Text = "Invalid authentication token";
            }
        }

        private string GetUserName(string email, string userRole)
        {
            string userName = string.Empty;
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT Name FROM [{userRole}] WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userName = result.ToString();
                    }
                }
            }

            return userName;
        }

        private string GetUserID(string email, string userRole)
        {
            string userID = string.Empty;
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;
            string columnID = "";

            // Determine the column name based on the user's role
            switch (userRole)
            {
                case "ServiceProvider":
                    columnID = "SP_ID";
                    break;
                case "TenantAdmin":
                    columnID = "TA_ID";
                    break;
                case "TenantUser":
                    columnID = "TU_ID";
                    break;
                case "ClientAdmin":
                    columnID = "CA_ID";
                    break;
                case "ClientUser":
                    columnID = "CU_ID";
                    break;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT {columnID} FROM [{userRole}] WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userID = result.ToString();
                    }
                }
            }

            return userID;
        }



        private static byte[] ConvertSecretToBytes(string secret, bool secretIsBase32)
        {
            return secretIsBase32 ? Base32Encoding.ToBytes(secret) : System.Text.Encoding.UTF8.GetBytes(secret);
        }

        protected void btnEmailOTP_Click(object sender, EventArgs e)
        {
            // Add logic for sending OTP via email
            string userEmail = txtEmail.Text;
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            SendEmail(userEmail, "Your OTP", "Your OTP is: " + otp);
            Session["OTP"] = otp;
            Session["OTPExpiration"] = DateTime.Now.AddMinutes(5);
            multiView.SetActiveView(emailOTPView);
        }

        protected void btnVerifyEmailOTP_Click(object sender, EventArgs e)
        {
            string userEnteredOTP = txtEmailOTP.Text;
            string sessionOTP = Session["OTP"].ToString();
            DateTime otpExpiration = (DateTime)Session["OTPExpiration"];

            if (DateTime.Now <= otpExpiration && userEnteredOTP == sessionOTP)
            {
                Session["Email"] = txtEmail.Text;
                Session["IsValidTwoFactorAuthentication"] = true;
                // Ensure UserRole is assigned before using it in a redirect
                if (Session["UserType"] != null && Session["UserRole"] == null)
                {
                    Session["UserRole"] = Session["UserType"].ToString();
                }
                Response.Redirect("Login.aspx");
            }
            else
            {
                statusLabel.Text = "Invalid or expired OTP";
            }
        }

        private void SendEmail(string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("hoezhun@gmail.com");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                smtpServer.Port = 587; // SMTP server port
                smtpServer.Credentials = new System.Net.NetworkCredential("hoezhun@gmail.com", "nnxjcoztxusbsory");
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                statusLabel.Text = "Email sent successfully";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Failed to send email. Error: " + ex.Message;
            }
        }

        private string GetUserRole(string email, string password)
        {
            // Implement the logic to query the database and return the user's role based on their email and password
            string connectionString = WebConfigurationManager.ConnectionStrings["RecordManagementConnectionString"].ConnectionString;

            string[] userTables = { "ServiceProvider", "TenantAdmin", "TenantUser", "ClientAdmin", "ClientUser" };
            string userRole = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var userTable in userTables)
                {
                    string query = $"SELECT 'Found' FROM {userTable} WHERE Email = @Email AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        var result = command.ExecuteScalar();

                        if (result != null && result.ToString() == "Found")
                        {
                            userRole = userTable;
                            break;
                        }
                    }
                }
            }

            return userRole;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}