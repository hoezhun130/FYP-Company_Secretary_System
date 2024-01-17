<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UserSite/TenantHeader.Master" CodeBehind="TenantUploadDocument.aspx.cs" Inherits="FYP.UserSite.TenantUploadDocument" %>


<asp:Content runat="server" ContentPlaceHolderID="Content">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Create Tenant Admin Page</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</head>
<script>
    function showNotification(message, redirectUrl) {
        $('#notificationMessage').text(message);
        $('#notificationModal').modal('show');

        // Redirect after modal is closed
        $('#notificationModal').on('hidden.bs.modal', function () {
            if (redirectUrl) {
                window.location.href = redirectUrl;
            }
        });
    }
</script>

<body>
    <div class="modal fade" id="notificationModal" tabindex="-1" role="dialog" aria-labelledby="notificationModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="notificationModalLabel">Notification</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p id="notificationMessage"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>





<div class="mt-3 needs-validation" style="border-radius: 10px; position: relative;">

<div class="row">
    <div class="col-12">
        <asp:Button ID="Button1" runat="server" Text="< Back" OnClick="Button1_Click" CssClass="btn text-dark underline-with-arrow mt-2 ml-3" />
    </div>
</div>


        <div class="container mt-3 card p-3 mb-4" style="max-width: 500px;">
            <h3 class="text-left">
                <i class="fas fa-user mr-2 mb-3"></i><b>Upload Documents</b>
            </h3>

            <asp:Label ID="lblUploadDocument" runat="server" Text="Upload Document:" CssClass="font-weight-bold" />
            <asp:FileUpload ID="FileUploadDocument" runat="server" CssClass="form-control" Style="display: none;" />
            <div id="dropArea" class="mt-3 p-3 border rounded border-dashed text-center">
                <i class="fas fa-cloud-upload-alt fa-3x mb-2"></i>
                <p class="mb-1">Drag & Drop documents here</p>
                <p class="mb-0">or</p>
                <label id="FileUploadDocument2" class="btn btn-primary btn-sm mt-2">
                    Click to select
                </label>
            </div>



            <br />
            <asp:Label ID="lblUploadedBy" runat="server" Text="Uploaded By:" />
            <asp:TextBox ID="txtUploadedBy" runat="server" ReadOnly="true" CssClass="form-control" />
            <br />
            <asp:Label ID="lblDate" runat="server" Text="Date:" />
            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" CssClass="form-control" Text='<%# DateTime.Now.ToShortDateString() %>' />
            <br />
            <asp:Label ID="lblCompany" runat="server" Text="Company:" />
            <asp:DropDownList ID="ddlCompany" runat="server" Enabled="false" CssClass="form-control" />

            <br />
            <div class="row mt-3">
                <div class="col">
                    <asp:Button ID="btnSaveDocument" runat="server" Text="Save" OnClick="btnSaveDocument_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-secondary" />
                </div>
            </div>

            <asp:HiddenField ID="hiddenCategoryID" runat="server" />
   </div>
        </div>
 
    <script>
        // Function to handle file input change event
        function handleFileInput(event) {
            var files = event.target.files;
            var dropArea = document.getElementById('dropArea');

            // Update the drop area text to show the selected file names
            dropArea.innerHTML = 'Selected files: ' + Array.from(files).map(file => file.name).join(', ');
        }

        // Function to handle drag-and-drop events
        function handleDrop(event) {
            event.preventDefault();

            var files = event.dataTransfer.files;
            var dropArea = document.getElementById('dropArea');

            // Check if files were dropped
            if (files.length > 0) {
                // Assign the dropped file to the file input
                document.getElementById('<%= FileUploadDocument.ClientID %>').files = files;

                // Update the drop area text to show the dropped file names
                dropArea.innerHTML = 'Dropped files: ' + Array.from(files).map(file => file.name).join(', ');
            }
        }

        // Function to prevent the default behavior for drag-and-drop
        function handleDragOver(event) {
            event.preventDefault();
        }

        // Add event listeners for file input and drag-and-drop
        var fileInput = document.getElementById('<%= FileUploadDocument.ClientID %>');
        var dropArea = document.getElementById('dropArea');

        fileInput.addEventListener('change', handleFileInput);
        dropArea.addEventListener('drop', handleDrop);
        dropArea.addEventListener('dragover', handleDragOver);

        // Function to trigger file input click when the drop area is clicked
        dropArea.addEventListener('click', function () {
            fileInput.click();
        });
    </script>
</body>
</html>

</asp:Content>
