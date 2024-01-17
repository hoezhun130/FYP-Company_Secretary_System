<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientAdminPage.aspx.cs" Inherits="FYP.ClientAdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Admin Page</title>

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
            <h3 class="text-left">
                <i class="fas fa-user mr-2"></i><b>Create Client User Account Dashboard</b>
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

            <asp:GridView ID="GridView1" runat="server" DataKeyNames="CU_ID" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CU_ID" HeaderText="Client User ID" InsertVisible="False" ReadOnly="True" SortExpression="CU_ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                    <asp:BoundField DataField="ICNumber" HeaderText="ICNumber" SortExpression="ICNumber" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="CAName" HeaderText="Created By" SortExpression="CAName" ReadOnly="True"/>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" 
                DeleteCommand="BEGIN TRANSACTION;
                DELETE FROM [Subscription] WHERE [CU_ID] = @CU_ID;
                DELETE FROM [Comment] WHERE [CU_ID] = @CU_ID;
                DELETE FROM [Document] WHERE [CU_ID] = @CU_ID;
                DELETE FROM [Category] WHERE [CU_ID] = @CU_ID;
                DELETE FROM [ClientUser] WHERE [CU_ID] = @CU_ID;
                COMMIT;" 
                InsertCommand="INSERT INTO [ClientUser] ([Name], [Position], [Department], [CompanyName], [ICNumber], [ContactNumber], [Email], [Password]) VALUES (@Name, @Position, @Department, @CompanyName, @ICNumber, @ContactNumber, @Email, @Password)" 
                SelectCommand="SELECT CU.*, CA.Name AS CAName FROM [ClientUser] CU LEFT JOIN [ClientAdmin] CA ON CU.CA_ID = CA.CA_ID" 
                UpdateCommand="UPDATE [ClientUser] SET [Name] = @Name, [Position] = @Position, [Department] = @Department, [CompanyName] = @CompanyName, [ICNumber] = @ICNumber, [ContactNumber] = @ContactNumber, [Email] = @Email WHERE [CU_ID] = @CU_ID">
                <DeleteParameters>
                    <asp:Parameter Name="CU_ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Department" Type="String" />
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="ICNumber" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Department" Type="String" />
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="ICNumber" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />

                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="Button1" runat="server" Text="Create Client User" OnClick="Button1_Click" CssClass="btn btn-primary" />
            <br />

        </div>
    </form>
</body>
</html>
