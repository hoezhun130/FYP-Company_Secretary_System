<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceProviderPage.aspx.cs" Inherits="FYP.ServiceProviderPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Provider</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvServiceProvider" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TA_ID">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                    <asp:BoundField DataField="TA_ID" HeaderText="TA_ID" SortExpression="TA_ID" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                    <asp:BoundField DataField="SPName" HeaderText="Create by" SortExpression="SPName" ReadOnly="True"/>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" 
                SelectCommand="SELECT TA.*, SP.Name AS SPName FROM [TenantAdmin] TA LEFT JOIN [ServiceProvider] SP ON TA.SP_ID = SP.SP_ID" 
                DeleteCommand="DELETE FROM [TenantAdmin] WHERE [TA_ID] = @TA_ID" 
                InsertCommand="INSERT INTO [TenantAdmin] ([Name], [Position], [Role], [ContactNumber], [Email], [Password], [SP_ID]) VALUES (@Name, @Position, @Role, @ContactNumber, @Email, @Password, @SP_ID)" 
                UpdateCommand="UPDATE [TenantAdmin] SET [Name] = @Name, [Position] = @Position, [Role] = @Role, [ContactNumber] = @ContactNumber, [Email] = @Email, [Password] = @Password WHERE [TA_ID] = @TA_ID">

                <DeleteParameters>
                    <asp:Parameter Name="TA_ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Role" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="SP_ID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Role" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="TA_ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="Button1" runat="server" Text="Create Tenant Admin" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
