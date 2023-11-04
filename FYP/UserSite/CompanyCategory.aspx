<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyCategory.aspx.cs" Inherits="FYP.UserSite.CompanyCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                        <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
            <h2>Categories for <asp:Literal ID="ltCompanyName" runat="server"></asp:Literal></h2>
            <asp:Repeater ID="rptCategories" runat="server">
                <ItemTemplate>
                    <div>
                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
