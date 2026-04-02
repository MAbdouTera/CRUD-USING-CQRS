using Infrasturcture.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.Helpers;

public static class TestDbContextFactory
{
    public static AppDbContext Create(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AppDbContext(options);
    }
}
