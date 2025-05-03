using Microsoft.EntityFrameworkCore;
using WhisperBranch.Domain.Entities;


namespace WhisperBranch.Persistence.Context
{
    public class WBContext : DbContext
    {
        public DbSet<GraphEnt> Graphs { get; set; } 
        public WBContext(DbContextOptions<WBContext> options) : base(options) { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    
}
