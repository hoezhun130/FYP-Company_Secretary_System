<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceProviderPage.aspx.cs" Inherits="FYP.ServiceProviderPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
<asp:GridView ID="gvTenantAdmins" runat="server" AutoGenerateColumns="False" DataKeyNames="TA_ID"
    OnRowEditing="gvTenantAdmins_RowEditing" OnRowUpdating="gvTenantAdmins_RowUpdating" 
    OnRowCancelingEdit="gvTenantAdmins_RowCancelingEdit" OnRowDeleting="gvTenantAdmins_RowDeleting">
    <Columns>
        <asp:BoundField DataField="TA_ID" HeaderText="TA_ID" InsertVisible="False" ReadOnly="True" SortExpression="TA_ID" />
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
        <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
        <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
        <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
        <asp:BoundField DataField="SP_ID" HeaderText="SP_ID" SortExpression="SP_ID" />
    </Columns>
</asp:GridView>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" SelectCommand="SELECT * FROM [Category]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
