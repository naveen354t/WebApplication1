using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {

            //CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}
