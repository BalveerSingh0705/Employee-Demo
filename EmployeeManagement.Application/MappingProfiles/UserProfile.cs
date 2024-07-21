using AutoMapper;
using EmployeeManagement.Applictaion.Models.User;
using EmployeeManagement.DataAccess.Identity;

namespace EmployeeManagement.Applictaion.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
    }
}
