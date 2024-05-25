using FutureOFTask.Domain.Entities;
using FutureOFTask.Repository.Data;
using FutureOFTask.Repository.GenericRepo;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookManagement.Tests
{
    public class GenericRepositoryTest :IDisposable
    {
        private readonly BookDbContext _dbContext;

        public GenericRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new BookDbContext(options);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
         }

        [Fact]
    public void AddBook_WhenAddNewBook_BookIsAddedSuccessfully()
    {
        // Arrange
        var repository = new GenericRepository<Book>(_dbContext);

            var isbn = "23232";
            var datetime = DateTime.Now;
        // Create a new book
        var book = new Book { Id = Guid.NewGuid(), Title = "Test Book",Author="br",ISBN=isbn,PublicationDate = datetime };

        // Act
        repository.AddAsync(book);
        _dbContext.SaveChanges();

        // Assert
        var retrievedBook = _dbContext.Books.FirstOrDefault();
        Assert.NotNull(retrievedBook);
        Assert.Equal("Test Book", retrievedBook.Title);
    }



        [Fact]
        public void UPdateBook_WhenUpdateBook_BookIsUpdatedSuccessfully()
        {
            // Arrange
            var repository = new GenericRepository<Book>(_dbContext);

            var isbn = "23232";
            var datetime = DateTime.Now;
            var id = Guid.NewGuid();

            // Add a book
            var initialBook = new Book { Id = id, Title = "Initial Book", Author = "Author", ISBN = isbn, PublicationDate = datetime };
            _dbContext.Books.Add(initialBook);
            _dbContext.SaveChanges(); 

            _dbContext.Entry(initialBook).State = EntityState.Detached;

            // Update a book 
            var updatedBook = new Book { Id = id, Title = "updated Book", Author = "brr", ISBN = isbn, PublicationDate = datetime };

            // Act
            repository.UpdateBook(updatedBook);

            // Assert
            var getBook = _dbContext.Books.FirstOrDefault(b => b.Id == id);
            Assert.NotNull(getBook);
            Assert.Equal("updated Book", getBook.Title);
        }



        [Fact]
        public void DeleteBook_WhenBookExists_BookIsDeletedSuccessfully()
        {
            // Arrange
            var repository = new GenericRepository<Book>(_dbContext);

            var isbn = "23232";
            var datetime = DateTime.Now;
            var id = Guid.NewGuid();

            // Add a book
            var initialBook = new Book { Id = id, Title = "Initial Book", Author = "Author", ISBN = isbn, PublicationDate = datetime };
            _dbContext.Books.Add(initialBook);
            _dbContext.SaveChanges();  

            // Act
            var bookToDelete = _dbContext.Books.First(b => b.Id == id);

            repository.DeleteBook(bookToDelete);
            _dbContext.SaveChanges();

            // Assert
            var getBook = _dbContext.Books.FirstOrDefault(b => b.Id == id);
            Assert.Null(getBook); 
        }

    }
}