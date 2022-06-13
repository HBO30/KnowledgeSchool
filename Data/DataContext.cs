using Microsoft.EntityFrameworkCore;
namespace KnowledgeSchool.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
        public DbSet<Movie> Movies {get;set;}
        public DbSet<Song> Songs {get;set;}
        public DbSet<User> Users {get;set;}
        
    }
}