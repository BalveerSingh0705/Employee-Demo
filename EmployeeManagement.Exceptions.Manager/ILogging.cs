using System;
namespace EmployeeManagement.Exceptions.Manager
{
    public interface ILogging
    {
        void Progress(string message);
        void Info(string message);
        void Debug(string message);
        void Warning(string message);
        void Error(string message);
        void Message(string message);
        void Exception(string message, Exception exception, string tenantName, string userInfo, object data);
       /* void Exception(string message, Exception exception);*/
        void Info(Guid id, string screenName, string action, string data);





    }
}
