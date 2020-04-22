using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Livro
    {
        public Livro()
        {
            this.Assuntos = new HashSet<Assunto>().ToList();
            this.Autores = new HashSet<Autor>().ToList();
        }

        [Key]
        public int Cod { get; set; }

        [Required]
        [MaxLength(40)]
        public string Titulo { get; set; }
        public string Editora { get; set; }

        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }

        public List<Assunto> Assuntos { get; set; }
        public List<Autor> Autores { get; set; }
    }
}