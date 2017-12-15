using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab6Solution.App_Code.StudentRecordDal;

namespace Lab6Solution.ProtectedPages.UserManagement
{
    public partial class AddEmployee : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
            btnHome.Enabled = true;

            if (!IsPostBack)
            {
            }

            showEmployees();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtEmployeeName.Text;
            string userName = txtUserName.Text.ToLower();
            string password = txtPassword.Text;

            using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
            {
                Employee employee = (from em in entityContext.Employees
                                     where em.UserName == userName
                                     select em).FirstOrDefault<Employee>();

                if (employee == null)
                {
                    Employee em = new Employee();
                    em.UserName = userName;
                    em.Name = name;
                    em.Password = password;

                    List<ListItem> selected = new List<ListItem>();
                    foreach (ListItem item in checkList.Items)
                        if (item.Selected)
                        {
                            Role r = (from rm in entityContext.Roles
                                      where rm.Role1.ToLower() == item.Value.ToLower()
                                      select rm).FirstOrDefault<Role>();
                            if (r != null)
                            {
                                r.Employees.Add(em);
                            }
                        }
                    entityContext.SaveChanges();

                    Response.Redirect("AddEmployee.aspx");
                }
                else
                {
                    errMsg.Text = "Our system has indicated that username is already in use, please choose another";
                }
            }
        }

        protected void showEmployees()
        {           
            for (int i = employeeTable.Rows.Count - 1; i > 0; i--)
            {
                employeeTable.Rows.RemoveAt(i);
            }
            using (StudentRecordEntities1 entityContext = new StudentRecordEntities1())
            {
                var employees = entityContext.Employees.ToList<Employee>();
                foreach (Employee employee in employees)
                {

                    TableRow employeeRow = new TableRow();
                    TableCell nameCell = new TableCell();
                    TableCell userCell = new TableCell();
                    TableCell roleCell = new TableCell();

                    nameCell.Text = employee.Name;
                    userCell.Text = employee.UserName;

                    string role = string.Empty;
                    foreach (Role r in employee.Roles)
                    {
                        role += r.Role1 + ", ";
                    }
                    roleCell.Text = role;

                    employeeRow.Cells.Add(nameCell);
                    employeeRow.Cells.Add(userCell);
                    employeeRow.Cells.Add(roleCell);

                    employeeTable.Rows.Add(employeeRow);
                }

            }

        }
    }
}