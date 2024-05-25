using FutureOFTask.Domain.Entities;
using FutureOFTask.Repository.Data;
using FutureOFTask.Repository.GenericRepo;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FutureOfTask.Tests
{
    public class GenericRepositoryTests
    {


        [Fact]
        public async Task AddAsync_ShouldAddBook()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var genreId = Guid.NewGuid();
            var date = new DateTime(2024, 5, 24, 11, 33, 57, DateTimeKind.Utc); 
            var entity = new Book
            {
                Title = "Ghandy",
                Author = "Handos",
                ISBN = "9387d4398",
                PublicationDate = date,
                GenreId = genreId
            };

            await using var context = new BookDbContext(options);
            var repository = new GenericRepository<Book>(context);

            // Act
            await repository.AddAsync(entity);

            // Assert
            var addedEntity = await context.Books.FirstOrDefaultAsync();
            Assert.NotNull(addedEntity);
            Assert.Equal("Ghandy", addedEntity.Title);
            Assert.Equal("9387d4398", addedEntity.ISBN);
            Assert.Equal("Handos", addedEntity.Author);
            Assert.Equal(date, addedEntity.PublicationDate);
            Assert.Equal(genreId, addedEntity.GenreId);
        }
        //[Fact]
        //public async Task AddAsync_ShouldAddEntity()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<BookDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;

        //    await using var context = new BookDbContext(options);
        //    var repository = new GenericRepository<Book>(context);
        //    var entity = new Book { Title = "Test", ISBN="wewewe", Author="sdasd", GenreId=Guid.NewGuid()};

        //    // Act
        //    await repository.AddAsync(entity);

        //    // Assert
        //    var addedEntity = await context.Books.FirstOrDefaultAsync();
        //    Assert.NotNull(addedEntity.Id);
        //}
    }
}
