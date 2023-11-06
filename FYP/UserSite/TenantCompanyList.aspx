<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantCompanyList.aspx.cs" Inherits="FYP.UserSite.CompanyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
        <div>
            <!-- Company listing -->
            <asp:Repeater ID="rptCompanies" runat="server" OnItemCommand="Company_Command">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkCompany" runat="server" CommandName="SelectCompany" CommandArgument='<%# Eval("CompanyName") %>' CssClass="company-item">
                        <span><%# Eval("CompanyName") %></span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
