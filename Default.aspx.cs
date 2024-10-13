using HREmployeeApp.Models;
using HREmployeeApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace HREmployeeApp
{
    public partial class _Default : Page
    {
        private IEmployeeRepository _employeeRepository;
        public _Default()
        {
            _employeeRepository = new EmployeeRepository();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeeGrid();
            }
        }
        private void BindEmployeeGrid()
        {
            List<Employee> employees = _employeeRepository.GetAllEmployees().ToList();

            EmployeeGridView.DataSource = employees;
            EmployeeGridView.DataBind();
        }

        // Section: Search Employee by ID
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string employeeID = txtEmployeeID.Text.Trim();
            var employee = _employeeRepository.GetEmployeeById(employeeID);

            if (employee != null)
            {
                List<Employee> result = new List<Employee> { employee };
                EmployeeGridView.DataSource = result;
            }
            else
            {
                EmployeeGridView.DataSource = null;
            }
            EmployeeGridView.DataBind();
        }

        // Section: Edit Employee
        protected void EmployeeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmployeeGridView.EditIndex = e.NewEditIndex;
            BindEmployeeGrid();
        }

        // Section: Update Employee
        protected void EmployeeGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int employeeID = Convert.ToInt32(EmployeeGridView.DataKeys[e.RowIndex].Value);
            Employee employee = new Employee
            {
                EmployeeId = employeeID,
                FirstName = (EmployeeGridView.Rows[e.RowIndex].FindControl("txtFirstName") as TextBox).Text,
                LastName = (EmployeeGridView.Rows[e.RowIndex].FindControl("txtLastName") as TextBox).Text,
                Division = (EmployeeGridView.Rows[e.RowIndex].FindControl("txtDivision") as TextBox).Text,
                Building = (EmployeeGridView.Rows[e.RowIndex].FindControl("txtBuilding") as TextBox).Text,
                Title = (EmployeeGridView.Rows[e.RowIndex].FindControl("txtTitle") as TextBox).Text,
                Room = (EmployeeGridView.Rows[e.RowIndex].FindControl("txtRoom") as TextBox).Text
            };
            _employeeRepository.UpdateEmployee(employee);

            EmployeeGridView.EditIndex = -1;
            BindEmployeeGrid();
        }

        // Section: Cancel Edit
        protected void EmployeeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EmployeeGridView.EditIndex = -1;
            BindEmployeeGrid();
        }

        // Section: Delete Employee
        protected void EmployeeGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string employeeID = EmployeeGridView.DataKeys[e.RowIndex].Value.ToString();
            _employeeRepository.DeleteEmployee(employeeID);
            BindEmployeeGrid();
        }

        // Section: Add New Employee
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee newEmployee = new Employee
            {
                FirstName = txtAddFirstName.Text,
                LastName = txtAddLastName.Text,
                Division = txtAddDivision.Text,
                Building = txtAddBuilding.Text,
                Title = txtAddTitle.Text,
                Room = txtAddRoom.Text
            };

            _employeeRepository.AddEmployee(newEmployee);
            ClearAddEmployeeForm();
            BindEmployeeGrid();
        }

        private void ClearAddEmployeeForm()
        {
            txtAddFirstName.Text = "";
            txtAddLastName.Text = "";
            txtAddDivision.Text = "";
            txtAddBuilding.Text = "";
            txtAddTitle.Text = "";
            txtAddRoom.Text = "";
            addEmployeeForm.Visible = false;
        }
        protected void btnImportXML_Click(object sender, EventArgs e)
        {
            if (fileUploadXml.HasFile)
            {
                try
                {
                    // Parse the uploaded XML file
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(fileUploadXml.PostedFile.InputStream);

                    // Parse employee nodes
                    XmlNodeList employeeNodes = xmlDoc.SelectNodes("//Employees/Employee");

                    if (employeeNodes != null && employeeNodes.Count > 0)
                    {

                        foreach (XmlNode employeeNode in employeeNodes)
                        {
                            try
                            {
                                Employee newEmployee = new Employee
                                {
                                    FirstName = employeeNode["FirstName"].InnerText,
                                    LastName = employeeNode["LastName"].InnerText,
                                    Division = employeeNode["Division"].InnerText,
                                    Building = employeeNode["Building"].InnerText,
                                    Title = employeeNode["Title"].InnerText,
                                    Room = employeeNode["Room"].InnerText
                                };

                                _employeeRepository.AddEmployee(newEmployee);

                            }
                            catch (Exception ex)
                            {
                                lblMessage.Text += $"Failed to insert FirstName: {employeeNode["FirstName"].InnerText}. Error: {ex.Message}<br/>";
                            }
                        }
                        BindEmployeeGrid();

                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "XML data imported successfully!";

                    }
                    else
                    {
                        lblMessage.Text = "No valid employee records found in the XML file.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
            else
            {
                lblMessage.Text = "Please select an XML file to upload.";
            }
        }

    }
}