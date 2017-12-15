<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="Lab6Solution.ProtectedPages.RecordManagement.AddStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/SiteStyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Add Student Record</h1>
        <div class="form-group">
            <asp:Label ID="lbl1" runat="server" Text="Course: "></asp:Label>
            <asp:DropDownList ID="CourseList" runat="server" CssClass="form-control" OnSelectedIndexChanged="CourseList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label ID="lbl2" runat="server" Text="Student Number:"></asp:Label>
            <asp:TextBox ID="txtStudentNum" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="errMsg" runat="server" Visible="false" CssClass="error"></asp:Label>
            <asp:RequiredFieldValidator 
                id="RequiredField1" 
                runat="server" 
                ControlToValidate="txtStudentNum" 
                ErrorMessage="Student Number Required" 
                ForeColor="Red">
            </asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="lbl3" runat="server" Text="Student Name:"></asp:Label>
            <asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator 
                id="RequiredField2" 
                runat="server" 
                ControlToValidate="txtStudentName" 
                ErrorMessage="Student Name is Required" 
                ForeColor="Red">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator 
                ID="RegularExpValidator"  
                ValidationExpression="[a-zA-Z]+\s+[a-zA-Z]+" 
                ControlToValidate="txtStudentName" 
                CssClass="error" 
                Display="Dynamic"
                ErrorMessage="Must be in first_name last_name!" runat="server"/>
        </div>
        <div class="form-group">
            <asp:Label ID="lbl4" runat="server" Text="Grade"></asp:Label>
            <asp:TextBox ID="txtStudentGrade" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator 
                id="RequiredField3" 
                runat="server" 
                ControlToValidate="txtStudentGrade" 
                ErrorMessage="Student Grade is Required" 
                ForeColor="Red">
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <asp:Button ID="btn_AddToCourse" runat="server" Text="Submit" CssClass="btn-primary" OnClick="btn_AddToCourse_Click"/>
    <br />
    <br />
    <p>The Selected course has the following student records:</p>
    <asp:Table ID="studentTable" runat="server" CssClass="table">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Grade</asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableHeaderRow>
     </asp:Table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        $(document).ready(function () {
            $(".deleteCourse").on('click', function () {
                if (!confirm("Selected student record will be deleted!"))
                    return false;
            });
        });
    </script>
</asp:Content>