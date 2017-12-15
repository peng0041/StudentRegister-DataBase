<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="Lab6Solution.ProtectedPages.UserManagement.AddEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/SiteStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row vertical-margin">
            <div class="col-md-10 col-md-offset-1">
                <h1>Add New Employee</h1>
            </div>
        </div>

        <div class="row vertical-margin  form-group">
            <div class="col-md-3 col-md-offset-1">
                <label for="txtEmployeeName" id="lblEmployeeName">Name: </label>
            </div>
            <div class="col-md-6">
                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmployeeName" CssClass="error" Display="Static" ErrorMessage="Required!" runat="server" />
            </div>
        </div>
        <div class="row vertical-margin  form-group">
            <div class="col-md-3 col-md-offset-1">
                <label for="txtUserName" id="lblUserName">User Name: </label>
            </div>
            <div class="col-md-6">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtUserName" CssClass="error" Display="Static" ErrorMessage="Required!" runat="server" />
            </div>
            <div class="col-md-2">
            <asp:Label runat="server" ID="errMsg" CssClass="error"></asp:Label>
            </div>
        </div>               

        <div class="row vertical-margin form-group">
            <div class="col-md-3 col-md-offset-1">
                <label for="txtPassword" id="lblPassword">Password:</label>
            </div>
            <div class="col-md-6">
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPassword" CssClass="error" Display="Static" ErrorMessage="Required!" runat="server" />
            </div>
        </div>

        <div class="row vertical-margin form-group">
            <div class="col-md-3 col-md-offset-1">
                <label for="radioList" id="lblRadio">Roles: </label>
            </div>
            <div class="col-md-6">
                <asp:CheckBoxList ID="checkList" runat="server">
                    <asp:ListItem Text="Department Chair" Value="Department Chair"></asp:ListItem>
                    <asp:ListItem Text="Coordinator" Value="Coordinator"></asp:ListItem>
                    <asp:ListItem Text="Professor" Value="Instructor"></asp:ListItem>
                </asp:CheckBoxList>              
            </div>
        </div>
        <div class="row vertical-margin form-group">
            <div class="col-md-3 col-md-offset-1">
                <asp:Button runat="server" ID="btn_Submit" OnClick="btnSubmit_Click" Text="Save" CssClass="btn btn-primary" />
            </div>
        </div>
            <div class="col-md-3 col-md-offset-1">
                <p>The following employees are currently in the system.</p>
            </div>            
        
        <div class="row vertical-margin form-group">
            <asp:Table ID="employeeTable" runat="server" CssClass="table">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Name"></asp:TableHeaderCell>
                    <asp:TableHeaderCell Text="User Name"></asp:TableHeaderCell>
                    <asp:TableHeaderCell Text="Role"></asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
