<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientAdminPage.aspx.cs" Inherits="FYP.ClientAdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CU_ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CU_ID" HeaderText="CU_ID" InsertVisible="False" ReadOnly="True" SortExpression="CU_ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                    <asp:BoundField DataField="ICNumber" HeaderText="ICNumber" SortExpression="ICNumber" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                    <asp:BoundField DataField="CA_ID" HeaderText="CA_ID" SortExpression="CA_ID" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" DeleteCommand="DELETE FROM [ClientUser] WHERE [CU_ID] = @CU_ID" InsertCommand="INSERT INTO [ClientUser] ([Name], [Position], [CompanyName], [ICNumber], [ContactNumber], [Email], [Password], [CA_ID]) VALUES (@Name, @Position, @CompanyName, @ICNumber, @ContactNumber, @Email, @Password, @CA_ID)" SelectCommand="SELECT * FROM [ClientUser]" UpdateCommand="UPDATE [ClientUser] SET [Name] = @Name, [Position] = @Position, [CompanyName] = @CompanyName, [ICNumber] = @ICNumber, [ContactNumber] = @ContactNumber, [Email] = @Email, [Password] = @Password, [CA_ID] = @CA_ID WHERE [CU_ID] = @CU_ID">
                <DeleteParameters>
                    <asp:Parameter Name="CU_ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="ICNumber" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="CA_ID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="ICNumber" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="CA_ID" Type="Int32" />
                    <asp:Parameter Name="CU_ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="Button1" runat="server" Text="Create Client User" OnClick="Button1_Click"/>
            <br />
            
        </div>
    </form>
</body>
</html>
