using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Biblioteca.Models;

namespace Biblioteca.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private BibliotecaContext _context;
        public AutorRepository()
        {
            _context = new BibliotecaContext();
        }

        public void DeleteAutor(int autorId)
        {
            Autor autor = _context.Autores.Find(autorId);
            _context.Autores.Remove(autor);
        }

        public IEnumerable<Autor> GetAutor()
        {
            return _context.Autores.ToList();
        }

        public Autor GetAutorPorID(int Id)
        {
            return _context.Autores.Find(Id);
        }

        public void InsertAutor(Autor autor)
        {
            _context.Autores.Add(autor);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public void UpdateAutor(Autor autor)
        {
            _context.Entry(autor).State = EntityState.Modified;
        }
    }
}