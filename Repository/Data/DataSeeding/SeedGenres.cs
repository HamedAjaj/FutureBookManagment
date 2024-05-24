using FutureOFTask.Domain.Entities;
using System.Text.Json;

namespace FutureOFTask.Repository.Data.DataSeeding
{
    public static class SeedGenres
    {
        public static async Task SeedAsync(BookDbContext context)
        {
            if (!context.Genres.Any())
            {
                var GenreData = File.ReadAllText("../FutureOFTask/Repository/Data/DataSeeding/Genres.json");
                var brands = JsonSerializer.Deserialize<List<Genre>>(GenreData);

                if (brands?.Count > 0)
                {
                    var Genres2 = brands.Select(b => new Genre() { Name = b.Name });
                    foreach (var genre in Genres2)
                        await context.Set<Genre>().AddAsync(genre);
                    await context.SaveChangesAsync();
                }
            }
        }
    }    
}
