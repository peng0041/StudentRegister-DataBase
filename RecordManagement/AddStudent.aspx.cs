using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab6Solution.App_Code.StudentRecordDal;

namespace Lab6Solution.ProtectedPages.RecordManagement
{
    public partial class AddStudent : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");
            topMenu.Items[1].Enabled = true;

            if (!IsPostBack)
            {
                var courses = GetCourses();

                courses.Sort((c1, c2) => c1.Code.CompareTo(c2.Code));
                foreach (Course course in courses)
                {
                    string format = course.Code + " " + course.Title;
                    CourseList.Items.Add(format);
                }
                txtStudentNum.ReadOnly = false;
                txtStudentName.ReadOnly = false;
                CourseList.Enabled = true;
            }
            string action = Request.Params["action"];
            ActionHandler(action);

            ShowStudentRecords();
        }

        protected List<Course> GetCourses()
        {
            using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
            {
                List<Course> courses = entityContext.Courses.ToList<Course>();
                if (courses != null)
                {
                    return courses;
                }
            }
            return null;
        }
        protected void CourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowStudentRecords();
        }

        protected void ActionHandler(string action)
        {
            string courseId = Request.Params["course"] as String;
            string studentId = Request.Params["id"] as String;

            using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
            {
                Course course = (from c in entityContext.Courses
                                 where c.Code == courseId
                                 select c).FirstOrDefault<Course>();

                if (action == "edit")
                {
                    AcademicRecord record = (from c in course.AcademicRecords
                                             where c.StudentId == studentId
                                             select c).FirstOrDefault<AcademicRecord>();

                    CourseList.SelectedValue = course.Code + " " + course.Title;
                    txtStudentName.Text = record.Student.Name;
                    txtStudentNum.Text = record.Student.Id;
                    txtStudentName.ReadOnly = true;
                    txtStudentNum.ReadOnly = true;
                    CourseList.Enabled = false;
                }
                else if (action == "delete")
                {
                    AcademicRecord record = (from c in course.AcademicRecords
                                             where c.StudentId == studentId
                                             select c).FirstOrDefault<AcademicRecord>();
                    course.AcademicRecords.Remove(record);
                    entityContext.SaveChanges();
                    Response.Redirect("AddStudent.aspx");
                }
            }
        }

        protected void btn_AddToCourse_Click(object sender, EventArgs e)
        {
            string action = Request.Params["action"];
            ActionHandler(action);

            String studentName = txtStudentName.Text.Trim();
            String studentID = txtStudentNum.Text.Trim();

            String[] courseString = CourseList.SelectedValue.Split();
            string courseCode = courseString[0];

            using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
            {
                // TODO: Add edit actions
                if (action == "edit")
                {
                    string studentId = Request.Params["id"] as String;
                    string courseId = Request.Params["course"] as String;
                    Course editCourse = (from c in entityContext.Courses
                                         where c.Code == courseId
                                         select c).FirstOrDefault<Course>();

                    if (editCourse != null)
                    {
                        AcademicRecord record = (from c in editCourse.AcademicRecords
                                                 where c.StudentId == studentId
                                                 select c).FirstOrDefault<AcademicRecord>();

                        record.Grade = int.Parse(txtStudentGrade.Text);
                        entityContext.Entry(record).State = System.Data.Entity.EntityState.Modified;
                        entityContext.SaveChanges();
                        Response.Redirect("AddStudent.aspx");
                    }

                }
                else
                {
                    Course course = (from c in entityContext.Courses
                                     where c.Code == courseCode.Trim()
                                     select c).FirstOrDefault<Course>();

                    string format = course.Code + " " + course.Title;
                    if (CourseList.SelectedValue == format)
                    {
                        int studentGrade = int.Parse(txtStudentGrade.Text.Trim());

                        Student student = (from c in entityContext.Students
                                           where c.Id == studentID
                                           select c).FirstOrDefault<Student>();

                        if (student != null)
                        {
                            AcademicRecord record = (from c in student.AcademicRecords
                                                     where c.CourseCode == course.Code
                                                     select c).FirstOrDefault<AcademicRecord>();

                            if (course.AcademicRecords.Contains(record))
                            {
                                errMsg.Text = "The system already has a record of this student for the selected course";
                                errMsg.Visible = true;
                            }
                            else
                            {
                                AcademicRecord newRecord = new AcademicRecord();
                                newRecord.Grade = studentGrade;
                                newRecord.Student = student;
                                newRecord.Course = course;
                                course.AcademicRecords.Add(newRecord);
                                entityContext.SaveChanges();
                                Response.Redirect("AddStudent.aspx");
                            }
                        }
                        else
                        {
                            Student newStudent = new Student();
                            newStudent.Name = studentName;
                            newStudent.Id = studentID;
                            AcademicRecord newRecord = new AcademicRecord();
                            newRecord.Grade = studentGrade;
                            newRecord.Student = newStudent;
                            newRecord.Course = course;
                            course.AcademicRecords.Add(newRecord);
                            entityContext.SaveChanges();
                            Response.Redirect("AddStudent.aspx");
                        }
                    }
                }
            }
        }

        protected void ShowStudentRecords()
        {
            var courses = GetCourses();

            for (int i = studentTable.Rows.Count - 1; i > 0; i--)
            {
                studentTable.Rows.RemoveAt(i);
            }
            foreach (Course course in courses)
            {
                string format = course.Code + " " + course.Title;
                if (CourseList.SelectedValue == format)
                {
                    using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
                    {
                        AcademicRecord records = (from c in entityContext.AcademicRecords
                                                  where c.CourseCode == course.Code
                                                  select c).FirstOrDefault<AcademicRecord>();
                        if (records != null)
                        {
                            foreach (AcademicRecord record in records.Course.AcademicRecords)
                            {
                                TableRow studentRow = new TableRow();
                                TableCell idCell = new TableCell();
                                TableCell nameCell = new TableCell();
                                TableCell gradeCell = new TableCell();
                                TableCell cell = new TableCell();

                                idCell.Text = record.Student.Id;
                                nameCell.Text = record.Student.Name;
                                gradeCell.Text = record.Grade.ToString();
                                cell.Text = "<a href='AddStudent.aspx?action=edit&id=" + record.Student.Id + "&course=" + course.Code + "'/>Change Grade</a>"
                                    + " | <a class='deleteCourse' href='AddStudent.aspx?action=delete&id=" + record.Student.Id + "&course=" + course.Code + "'/>Delete</a>";

                                studentRow.Cells.Add(idCell);
                                studentRow.Cells.Add(nameCell);
                                studentRow.Cells.Add(gradeCell);
                                studentRow.Cells.Add(cell);
                                studentTable.Rows.Add(studentRow);
                            }
                        }
                    }
                }
            }
        }
    }
}