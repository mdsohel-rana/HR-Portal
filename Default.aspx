﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HREmployeeApp._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HR Employee Management</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            padding: 20px;
            max-width: 900px;
            margin: auto;
        }

        h2 {
            margin-top: 20px;
            margin-bottom: 10px;
        }

        form {
            display: flex;
            flex-direction: column;
            gap: 20px;
        }

        /* Styling for the search section */
        #search-section {
            display: flex;
            align-items: center;
            gap: 10px;
        }

            #search-section input {
                padding: 10px;
                width: 300px;
            }

        /* Styling for the Employee List */
        .gridview-container {
            margin: 20px auto;
            width: 100%;
            max-width: 900px;
        }

            .gridview-container .GridView {
                width: 100%;
                border-collapse: collapse;
            }

            .gridview-container th, .gridview-container td {
                padding: 10px;
                text-align: left;
                border: 1px solid #ddd;
            }

            .gridview-container th {
                background-color: #f2f2f2;
            }

            .gridview-container .GridView tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .gridview-container .GridView tr:hover {
                background-color: #f1f1f1;
            }

            .gridview-container input[type="text"] {
                width: 100%;
                padding: 6px;
            }

        /* Styling for Add Employee Form */
        #addEmployeeForm {
            margin-top: 20px;
            display: none;
            border: 1px solid #ddd;
            padding: 20px;
            border-radius: 5px;
            gap: 15px;
        }

            #addEmployeeForm label {
                display: block;
                margin-bottom: 5px;
            }

            #addEmployeeForm input {
                width: 100%;
                padding: 10px;
                margin-bottom: 15px;
                box-sizing: border-box;
            }

        .form-buttons {
            display: flex;
            gap: 10px;
        }

        /* Hide Add New Employee button when form is shown */
        #btnShowAddEmployee[style*="display: none;"] {
            display: none !important;
        }

        #btnShowAddEmployee {
            padding: 10px;
            width: 300px;
        }
    </style>

    <script type="text/javascript">
        function showAddEmployeeForm() {
            document.getElementById('addEmployeeForm').style.display = 'block';
            document.getElementById('btnShowAddEmployee').style.display = 'none';
        }

        function hideAddEmployeeForm() {
            document.getElementById('addEmployeeForm').style.display = 'none';
            document.getElementById('btnShowAddEmployee').style.display = 'block';
        }
    </script>
</head>
<body>
    <form id="form" runat="server">

        <!-- Search Employee Section -->
        <div id="search-section">
            <h2>Search Employee</h2>
            <asp:TextBox ID="txtEmployeeID" runat="server" placeholder="Enter Employee ID"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>

        <!-- Employee List Section -->
        <div id="employee-list">
            <h2>Employee List</h2>
            <div class="gridview-container">
                <asp:GridView ID="EmployeeGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="EmployeeID"
                    OnRowEditing="EmployeeGridView_RowEditing" OnRowUpdating="EmployeeGridView_RowUpdating"
                    OnRowCancelingEdit="EmployeeGridView_RowCancelingEdit" OnRowDeleting="EmployeeGridView_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" ReadOnly="True" />
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Division">
                            <ItemTemplate>
                                <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("Division") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDivision" runat="server" Text='<%# Eval("Division") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Building">
                            <ItemTemplate>
                                <asp:Label ID="lblBuilding" runat="server" Text='<%# Eval("Building") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBuilding" runat="server" Text='<%# Eval("Building") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Room">
                            <ItemTemplate>
                                <asp:Label ID="lblRoom" runat="server" Text='<%# Eval("Room") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRoom" runat="server" Text='<%# Eval("Room") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Section: Add New Employee Form -->
        <div id="addEmployeeForm" runat="server">
            <h2>Add New Employee</h2>
            <label>First Name:</label>
            <asp:TextBox ID="txtAddFirstName" runat="server"></asp:TextBox><br />
            <label>Last Name:</label>
            <asp:TextBox ID="txtAddLastName" runat="server"></asp:TextBox><br />
            <label>Division:</label>
            <asp:TextBox ID="txtAddDivision" runat="server"></asp:TextBox><br />
            <label>Building:</label>
            <asp:TextBox ID="txtAddBuilding" runat="server"></asp:TextBox><br />
            <label>Title:</label>
            <asp:TextBox ID="txtAddTitle" runat="server"></asp:TextBox><br />
            <label>Room:</label>
            <asp:TextBox ID="txtAddRoom" runat="server"></asp:TextBox><br />
            <div class="form-buttons">
                <asp:Button ID="btnAddEmployee" runat="server" Text="Save" OnClick="btnAddEmployee_Click" />
                <asp:Button ID="btnCancelAddEmployee" runat="server" Text="Cancel" OnClientClick="hideAddEmployeeForm(); return false;" />
            </div>
        </div>
        <asp:Button ID="btnShowAddEmployee" runat="server" Text="Add New Employee" OnClientClick="showAddEmployeeForm(); return false;" />
        <!-- Section: Import XML Data -->
        <div id="importXmlSection">
            <h2>Import Employee Data from XML</h2>
            <asp:FileUpload ID="fileUploadXml" runat="server" />
            <asp:Button ID="btnImportXml" runat="server" Text="Import" OnClick="btnImportXML_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
