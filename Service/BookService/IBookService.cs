using FutureOFTask.Domain.Entities;
using FutureOFTask.Domain.ISpecifications.SpecificationTypes;
using FutureOFTask.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FutureOFTask.Service.BookService
{
    public interface IBookService
    {
        Task<int> GetTotalNumberOfReviewsPerBookAsync(string bookId);
        Task<double> GetAverageRatingPerBookAsync(Book book);
        Task<int> GetNumberOfBooksPerGenreAsync(string genreName);
        Task<IReadOnlyList<BookDto>> GetBooksAsync(BookSpecParams specParams);
        Task<IReadOnlyList<BookDto>> GetPopularBooksAsync(int topN);
    }
}
