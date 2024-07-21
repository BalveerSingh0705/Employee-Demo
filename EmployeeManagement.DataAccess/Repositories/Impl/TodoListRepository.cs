using EmployeeManagement.Core.Entities;
using EmployeeManagement.DataAccess.Persistence;

namespace EmployeeManagement.DataAccess.Repositories.Impl;

public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
{
    public TodoListRepository(DatabaseContext context) : base(context) { }
}
