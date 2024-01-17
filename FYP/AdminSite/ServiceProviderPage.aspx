<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceProviderPage.aspx.cs" Inherits="FYP.ServiceProviderPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Provider Page</title>

    <!--Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">


    <!-- jQuery, Popper.js, and Bootstrap JavaScript -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>
        .input-control {
            width: 200px; /* Set the width as needed */
            height: 30px; /* Set the height as needed */
            font-size: 16px; /* Set the font size as needed */
        }

        .larger-header {
            width: 80px;
        }

        .larger-header2 {
            width: 150px;
        }

        body {
            background-color: #e8e7e9; /* Set the background color to white */
        }

        .table.table-striped.table-bordered.thead-dark th {
            color: #000; /* Black color for the table heading text */
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

        <div class="container mt-3 mx-lg-5">
            <br />
            <h3 class="text-left">
                <i class="fas fa-user mr-2"></i><b>Create Tenant Admin Account Dashboard</b>
            </h3>
            <br />
            <p class="text-left">
                <b class="mr-1">User: </b>
                <asp:Label ID="lblUserName" runat="server" CssClass="mr-1" />
            </p>

            <p class="text-left">
                <b class="mr-1">Role:</b>
                <asp:Label ID="lblUserRole" runat="server" />
            </p>

            <br />
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>

            <asp:GridView ID="gvServiceProvider" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TA_ID" OnRowDeleted="gvServiceProvider_RowDeleted" CssClass="table table-striped table-bordered">
                <HeaderStyle CssClass="table-header table-active" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-outline-danger mb-3" />
                    <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-outline-primary mb-3" />

                    <asp:BoundField DataField="TA_ID" HeaderText="TA ID" SortExpression="TA_ID" InsertVisible="False" ReadOnly="True">
                        <HeaderStyle CssClass="larger-header" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="SPName" HeaderText="Create by" SortExpression="SPName" ReadOnly="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>"
                SelectCommand="SELECT TA.*, SP.Name AS SPName FROM [TenantAdmin] TA LEFT JOIN [ServiceProvider] SP ON TA.SP_ID = SP.SP_ID"
                DeleteCommand="DELETE FROM [TenantAdmin] WHERE [TA_ID] = @TA_ID"
                InsertCommand="INSERT INTO [TenantAdmin] ([Name], [Position], [Role], [ContactNumber], [Email]) VALUES (@Name, @Position, @Role, @ContactNumber, @Email, @Password, @SP_ID)"
                UpdateCommand="UPDATE [TenantAdmin] SET [Name] = @Name, [Position] = @Position, [Role] = @Role, [ContactNumber] = @ContactNumber, [Email] = @Email WHERE [TA_ID] = @TA_ID">

                <DeleteParameters>
                    <asp:Parameter Name="TA_ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Role" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Role" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="Button1" runat="server" Text="Create Tenant Admin" OnClick="Button1_Click" CssClass="btn btn-primary" />
        </div>
    </form>
</body>
</html>
