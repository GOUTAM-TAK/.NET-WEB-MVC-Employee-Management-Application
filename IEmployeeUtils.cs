using Employee_Management_MVC_Application.Models;

namespace Employee_Management_MVC_Application.Data_Access_Layer_DAL_
{
    public interface IEmployeeUtils
    {
         string AddEmployee(Employee emp);

         List<Employee> GetAllEmployees();

        Employee GetEmployee(int id);

        String UpdateEmployee(Employee emp);

        String DeleteEmployee(int id);

        
    }
}
