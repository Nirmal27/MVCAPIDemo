using EmployeeSystem.DataAccess.Abstraction;
using EmployeeSystem.DataAccess.Identity;
using EmployeeSystem.Models.APIModels;
using EmployeeSystem.Models.CommonModels;
using EmployeeSystem.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem.Services.Repository
{
    public class AccountHelper : IAccountHelper
    {
        private readonly AppSettings appSettings;
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration configuration;

        public AccountHelper(IAccountRepository accountRepository, IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            this.accountRepository = accountRepository;
            this.appSettings = appSettings.Value;
            this.configuration = configuration;
        }

        /// <summary>
        /// Method for Checking username already exist or not
        /// </summary>
        /// <param name="UserName">Username of the user</param>
        /// <returns>returns username if exist</returns>
        public async Task<string> CheckExistsUserName(string UserName)
        {
            return await accountRepository.CheckExistsUserName(UserName);
        }

        /// <summary>
        /// Method for creating the user
        /// </summary>
        /// <param name="registerModel">Object value of the register model</param>
        /// <returns>returns true if user is created else false</returns>
        public async Task<bool> CreateUser(RegisterModel registerModel)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = registerModel.UserName,
                Email = registerModel.UserName
            };

            return await accountRepository.CreateUser(applicationUser, registerModel.Password);
        }

        /// <summary>
        /// Method for generating the JWT Token
        /// </summary>
        /// <param name="applicationUser">Object value of hte Application user model</param>
        /// <returns>returns JWT Token</returns>
        public string GenerateToken(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                return null;

            var applicationUserData = accountRepository.GetUser(applicationUser.UserName);
            if (applicationUserData == null)
                return null;

            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            var claimsData = new Claim[]
            {
                new Claim(ClaimTypes.Name, applicationUserData.UserName.ToString()),
                new Claim(ClaimTypes.Email, applicationUserData.Email.ToString()),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            };
            var tokenString = new JwtSecurityToken(
            issuer: configuration["BearerTokens:Issuer"],
            audience: configuration["BearerTokens:Audience"],
            expires: DateTime.Now.AddMinutes(60),
            claims: claimsData,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenString);
            return token;
        }

        /// <summary>
        /// Method for getting the user details
        /// </summary>
        /// <param name="userName">Username of the user</param>
        /// <returns></returns>
        public ApplicationUser GetUser(string userName)
        {
            return accountRepository.GetUser(userName);
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="UserName">Username of the user</param>
        /// <param name="Password">Password of the user</param>
        /// <param name="IsPersistent">Boolean value of Persistent</param>
        /// <param name="lockoutOnFailure">Boolean value for lock on failure</param>
        /// <returns></returns>
        public async Task<SignInResult> Login(string UserName, string Password, bool IsPersistent, bool lockoutOnFailure)
        {
            return await accountRepository.Login(UserName, Password, IsPersistent, lockoutOnFailure);
        }
    }
}
