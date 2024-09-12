namespace EmployeeManagement.Core.Common
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? UserId { get; set; }  // Optionally, include the UserId
    }




}
