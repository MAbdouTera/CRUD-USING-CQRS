using Application.Common.Interfaces;
using Domain.Todos;
using Infrasturcture.Configurations;
using Microsoft.EntityFrameworkCore;
namespace Infrasturcture.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options),
IAppDbContext
{
    public DbSet<Todo> Todos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoConfigurations).Assembly);
    }
}