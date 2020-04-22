using Biblioteca.Models;
using PagedList;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class RelatoriosController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();
        
        public ActionResult ListarLivros(int? pagina)
        {
            var listaLivros = db.Livros
                                .Include("Assuntos")
                                .Include("Autores")
                                .ToList();

           int paginaNumero = 1;

            var pdf = new ViewAsPdf
            {
                ViewName = "ListarLivros",
                PageSize = Size.A4,
                IsGrayScale = true,
                Model = listaLivros.ToPagedList(paginaNumero, listaLivros.Count)
            };
                return pdf;
        }
    }
}