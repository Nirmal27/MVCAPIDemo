using EmployeeSystem.DataAccess.Entities;
using EmployeeSystem.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeHelper employeeHelper;
        public EmployeeController(IEmployeeHelper employeeHelper)
        {
            this.employeeHelper = employeeHelper;
        }

        [HttpGet]
        [Route("employees/{id=0}")]
        public IActionResult GetEmployees(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (id != 0)
                {
                    var employee = employeeHelper.GetEmployeeById(id);
                    if (employee == null)
                    {
                        response.Message = "Employee not found.";
                        return NotFound(response);
                    }

                    return Ok(employee);
                }

                var employees = employeeHelper.GetEmployees();
                if (employees == null || employees.Count == 0)
                {
                    response.Message = "Employees not found.";
                    return NotFound(response);
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                response.Message = "Something went wrong. Error - " + ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("employeesdepartments/{id=0}")]
        public IActionResult GetEmployeesDepartment(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (id != 0)
                {
                    var employee = employeeHelper.GetEmployeesDepartments(id);
                    if (employee == null)
                    {
                        response.Message = "Employee not found.";
                        return NotFound(response);
                    }

                    return Ok(employee);
                }

                var employees = employeeHelper.GetEmployeesDepartments();
                if (employees == null || employees.Count == 0)
                {
                    response.Message = "Employees not found.";
                    return NotFound(response);
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                response.Message = "Something went wrong. Error - " + ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("employees/{id=0}")]
        public IActionResult DeleteEmployee(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (id == 0)
                {
                    response.Message = "Please provide valid employee id.";
                    return BadRequest(response);
                }

                if (employeeHelper.DeleteEmployee(id))
                {
                    response = "Employee deleted.";
                    return Ok(response);
                }
                response.Message = "Employees not found.";
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Message = "Something went wrong. Error - " + ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("employees")]
        public IActionResult UpdateEmployee(EmployeesDepartments employee)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!employeeHelper.CheckDepartmentExist(Convert.ToInt32(employee.DepartmentId)))
                    {
                        response = "Role does not exist.";
                        return BadRequest(response);
                    }

                    var result = employeeHelper.UpdateEmployee(employee);
                    if (result)
                    {
                        response = "Employee updated.";
                        return Ok(response);
                    }

                    response.Message = "Employee not found.";
                    return NotFound(response);
                }
                else
                {
                    response.Message = "Please provide valid employee details";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Something went wrong. Error - " + ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("employees")]
        public IActionResult InsertEmployee(Employee employee)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!employeeHelper.CheckDepartmentExist(Convert.ToInt32(employee.DepartmentId)))
                    {
                        response = "Role does not exist.";
                        return BadRequest(response);
                    }

                    var result = employeeHelper.AddEmployee(employee);
                    if (result)
                    {
                        response = "Employee inserted.";
                        return StatusCode(Convert.ToInt32(HttpStatusCode.Created));
                    }

                    response.Message = "Employee exists.";
                    return Conflict(response);
                }
                else
                {
                    response.Message = "Please provide valid employee details";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Something went wrong. Error - " + ex.Message;
                return BadRequest(response);
            }
        }
    }
}
