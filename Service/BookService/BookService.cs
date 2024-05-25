using AutoMapper;
using FutureOFTask.Domain.Entities;
using FutureOFTask.Domain.ISpecifications.SpecificationTypes;
using FutureOFTask.Domain.IUnitOfWork;
using FutureOFTask.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace FutureOFTask.Service.BookService
{
    public class BookService : IBookService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IReadOnlyList<BookDto>> GetBooksAsync(BookSpecParams specParams)
        {
            var spec = new BookSpecifications(specParams);
            var books = await _unitOfWork.Repository<Book>().GetAllWithSpecificationAsync(spec);

            if (!books.Any())
                return null;

            return await mapBooKToWithCalculateAVGBookDtoAsync(books);
        }


        public async Task<double> GetAverageRatingPerBookAsync(Book book)
            => book.Ratings.Any() ? book.Ratings.Average(r => r.Score) : 0;
        

        public async Task<int> GetNumberOfBooksPerGenreAsync(string genreName)
        {
            var spec = new BooksByGenreSpecification(genreName);
            var books = await _unitOfWork.Repository<Book>().GetAllWithSpecificationAsync(spec);

            return books?.Count() ?? 0;
        }

        public async Task<int> GetTotalNumberOfReviewsPerBookAsync(string bookId)
        {
            var spec = new BookSpecifications(bookId);
            var books = await _unitOfWork.Repository<Book>().GetAllWithSpecificationAsync(spec);
            return books.Sum(b => b.Ratings.Count());
        }


        public async Task<IReadOnlyList<BookDto>> GetPopularBooksAsync(int topN)
        {
            var spec = new PopularBooksSpecification(topN);
            var books = await _unitOfWork.Repository<Book>().GetAllWithSpecificationAsync(spec);
            return await mapBooKToWithCalculateAVGBookDtoAsync(books);
        }

        // to Don't Repeat Your Self
        private async Task<IReadOnlyList<BookDto>> mapBooKToWithCalculateAVGBookDtoAsync(IReadOnlyList<Book> books)
        {
            var bookDtos = new List<BookDto>();
            foreach (var book in books)
            {
                var averageRating = await GetAverageRatingPerBookAsync(book);
                var bookDto = _mapper.Map<BookDto>(book);
                bookDto.rate = averageRating;
                bookDtos.Add(bookDto);
            }
            return bookDtos;
        }
    
    }
}
