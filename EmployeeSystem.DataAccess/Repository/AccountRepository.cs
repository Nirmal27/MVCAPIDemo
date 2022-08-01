using EmployeeSystem.DataAccess.Abstraction;
using EmployeeSystem.DataAccess.DbContext;
using EmployeeSystem.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystem.DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly EmployeeDbContext context;

        public AccountRepository(RoleManager<Role> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, EmployeeDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        /// <summary>
        /// Method for Checking username already exist or not
        /// </summary>
        /// <param name="UserName">Username of the user</param>
        /// <returns>returns username if exist</returns>
        public async Task<string> CheckExistsUserName(string UserName)
        {
            var userName = await userManager.FindByNameAsync(UserName);
            if (userName != null)
            {
                // return CommonMessage.UserNameAlreadyExits;
            }
            return "";
        }

        /// <summary>
        /// Method for creating user
        /// </summary>
        /// <param name="model">Object user model</param>
        /// <param name="password">Password of the user</param>
        /// <returns>returns true if user is created.</returns>
        public async Task<bool> CreateUser(ApplicationUser model, string password)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method for getting user details
        /// </summary>
        /// <param name="userName">Username of the user</param>
        /// <returns>returns user details</returns>
        public ApplicationUser GetUser(string userName)
        {
            var applicationUser = context.Users.FirstOrDefault(x => x.UserName == userName);
            if (applicationUser == null)
                return null;

            return applicationUser;
        }

        /// <summary>
        /// Method for loging
        /// </summary>
        /// <param name="UserName">Username of the user</param>
        /// <param name="Password">Password of the user</param>
        /// <param name="IsPersistent">Bool value if Persistent</param>
        /// <param name="lockoutOnFailure">Bool value for the Lockout on failure</param>
        /// <returns>Returns identity signinresult</returns>
        public async Task<SignInResult> Login(string UserName, string Password, bool IsPersistent, bool lockoutOnFailure)
        {
            var Result = await signInManager.PasswordSignInAsync(UserName, Password, true, lockoutOnFailure: lockoutOnFailure);
            return Result;
        }
    }
}
