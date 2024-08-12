namespace EmployeeManagement.Core.Common
{
    public class ApiResponseEntity<T>
    {
        // Indicates if the operation was successful
        public bool IsSuccess { get; set; }

        // Status code of the response
        public int StatusCode { get; set; }

        // Message providing additional information about the response
        public string Message { get; set; }

        // The actual data of the response
        public T Data { get; set; }
    }

}

