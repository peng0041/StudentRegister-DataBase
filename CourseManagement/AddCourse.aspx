<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="Lab6Solution.ProtectedPages.CourseManagement.AddCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/SiteStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1 class="h1">Add New Courses</h1>
        <div class="form-group">
            <asp:Label ID="lbl1" runat="server" Text="Course Number:"></asp:Label>
            <asp:TextBox ID="txtCourseNumber" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="err" runat="server" CssClass="error" Visible="false"></asp:Label>
            <asp:RequiredFieldValidator 
                id="RequiredField1" 
                runat="server" 
                ControlToValidate="txtCourseNumber" 
                ErrorMessage="Course Number is Required" 
                ForeColor="Red">
            </asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="lbl2" runat="server" Text="Course Name:"></asp:Label>
            <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator 
                id="RequiredField2" 
                runat="server" 
                ControlToValidate="txtCourseName" 
                ErrorMessage="Course Name is Required" 
                ForeColor="Red">
            </asp:RequiredFieldValidator>
        </div>   
        <asp:Button ID="btn_SubmitCourse" runat="server" Text="Submit Course Information" CssClass="btn-primary" OnClick="btn_SubmitCourse_Click" />
        <br />
        <br />
        <p>The Following courses are currently in the system.</p>
        <asp:Table ID="courseTable" runat="server" CssClass="table">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell><a href="AddCourse.aspx?sort=code">Code</a></asp:TableHeaderCell>
                <asp:TableHeaderCell><a href="AddCourse.aspx?sort=title">Course Title</a></asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        $(document).ready(function () {
            $(".deleteCourse").on('click', function () {
                if (!confirm("Selected course and its student records will be deleted!"))
                    return false;
            });
        });
    </script>
</asp:Content>
