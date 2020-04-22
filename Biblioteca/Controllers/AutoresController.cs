using Biblioteca.Models;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class AutoresController : Controller
    {
        private IAutorRepository _autorRepository;

        public AutoresController()
        {
            _autorRepository = new AutorRepository();
        }

        // GET: Autores
        public ActionResult Index()
        {
            var autores = from autor in _autorRepository.GetAutor()
                           select autor;

            return View(autores);
        }

        public ActionResult Create()
        {
            return View(new Autor());
        }

        [HttpPost]
        public ActionResult Create(Autor autor)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _autorRepository.InsertAutor(autor);
                    _autorRepository.Salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível salvar as mudanças. Tente novamente.");
            }
            return View(autor);
        }

        public ActionResult Details(int id)
        {
            Autor autor = _autorRepository.GetAutorPorID(id);
            return View(autor);
        }

        public ActionResult Edit(int id)
        {
            Autor autor = _autorRepository.GetAutorPorID(id);
            return View(autor);
        }

        [HttpPost]
        public ActionResult Edit(Autor autor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _autorRepository.UpdateAutor(autor);
                    _autorRepository.Salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível salvar as mudanças. Tente novamente.");
            }
            return View(autor);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Não foi possível salvar as mudanças. Tente novamente.";
            }
            Autor autor = _autorRepository.GetAutorPorID(id);
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                Autor autor = _autorRepository.GetAutorPorID(id);
                _autorRepository.DeleteAutor(id);
                _autorRepository.Salvar();
            }
            catch (Exception)
            {
                return RedirectToAction("Delete",
                  new System.Web.Routing.RouteValueDictionary {
               { "id", id },
               { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}