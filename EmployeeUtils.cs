using Employee_Management_MVC_Application.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Employee_Management_MVC_Application.Data_Access_Layer_DAL_
{
    public class EmployeeUtils : IEmployeeUtils, IDisposable
    {
        private static SqlConnection cn;

        //call Connection class method GetConnection() to establish connection
        static EmployeeUtils()
        {
            cn = Connection.GetConnection();
        }
        public string AddEmployee(Employee emp)
        {
            String? msg;
           SqlTransaction tx = cn.BeginTransaction();
            try
            {
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = tx;
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.CommandText = "AddEmployee";
                /*
                 CREATE PROCEDURE [dbo].[AddEmployee]
	             @Name varchar(150),
	             @City varchar(80),
	             @Address varchar(150)
                 AS
                 BEGIN
	             INSERT INTO Employees VALUES(@Name,@City,@Address);
                 END
                 */
                cmdInsert.Parameters.AddWithValue("@Name", emp.Name);
                cmdInsert.Parameters.AddWithValue("@City", emp.City);
                cmdInsert.Parameters.AddWithValue("@Address", emp.Address);
                cmdInsert.ExecuteNonQuery();
                tx.Commit();
                msg = "Employee Added Successfully!!!";
            }
            catch (Exception) {
                tx.Rollback();
                msg = "Employee Not Added, Unsuccussefull Operation!!!";
                throw new Exception(msg);
            }
            return msg;
        }

        public string DeleteEmployee(int id)
        {
            String? msg;
            SqlTransaction tx = cn.BeginTransaction();
            try
            {
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = tx;
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.CommandText = "DeleteEmployee";

                /*
                 CREATE PROCEDURE [dbo].[DeleteEmployee]
                 @Id int
                 AS
                 BEGIN
                 DELETE FROM Employees WHERE Id=@Id;
                 END
                 */
                cmdInsert.Parameters.AddWithValue("@Id", id);
                cmdInsert.ExecuteNonQuery();
                tx.Commit();
                msg = "Employee Delete Successfully!!!";
            }
            catch (Exception)
            {
                tx.Rollback();
                msg = "Employee Not Delete, Unsuccussefull Operation!!!";
                throw new Exception(msg);
            }
            return msg;
        }


        //close connection in Dispose method so that connection cannot leak
        public void Dispose()
        {
            cn.Close();
        }

        public List<Employee> GetAllEmployees()
        {
           List<Employee> list = new List<Employee>();  
          
            try
            {
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
               
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.CommandText = "GetAllEmployees";

                /*
                 CREATE PROCEDURE [dbo].[GetAllEmployees]
                 AS
                 BEGIN
	             SELECT * FROM Employees;
                 END
                 */

                using (SqlDataReader reader = cmdInsert.ExecuteReader())
                {
                    
                    
                    
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = Convert.ToInt32(reader["Id"]);
                        employee.Name = Convert.ToString(reader["Name"]);
                        employee.City = Convert.ToString(reader["City"]);
                        employee.Address = Convert.ToString(reader["Address"]);
                        list.Add(employee);
                    }
                    
                }
               return list;
                
            }
            catch (Exception)
            {
                throw new Exception("Invalid Operation!!");
             
            }
          
        }

        public Employee GetEmployee(int id)
        {
            try
            {
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;

                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.CommandText = "GetEmployee";
                /*
                 CREATE PROCEDURE [dbo].[GetEmployee]
	             @Id int
                 AS
                 BEGIN
	             SELECT * FROM Employees WHERE Id=@Id;
                 END
                 */
                cmdInsert.Parameters.AddWithValue("@Id", id);
                Employee employee = new Employee();
                using (SqlDataReader reader = cmdInsert.ExecuteReader())
                { 

                    if (reader.Read())
                    {
                        
                        employee.Id = Convert.ToInt32(reader["Id"]);
                        employee.Name = Convert.ToString(reader["Name"]);
                        employee.City = Convert.ToString(reader["City"]);
                        employee.Address = Convert.ToString(reader["Address"]);
                      
                    }
                }
                return employee;

            }
            catch (Exception)
            {
                throw new Exception("Employee Not found");

            }
        }

        public String UpdateEmployee(Employee emp)
        {
            string msg;
            SqlTransaction tx = cn.BeginTransaction();
            try
            {
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = tx;
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.CommandText = "EditEmployee";
                /*
                 CREATE PROCEDURE [dbo].[EditEmployee]
                 @Id int,
	             @Name varchar(150),
	             @City varchar(80),
	             @Address varchar(150)
                 AS
                 BEGIN
	             UPDATE Employees SET Name=@Name,City=@City,Address=@Address WHERE Id=@Id;
                 END
                 */
                cmdInsert.Parameters.AddWithValue("@Id", emp.Id);
                cmdInsert.Parameters.AddWithValue("@Name", emp.Name);
                cmdInsert.Parameters.AddWithValue("@City", emp.City);
                cmdInsert.Parameters.AddWithValue("@Address", emp.Address);
                cmdInsert.ExecuteNonQuery();
                tx.Commit();
                msg = "Employee update Successfully!!!";
            }
            catch (Exception)
            {
                tx.Rollback();
                msg = "Employee Not updated, Unsuccussefull Operation!!!";
                throw new Exception(msg);
            }
            return msg;
        }
    }
}
