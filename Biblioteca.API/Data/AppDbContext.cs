using Biblioteca.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Data
    {
    public class AppDbContext : DbContext
    {

        public DbSet<BibliotecarioModel>? Bibliotecario {get; set;}
        public DbSet<EmprestimoModel>? Emprestimo {get; set;}
        public DbSet<LivroModel>? Livro {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=bibliotecadb;Integrated Security=True");
            
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<BibliotecarioModel>().ToTable("Bibliotecario");
            modelBuilder.Entity<EmprestimoModel>().ToTable("Emprestimo");
            modelBuilder.Entity<LivroModel>().ToTable("Livro");
        }
    }
}