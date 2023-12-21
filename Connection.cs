using Microsoft.Data.SqlClient;

namespace Employee_Management_MVC_Application.Models
{
    public class Connection
    {
     public static SqlConnection GetConnection()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployeeDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            cn.Open();
            return cn;
        }
    }
}
