using AutoMapper;
using FutureOFTask.Domain.Entities;
using FutureOFTask.Domain.ISpecifications.SpecificationTypes;
using FutureOFTask.Domain.IUnitOfWork;
using FutureOFTask.Dtos;
using FutureOFTask.Service.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FutureOFTask.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class BooksController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper, IBookService bookService) {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
            _bookService = bookService;
        }


        #region  Endpoints Actions
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BookDto>>> GetBooksAsync([FromQuery] BookSpecParams specParams)
        {
            var books = await _bookService.GetBooksAsync(specParams);
            if (books == null) return NotFound(new { Message = "No books found" });
            return Ok(books);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult> AddBookAsync(BookAddDto book)
        {
            var map = _mapper.Map<BookAddDto, Book>(book);
            map.GenreId = book.GenreId;
           await _unitOfWork.Repository<Book>().AddAsync(map);
           await _unitOfWork.Complete();
            return Ok(new {Message="Book Added Successfully :) "});
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateBook(BookDto bookupdate)
        {
            var specific = new BookSpecifications(bookupdate.Id);
            var book = await _unitOfWork.Repository<Book>().GetByWithSpecificationAsync(specific);
            if(book == null) { return NotFound(); }
            _mapper.Map(bookupdate,book);
            _unitOfWork.Repository<Book>().UpdateBook(book);
            await _unitOfWork.Complete();
            return Ok(book);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteBook(string BookId)
        {
            var spec = new BookSpecifications(BookId);
            var book = await _unitOfWork.Repository<Book>().GetByWithSpecificationAsync(spec);
            if(book == null) { return NotFound(); };
            _unitOfWork.Repository<Book>().DeleteBook(book);
            await _unitOfWork.Complete();
            return Ok(book);
        }

        [Authorize(Roles ="Admin,User")]
        [HttpGet("CountOfBooksPerGenre")]
        public async Task<ActionResult<int>> GetNumberOfBooksPerGenre(string genreName)
         => Ok(await _bookService.GetNumberOfBooksPerGenreAsync(genreName));


        //most popular books based on ratings
        [HttpGet("popular")]
        public async Task<ActionResult<IReadOnlyList<BookDto>>> GetPopularBooksAsync([FromQuery] int topN = 10)
        {
            var books = await _bookService.GetPopularBooksAsync(topN);
            if (books == null || !books.Any())
            {
                return NotFound(new { Message = "No popular books found" });
            }
            return Ok(books);
        }


        [HttpGet("totalReviews")]
        public async Task<ActionResult<int>> GetTotalNumberOfReviews(string bookId)
         => Ok(await _bookService.GetTotalNumberOfReviewsPerBookAsync(bookId));




        #endregion
    }
}
