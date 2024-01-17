<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantAdminPage.aspx.cs" Inherits="FYP.AdminSite.TenantAdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tenant Admin Page</title>

    <!-- Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">

    <!-- jQuery, Popper.js, and Bootstrap JavaScript -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>
        body {
            background-color: #f8f9fa;
        }

        .box-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 50px;
        }

        .box {
            border: 2px solid #000000;
            border-radius: 10px;
            padding: 30px;
            text-align: center;
            cursor: pointer;
            width: 48%; /* Adjusted width */
            background-color: #fff;
            transition: all 0.3s ease-in-out;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

            .box:hover {
                color: black;
                transform: translateY(-5px);
                box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
            }

        .box-icon {
            font-size: 40px;
            margin-bottom: 20px;
            color: #000000;
        }

        .selection-heading {
            font-size: 28px;
            margin-bottom: 20px;
            color: #000000;
        }
    </style>
</head>
<body>



    <form id="form1" runat="server">
            <!-- Headers -->
    <div class="container-fluid bg-dark text-white">
        <div class="row">
            <div class="col-6">
                <h3 class="p-3">Record Management System</h3>
            </div>
            <div class="col-6 text-right p-3">
                    <button runat="server" class="btn btn-light ml-2" onclick="btnLogout_Click">
                        <i class="fas fa-user mr-2"></i>
                        <asp:Button ID="btnLogout2" runat="server" Text="Log Out" OnClick="btnLogout_Click" CssClass="btn" />

                    </button>
            </div>
        </div>
    </div>
        <div class="container mt-3 mx-lg-auto">
            <br />
            <br />
            <br />
            <br />
            <h1 class="text-center selection-heading">Select a User to Manage:</h1>

            <div class="d-flex justify-content-center">
                <div class="box mr-3 h-100" onclick="location.href='ManageTenantUser.aspx'">
                    <div class="d-flex flex-column align-items-center h-100">
                        <i class="fas fa-user box-icon"></i>
                        <b>
                            <p class="mb-0 flex-grow-1">Manage Tenant Users</p>
                        </b>
                    </div>
                </div>

                <div class="box h-100" onclick="location.href='ManageClientAdmin.aspx'">
                    <div class="d-flex flex-column align-items-center h-100">
                        <i class="fas fa-user-cog box-icon"></i>
                        <b>
                            <p class="mb-0 flex-grow-1">Manage Client Admin</p>
                        </b>
                    </div>
                </div>
            </div>
        </div>
    </form>






</body>
</html>
