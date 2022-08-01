using EmployeeSystem.Models.APIModels;
using EmployeeSystem.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeSystem.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountHelper accountHelper;

        public AccountController(IAccountHelper accountHelper)
        {
            this.accountHelper = accountHelper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            dynamic response = new ExpandoObject();
            if (ModelState.IsValid)
            {
                try
                {
                    var loginUser = accountHelper.GetUser(model.UserName);
                    if (loginUser == null)
                    {
                        response.Message = "User not found.";
                        return NotFound(response);
                    }
                    var result = await accountHelper.Login(loginUser.Email, model.Password, true, true);
                    if (result.Succeeded)
                    {
                        string Token = accountHelper.GenerateToken(loginUser);
                        if (string.IsNullOrEmpty(Token))
                        {
                            response.Message = "Error while generating token.";
                            return BadRequest(response);
                        }

                        response.Name = loginUser.UserName;
                        response.Message = "Login successfully";
                        response.Token = Token;
                        response.Expiry = "10,080 mins";
                        return Ok(response);
                    }
                    else
                    {
                        response.Message = "Invalid credentials.";
                        return Unauthorized(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "Login failed. Something went wrong. Error - " + ex.Message;
                    return BadRequest(response);
                }
            }
            else
            {
                response.Message = "Please provide valid credentials.";
                return BadRequest(response);
            }
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            dynamic response = new ExpandoObject();
            if (ModelState.IsValid)
            {
                try
                {
                    var loginUser = accountHelper.GetUser(registerModel.UserName);
                    if (loginUser != null)
                    {
                        response.Message = "User exist.";
                        return Conflict(response);
                    }
                    var result = await accountHelper.CreateUser(registerModel);
                    if (result)
                    {
                        response.Message = "User created successfully.";
                        return StatusCode(Convert.ToInt32(HttpStatusCode.Created));
                    }
                    else
                    {
                        response.Message = "Registration failed";
                        return BadRequest(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "Register failed. Something went wrong. Error - " + ex.Message;
                    return BadRequest(response);
                }
            }
            else
            {
                response.Message = "Please provide valid details.";
                return BadRequest(response);
            }
        }
    }
}
