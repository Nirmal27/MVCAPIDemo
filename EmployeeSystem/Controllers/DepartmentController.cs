using EmployeeSystem.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace EmployeeSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentHelper departmentHelper;
        public DepartmentController(IDepartmentHelper departmentHelper)
        {
            this.departmentHelper = departmentHelper;
        }

        [HttpGet]
        [Route("departments/{id=0}")]
        public IActionResult GetDepartments(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (id != 0)
                {
                    var department = departmentHelper.GetDepartmentById(id);
                    if (department == null)
                    {
                        response.Message = "Employee not found.";
                        return NotFound(response);
                    }

                    return Ok(department);
                }

                var departments = departmentHelper.GetDepartment();
                if (departments == null || departments.Count == 0)
                {
                    response.Message = "Employees not found.";
                    return NotFound(response);
                }

                return Ok(departments);
            }
            catch (Exception ex)
            {
                response.Message = "Something went wrong. Error - " + ex.Message;
                return BadRequest(response);
            }
        }
    }
}
