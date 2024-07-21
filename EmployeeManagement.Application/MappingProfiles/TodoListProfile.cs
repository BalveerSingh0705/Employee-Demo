using AutoMapper;
using EmployeeManagement.Applictaion.Models.TodoList;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Applictaion.MappingProfiles;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<CreateTodoListModel, TodoList>();

        CreateMap<TodoList, TodoListResponseModel>();
    }
}
