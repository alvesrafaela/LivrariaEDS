namespace Biblioteca
{
    using global::Biblioteca.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext()
            : base("name=BibliotecaContext")
        {
        }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .HasMany(t => t.Assuntos)
                .WithMany(t => t.Livros)
                .Map(m =>
                {
                    m.ToTable("LivroAssunto");
                    m.MapLeftKey("Cod");
                    m.MapRightKey("CodAs");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}