using Biblioteca.Models;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class AssuntosController : Controller
    {
        private IAssuntoRepository _assuntoRepository;

        public AssuntosController()
        {
            _assuntoRepository = new AssuntoRepository();
        }

        // GET: Assuntos
        public ActionResult Index()
        {
            var assuntos = from assunto in _assuntoRepository.GetAssunto()
                         select assunto;

            return View(assuntos);
        }

        public ActionResult Edit(int id)
        {
            Assunto assunto = _assuntoRepository.GetAssuntoPorID(id);
            return View(assunto);
        }

        public ActionResult Create()
        {
            return View(new Assunto());
        }

        [HttpPost]
        public ActionResult Create(Assunto assunto)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _assuntoRepository.InsertAssunto(assunto);
                    _assuntoRepository.Salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível salvar as mudanças. Tente novamente.");
            }
            return View(assunto);
        }

        public ActionResult Details(int id)
        {
            Assunto assunto = _assuntoRepository.GetAssuntoPorID(id);
            return View(assunto);
        }

        [HttpPost]
        public ActionResult Edit(Assunto assunto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _assuntoRepository.UpdateAssunto(assunto);
                    _assuntoRepository.Salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível salvar as mudanças.");
            }
            return View(assunto);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Não foi possível salvar as mudanças. Tente novamente.";
            }
            Assunto assunto = _assuntoRepository.GetAssuntoPorID(id);
            return View(assunto);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                _assuntoRepository.DeleteAssunto(id);
                _assuntoRepository.Salvar();
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