using EmployeeManagement.Core.Entities;
using EmployeeManagement.DataAccess.Persistence;

namespace EmployeeManagement.DataAccess.Repositories.Impl;

public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(DatabaseContext context) : base(context) { }
}
