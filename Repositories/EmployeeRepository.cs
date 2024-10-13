using HREmployeeApp.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace HREmployeeApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string _connectionString;

        public EmployeeRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["HRDatabase"].ConnectionString;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("SELECT * FROM Employees", connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Division = reader["Division"].ToString(),
                                Building = reader["Building"].ToString(),
                                Title = reader["Title"].ToString(),
                                Room = reader["Room"].ToString()
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }
        public Employee GetEmployeeById(string employeeID)
        {
            Employee employee = null;

            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                string query = "SELECT EmployeeID, FirstName, LastName, Division, Building, Title, Room FROM Employees WHERE EmployeeID = :EmployeeID";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add(new OracleParameter("EmployeeID", employeeID));

                conn.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Division = reader["Division"].ToString(),
                        Building = reader["Building"].ToString(),
                        Title = reader["Title"].ToString(),
                        Room = reader["Room"].ToString()
                    };
                }
            }

            return employee;
        }

        public void AddEmployee(Employee employee)
        {
            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                conn.Open();

                // Query to get the maximum EmployeeID
                string getMaxIdQuery = "SELECT NVL(MAX(EmployeeID), 0) FROM Employees";
                OracleCommand getMaxIdCmd = new OracleCommand(getMaxIdQuery, conn);
                int nextEmployeeId = Convert.ToInt32(getMaxIdCmd.ExecuteScalar()) + 1;

                // Insert the new employee with the next available ID
                string insertQuery = @"
            INSERT INTO Employees 
            (EmployeeID, FirstName, LastName, Division, Building, Title, Room) 
            VALUES (:EmployeeID, :FirstName, :LastName, :Division, :Building, :Title, :Room)";

                OracleCommand cmd = new OracleCommand(insertQuery, conn);
                cmd.Parameters.Add(new OracleParameter("EmployeeID", nextEmployeeId));
                cmd.Parameters.Add(new OracleParameter("FirstName", employee.FirstName));
                cmd.Parameters.Add(new OracleParameter("LastName", employee.LastName));
                cmd.Parameters.Add(new OracleParameter("Division", employee.Division));
                cmd.Parameters.Add(new OracleParameter("Building", employee.Building));
                cmd.Parameters.Add(new OracleParameter("Title", employee.Title));
                cmd.Parameters.Add(new OracleParameter("Room", employee.Room));

                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateEmployee(Employee employee)
        {
            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                string query = "UPDATE Employees SET FirstName = :FirstName, LastName = :LastName, Division = :Division, Building = :Building, Title = :Title, Room = :Room WHERE EmployeeID = :EmployeeID";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add(new OracleParameter("FirstName", employee.FirstName));
                cmd.Parameters.Add(new OracleParameter("LastName", employee.LastName));
                cmd.Parameters.Add(new OracleParameter("Division", employee.Division));
                cmd.Parameters.Add(new OracleParameter("Building", employee.Building));
                cmd.Parameters.Add(new OracleParameter("Title", employee.Title));
                cmd.Parameters.Add(new OracleParameter("Room", employee.Room));
                cmd.Parameters.Add(new OracleParameter("EmployeeID", employee.EmployeeId));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(string employeeID)
        {
            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                string query = "DELETE FROM Employees WHERE EmployeeID = :EmployeeID";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add(new OracleParameter("EmployeeID", employeeID));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}