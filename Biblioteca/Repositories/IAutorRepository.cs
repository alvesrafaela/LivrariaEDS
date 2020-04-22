using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Repositories
{
    public interface IAutorRepository
    {
        IEnumerable<Autor> GetAutor();
        Autor GetAutorPorID(int Id);
        void InsertAutor(Autor autor);
        void DeleteAutor(int autorId);
        void UpdateAutor(Autor autor);
        void Salvar();
    }
}
