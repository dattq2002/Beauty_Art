using AutoMapper;
using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, LoginModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserView>().ReverseMap();

            CreateMap<Course, CourseModel>().ReverseMap();
            CreateMap<Course, CourseResponse>().ReverseMap();
            CreateMap<User, UserwithRole>().ReverseMap();
        }
    }
}
