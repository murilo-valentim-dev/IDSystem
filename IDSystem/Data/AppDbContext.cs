using Microsoft.EntityFrameworkCore;
using IDSystem.Models;

namespace IDSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=pessoas.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Se precisar de configurações específicas depois
        }
    }
}
