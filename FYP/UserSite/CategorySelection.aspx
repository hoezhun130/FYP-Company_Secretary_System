<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategorySelection.aspx.cs" Inherits="FYP.UserSite.CategorySelection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .category-item img.category-icon {
            width: 50px; /* Or whatever size you prefer */
            height: auto; /* Maintain aspect ratio */
        }

        .category-item {
            display: inline-block;
            text-align: center;
            margin: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="rptCategories" runat="server" OnItemCommand="Category_Command">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkCategory" runat="server" CommandName="SelectCategory" CommandArgument='<%# Eval("CategoryID") %>' CssClass="category-item">
            <img src='<%# ResolveUrl("~/CategoryIcon/" + Eval("IconPath")) %>' alt='<%# Eval("CategoryName") %>' class="category-icon" />
            <span><%# Eval("CategoryName") %></span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
