using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Assunto
    {
        [Key]
        public int CodAs { get; set; }

        [Required]
        [MaxLength(20)]
        public string Descricao { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }

        public bool Selecionado { get; set; }
    }
}