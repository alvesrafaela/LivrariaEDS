using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Repositories
{
    public interface IAssuntoRepository
    {
        IEnumerable<Assunto> GetAssunto();
        Assunto GetAssuntoPorID(int Id);
        void InsertAssunto(Assunto assunto);
        void DeleteAssunto(int assuntoId);
        void UpdateAssunto(Assunto assunto);
        void Salvar();
    }
}
