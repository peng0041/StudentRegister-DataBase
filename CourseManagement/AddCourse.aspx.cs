using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab6Solution.App_Code.StudentRecordDal;

namespace Lab6Solution.ProtectedPages.CourseManagement
{
    public partial class AddCourse : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");
            topMenu.Items[0].Enabled = true;

            if (!IsPostBack)
            {
                txtCourseName.Text = "";
                txtCourseNumber.Text = "";
                txtCourseNumber.ReadOnly = false;
            }

            string action = Request.Params["action"];
            string courseCode = Request.Params["id"] as String;
            CourseInformation(action, courseCode);
            ShowCourseInfo();
        }

        protected void btn_SubmitCourse_Click(object sender, EventArgs e)
        {
            string courseCode = txtCourseNumber.Text.ToUpper().Trim();
            string action = Request.Params["action"];
            using (StudentRecordEntities1 entity = new StudentRecordEntities1())
            {
                if (action == "edit")
                {
                    string id = Request.Params["id"];
                    Course course = (from c in entity.Courses
                                     where c.Code == id
                                     select c).FirstOrDefault<Course>();
                    if (course != null)
                    {
                        course.Title = txtCourseName.Text;
                        entity.Entry(course).State = System.Data.Entity.EntityState.Modified;
                        entity.SaveChanges();
                        Response.Redirect("AddCourse.aspx");
                    }
                }
                else
                {
                    Course course = (from c in entity.Courses
                                     where c.Code == courseCode
                                     select c).FirstOrDefault<Course>();
                    if (course != null)
                    {
                        if (course.Code == courseCode)
                        {
                            err.Text = "Course with that code already exists!";
                            err.Visible = true;
                        }
                    }
                    else
                    {
                        Course newCourse = new Course();
                        newCourse.Code = courseCode;
                        newCourse.Title = txtCourseName.Text;

                        entity.Courses.Add(newCourse);
                        entity.SaveChanges();
                        err.Visible = false;
                        Response.Redirect("AddCourse.aspx");
                    }
                }
            }
        }

        protected void CourseInformation(string action, string id)
        {
            using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
            {
                Course course = (from c in entityContext.Courses
                                 where c.Code == id
                                 select c).FirstOrDefault<Course>();
                if (course != null)
                {
                    if (action == "delete")
                    {
                        for (int i = course.AcademicRecords.Count() - 1; i >= 0; i--)
                        {
                            var ar = course.AcademicRecords.ElementAt<AcademicRecord>(i);
                            course.AcademicRecords.Remove(ar);
                        }
                        entityContext.Courses.Remove(course);
                        entityContext.SaveChanges();
                        Response.Redirect("AddCourse.aspx");
                    }
                    else if (action == "edit")
                    {
                        txtCourseNumber.Text = course.Code;
                        txtCourseNumber.ReadOnly = true;
                    }
                }
            }
        }

        protected void ShowCourseInfo()
        {
            for (int i = courseTable.Rows.Count - 1; i > 0; i--)
            {
                courseTable.Rows.RemoveAt(i);
            }
            using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
            {
                List<Course> courses = entityContext.Courses.ToList<Course>();
                if (courses != null)
                {
                    foreach (Course course in courses)
                    {
                        TableRow CourseRow = new TableRow();
                        TableCell CourseNumber = new TableCell();
                        TableCell CourseName = new TableCell();
                        TableCell Cell = new TableCell();

                        CourseNumber.Text = course.Code;
                        CourseName.Text = course.Title;
                        Cell.Text = "<a href='AddCourse.aspx?action=edit&id=" + course.Code + "'>Edit</a>"
                            + " | <a class='deleteCourse' href='AddCourse.aspx?action=delete&id=" + course.Code + "'>Delete</a>";
                        CourseRow.Cells.Add(CourseNumber);
                        CourseRow.Cells.Add(CourseName);
                        CourseRow.Cells.Add(Cell);
                        courseTable.Rows.Add(CourseRow);
                    }
                }
            }
        }

    }
}