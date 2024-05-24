using AutoMapper;
using FutureOFTask.Domain.Entities;
using FutureOFTask.Domain.Entities.Identity;
using FutureOFTask.Dtos;

namespace FutureOFTask.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, BookAddDto>().ReverseMap();

            CreateMap<AppUser,RegisterDto>().ReverseMap();
        }
    }
}
