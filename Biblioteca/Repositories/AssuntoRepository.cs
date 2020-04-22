using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Biblioteca.Models;

namespace Biblioteca.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private BibliotecaContext _context;
        public AssuntoRepository()
        {
            _context = new BibliotecaContext();
        }

        public void DeleteAssunto(int assuntoId)
        {
            Assunto livro = _context.Assuntos.Find(assuntoId);
            _context.Assuntos.Remove(livro);
        }

        public IEnumerable<Assunto> GetAssunto()
        {
            return _context.Assuntos.ToList();
        }

        public Assunto GetAssuntoPorID(int Id)
        {
            Assunto assunto = _context.Assuntos.Find(Id);
            return assunto;
        }

        public void InsertAssunto(Assunto assunto)
        {
            _context.Assuntos.Add(assunto);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public void UpdateAssunto(Assunto assunto)
        {
            _context.Entry(assunto).State = EntityState.Modified;
        }
    }
}