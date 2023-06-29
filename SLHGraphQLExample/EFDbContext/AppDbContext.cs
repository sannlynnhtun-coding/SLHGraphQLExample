using GraphQL20221004.Features.Fruit;
using GraphQL20221004.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GraphQL20221004.EfDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<FruitModel> Fruits { get; set; }
    }
}
