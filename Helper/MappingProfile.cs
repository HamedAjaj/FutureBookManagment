using AutoMapper;
using FutureOFTask.Domain.Entities;
using FutureOFTask.Domain.Entities.Identity;
using FutureOFTask.Dtos;
using FutureOFTask.Dtos.Rating;

namespace FutureOFTask.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, Book>().ReverseMap()
                .ForMember(b => b.GenreName, o => o.MapFrom(g => g.Genre.Name));

            
            CreateMap<Book, BookAddDto>().ReverseMap();

            CreateMap<AppUser,RegisterDto>().ReverseMap();

            CreateMap<Rating, RatingCreateDTO>().ReverseMap();
        }
    }
}
