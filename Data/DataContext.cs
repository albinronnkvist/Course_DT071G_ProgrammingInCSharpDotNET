using ForumAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Data
{
    // Inherit the DbContext class that represents a sessions with the database and 
    // can be used to communicate with the database in various ways.
    public class DataContext : DbContext
    {
        // Pass an instance of the DbContextOptions class in the contructor and 
        // specify that it will be used by the DataContext class that we created.
        // And that it should be passed to the base class DbContext. 
        // The DbContextOptions class handles the information about the configuration like a database connection string and a database manager.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        
        // Create DbSet classes with the migration models. 
        // DbSets can be used to perform the database CRUD-operations.
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}