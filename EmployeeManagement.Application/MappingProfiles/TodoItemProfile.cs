using AutoMapper;
using EmployeeManagement.Applictaion.Models.TodoItem;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Applictaion.MappingProfiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItemModel, TodoItem>()
            .ForMember(ti => ti.IsDone, ti => ti.MapFrom(cti => false));

        CreateMap<UpdateTodoItemModel, TodoItem>();

        CreateMap<TodoItem, TodoItemResponseModel>();
    }
}
