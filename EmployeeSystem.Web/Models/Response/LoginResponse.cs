namespace EmployeeSystem.Web.Models.Response
{
    public class LoginResponse
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Expiry { get; set; }
    }
}
