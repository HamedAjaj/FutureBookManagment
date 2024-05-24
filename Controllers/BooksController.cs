using AutoMapper;
using FutureOFTask.Domain.Entities;
using FutureOFTask.Domain.ISpecifications.SpecificationTypes;
using FutureOFTask.Domain.IUnitOfWork;
using FutureOFTask.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FutureOFTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }


        #region  Endpoints Actions

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BookDto>>> GetBooksAsync([FromQuery] BookSpecParams specParams) {
            var spec = new BookSpecifications(specParams);
            var books = await _unitOfWork.Repository<Book>().GetByAllWithSpecificationAsync(spec);
            var map = _mapper.Map<IEnumerable<Book>, IReadOnlyList<BookDto>>(books);
            return map == null || !map.Any() ? Ok(new { Message = "Data is Empty" }) : Ok(map);

        }


        [HttpPost("add")]
        public async Task<ActionResult> AddBookAsync(BookAddDto book)
        {
            var map = _mapper.Map<BookAddDto, Book>(book);
           await _unitOfWork.Repository<Book>().AddAsync(map);
           await _unitOfWork.Complete();
            return Ok(new {Message="Book Added Successfully :) "});
        }

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


            //var result =_unitOfWork.Repository<Book>().UpdateBook()
        }

         #endregion
    }
}
