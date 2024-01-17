<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UserSite/TenantHeader.Master" CodeBehind="TenantCompanyList.aspx.cs" Inherits="FYP.UserSite.CompanyList" %>


<asp:Content runat="server" ContentPlaceHolderID="Content">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Tenant Company List</title>

        <!--Bootstrap CSS -->
        <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />


        <!-- jQuery, Popper.js, and Bootstrap JavaScript -->
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

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



<div class="container-fluid text-white" style="height: 350px; background-color: #1d2f43;">
    <div class="row align-items-start justify-content-start" style="height: 87%;">
        <div class="col-12">
            <asp:Button ID="Button1" runat="server" Text="< Back" OnClick="Button1_Click" CssClass="btn mt-3 ml-3 text-white underline-with-arrow" />
        </div>
        <div class="col text-center">
            <h1 class="pt-3 display-2 font-weight-normal">Company</h1>
            <hr class="bg-white" style="height: 4px; width: 100px;" />
            <p class="mt-3 font-weight-normal">
                Please select a company from the options provided and proceed to upload
                <br />
                your digital documents.
            </p>
        </div>
    </div>
</div>



        <br />

        <div class="container">




            <div class="form-inline">
                <h5>List of Companies</h5>
            </div>
            <hr class="bg-dark" />
            <br />



            <!-- Company listing -->
            <div class="row">
                <asp:Repeater ID="rptCompanies" runat="server" OnItemCommand="Company_Command">
                    <ItemTemplate>
                        <div class="col-md-3 mb-4 mt-3">

                            <asp:LinkButton ID="lnkCompany" runat="server" CommandName="SelectCompany" CommandArgument='<%# Eval("CompanyName") %>' CssClass="company-item border p-3 mr-3">
                        <span><%# Eval("CompanyName") %></span>
                            </asp:LinkButton>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>



            <br />
            <br />
            <br />

        </div>
    </body>
    </html>

</asp:Content>
