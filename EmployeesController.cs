using Employee_Management_MVC_Application.Data_Access_Layer_DAL_;
using Employee_Management_MVC_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_MVC_Application.Controllers
{
    public class EmployeesController : Controller
    {
        //create IEmployeeutils Interface reference
        private IEmployeeUtils empUtils;

        public EmployeesController()
        {
            //create instance of EmployeeUtills class[Implement class of IemployeeUtills interface] and refer by IEmployeeUtills interface reference
            this.empUtils = new EmployeeUtils();
        }



        // GET: Employees
        public ActionResult Index()
        {
            //get all employees 
       List<Employee> list =   empUtils.GetAllEmployees();
            
            return View(list);
        }

        // GET: Employees/GetEmployee/5
        public ActionResult GetEmployee(int id)
        {
            try
            {
             Employee emp =   empUtils.GetEmployee(id);
                //check emp found or not
                if(emp == null)
                {
               return  NotFound();
                }
                return View(emp);
            }
            catch
            {
             return   NotFound();
            }
        }

        // GET: EmployeesController/Create
        public ActionResult AddEmployee()
        {
           return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
  
        public ActionResult AddEmployee(Employee emp)
        {
            String msg;
            try
            {
       empUtils.AddEmployee(emp);

                msg = "Added Successfully!!";

                //use tempdata to display message on index page
                //use tempdata instead of viewbag bcoz of redirect 
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                msg = "Added Unsuccessfully!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index)); ;
            }
        }

        // GET: EmployeesController/EditEmployee/5
        public ActionResult EditEmployee(int id)
        {
            try
            {
                Employee emp = empUtils.GetEmployee(id);
                //check employee found or not
                if (emp == null)
                {
                    return NotFound();
                }
                //employee details pass to view
                return View(emp);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
   
        public ActionResult EditEmployee(int id, Employee emp)
        {
            String msg;
            try
            { //check id come from url and employee data come from jsson body are match or not
                //if employee id not match with url id then this is badrequest
                if(id != emp.Id)
                {
                    return BadRequest();
                }
             empUtils.UpdateEmployee(emp);

                //use tempdata to display message on index page
                //use tempdata instead of viewbag bcoz of redirect 
                msg = "Update Successfully!!";
                TempData.Add("msg", msg);

                return RedirectToAction(nameof(Index));
            }
            catch
            {

                //use tempdata to display message on index page
                //use tempdata instead of viewbag bcoz of redirect 
                msg = "Update Unsuccessfully!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Employees/DeleteEmployee/5
        public ActionResult DeleteEmployee(int id)
        {
           
            try
            {
                Employee emp = empUtils.GetEmployee(id);
                //check employee found or not
                if (emp == null)
                {
                    return NotFound();
                }

                return View(emp);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
       
        public ActionResult DeleteEmployee(int id, Employee emp)
        {
            string msg;
            try
            {
           empUtils.DeleteEmployee(id);

                //use tempdata to display message on index page
                //use tempdata instead of viewbag bcoz of redirect 
                msg = "Deleted Successfully!!";
                TempData.Add("msg", msg);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //use tempdata to display message on index page
                //use tempdata instead of viewbag bcoz of redirect 
                msg = "Delete Unsuccessfully!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
