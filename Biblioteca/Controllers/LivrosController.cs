using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;
using Biblioteca.Repositories;

namespace Biblioteca.Controllers
{
    public class LivrosController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();
        //private ILivroRepository _livroRepository;
        //private IAutorRepository _autorRepository;
        //public LivrosController()
        //{
        //    _livroRepository = new LivroRepository(new BibliotecaContext());
        //    _autorRepository = new AutorRepository();
        //}

        // GET: Livros
        public ActionResult Index()
        {
            return View(db.Livros.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id); 
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        public ActionResult Create()
        {
            ViewBag.Autores = db.Autores.ToList();
            ViewBag.Assuntos = db.Assuntos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cod,Titulo,Editora,Edicao,AnoPublicacao")] Livro livro)
        {
            var listaAssuntos = Request.Form["Assuntos"];
            var listaAutores = Request.Form["Autores"];

            if (!string.IsNullOrEmpty(listaAssuntos))
            {
                int[] splAssuntos = listaAssuntos.Split(',').Select(Int32.Parse).ToArray();

                if (splAssuntos.Count() > 0)
                {
                    var listaAssuntosSelecionados = db.Assuntos.Where(x => splAssuntos.Contains(x.CodAs)).ToList();

                    livro.Assuntos.AddRange(listaAssuntosSelecionados);
                }
            }
            if (!string.IsNullOrEmpty(listaAutores))
            {
                int[] splAutores = listaAutores.Split(',').Select(Int32.Parse).ToArray();

                if (splAutores.Count() > 0)
                {
                    var lstAutoresSelecionados = db.Autores.Where(x => splAutores.Contains(x.CodAu)).ToList();

                    livro.Autores.AddRange(lstAutoresSelecionados);
                }
            }
            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(livro);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Livro livro = db.Livros
                .Include(i => i.Assuntos)
                .Include(i=>i.Autores)
                .Where(i => i.Cod == id)
                .Single();
            CarregarDadosAssunto(livro);
            if (livro == null)
            {
                return HttpNotFound();
            }

            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cod,Titulo,Editora,Edicao,AnoPublicacao")] Livro livro)
        {
            var listaAssuntos = Request.Form["assuntosSelecionados"];
            var listaAutores = Request.Form["autoresSelecionados"];
            int[] splAssuntos = listaAssuntos.Split(',').Select(Int32.Parse).ToArray();
            int[] splAutores = listaAutores.Split(',').Select(Int32.Parse).ToArray();

            Livro livroAlterar = db.Livros
                .Include(i => i.Assuntos)
                .Include(i=>i.Autores)
                .Where(i => i.Cod == livro.Cod)
                .Single();

            AlterarLivroAssunto(splAssuntos, livroAlterar);
            AlterarLivroAutor(splAutores, livroAlterar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);// _livroRepository.GetLivroByID(id.Value);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livros.Find(id);
            db.Livros.Remove(livro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void CarregarDadosAssunto(Livro livro)
        {
            var listaAssunto = db.Assuntos;
            var listaAutor = db.Autores;
            var livroAssunto = new HashSet<int>(livro.Assuntos.Select(c => c.CodAs));
            var livroAutor = new HashSet<int>(livro.Autores.Select(c => c.CodAu));
            var viewModel = new List<Assunto>();
            var viewModelAutor = new List<Autor>();

            foreach (var assunto in listaAssunto)
            {
                viewModel.Add(new Assunto
                {
                    CodAs = assunto.CodAs,
                    Descricao = assunto.Descricao,
                    Selecionado = livroAssunto.Contains(assunto.CodAs)
                });
            }
            ViewBag.Assuntos = viewModel;

            foreach (var autor in listaAutor)
            {
                viewModelAutor.Add(new Autor
                {
                    CodAu = autor.CodAu,
                    Nome = autor.Nome,
                    Selecionado = livroAutor.Contains(autor.CodAu)
                });
            }
            ViewBag.Autores = viewModelAutor;
        }

        private void AlterarLivroAssunto(int[] assuntosSelecionados, Livro livroAlterar)
        {
            if (assuntosSelecionados == null)
            {
                livroAlterar.Assuntos = new List<Assunto>();
                return;
            }

            var livroAssunto = new HashSet<int>
                (livroAlterar.Assuntos.Select(c => c.CodAs));
            foreach (var assunto in db.Assuntos)
            {
                if (assuntosSelecionados.Contains(assunto.CodAs))
                {
                    if (!livroAssunto.Contains(assunto.CodAs))
                    {
                        livroAlterar.Assuntos.Add(assunto);
                    }
                }
                else
                {
                    if (livroAssunto.Contains(assunto.CodAs))
                    {
                        livroAlterar.Assuntos.Remove(assunto);
                    }
                }
            }
        }

        private void AlterarLivroAutor(int[] autoresSelecionados, Livro livroAlterar)
        {
            if (autoresSelecionados == null)
            {
                livroAlterar.Autores = new List<Autor>();
                return;
            }

            var livroAutor = new HashSet<int>
                (livroAlterar.Autores.Select(c => c.CodAu));
            foreach (var autor in db.Autores)
            {
                if (autoresSelecionados.Contains(autor.CodAu))
                {
                    if (!livroAutor.Contains(autor.CodAu))
                    {
                        livroAlterar.Autores.Add(autor);
                    }
                }
                else
                {
                    if (livroAutor.Contains(autor.CodAu))
                    {
                        livroAlterar.Autores.Remove(autor);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
