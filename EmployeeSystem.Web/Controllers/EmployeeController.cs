using EmployeeSystem.Models.Models;
using EmployeeSystem.Web.Abstraction;
using EmployeeSystem.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeSystem.Web.Controllers
{
    [Authorize]
    [SessionFilter]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeHelper employeeHelper;
        private readonly IDepartmentHelper departmentHelper;
        public EmployeeController(IEmployeeHelper employeeHelper, IDepartmentHelper departmentHelper)
        {
            this.employeeHelper = employeeHelper;
            this.departmentHelper = departmentHelper;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            //var employees = employeeHelper.GetEmployees();
            var employees = employeeHelper.GetEmployeesDepartments();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var employee = employeeHelper.GetEmployeeDepartment(id);
            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            ViewBag.Departments = departmentHelper.GetDepartments();
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel employee)
        {
            try
            {
                var result = employeeHelper.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeesDepartmentsViewModel employee = employeeHelper.GetEmployeeDepartment(id);
            ViewBag.Departments = departmentHelper.GetDepartments();
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeesDepartmentsViewModel employee)
        {
            try
            {
                var result = employeeHelper.EditEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            EmployeesDepartmentsViewModel employee = employeeHelper.GetEmployeeDepartment(id);
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EmployeesDepartmentsViewModel employee)
        {
            try
            {
                var result = employeeHelper.DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
