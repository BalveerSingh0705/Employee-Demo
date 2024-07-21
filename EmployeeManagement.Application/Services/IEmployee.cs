using EmployeeManagement.Applictaion.Models.TodoItem;
using EmployeeManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Applictaion.Services
{
    public interface IEmployee
    {
        Task<CreateTodoItemResponseModel> CreateAsync(EmployeeEntity employeeEntity,
        CancellationToken cancellationToken = default);
    }
}
