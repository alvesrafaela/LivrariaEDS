using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Autor
    {
        [Key]
        public int CodAu { get; set; }
        [Required]
        [MaxLength(40)]
        public string Nome { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }

        public bool Selecionado { get; set; }
    }
}