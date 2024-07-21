namespace EmployeeManagement.Core.Common
{
    public class ApiResponseEntity
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }  // Optional, if you need to return additional data
    }
}

