using EmployeeManagement.Applictaion.Models;
using EmployeeManagement.Applictaion.Models.TodoList;

namespace EmployeeManagement.Applictaion.Services;

public interface ITodoListService
{
    Task<CreateTodoListResponseModel> CreateAsync(CreateTodoListModel createTodoListModel);

    Task<BaseResponseModel> DeleteAsync(Guid id);

    Task<IEnumerable<TodoListResponseModel>> GetAllAsync();

    Task<UpdateTodoListResponseModel> UpdateAsync(Guid id, UpdateTodoListModel updateTodoListModel);
}
