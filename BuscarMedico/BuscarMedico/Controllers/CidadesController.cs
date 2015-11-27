using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscarMedico.Models;
using System.Data;

namespace BuscarMedico.Controllers
{
    public class CidadesController : Controller
    {
        private Entitiescademeumedicobd db = new Entitiescademeumedicobd(); // CRIANDO UMA INSTANCIA DE COMUNICAÇÃO DO MODELO COM O BANCO DE DADOS..

        public ActionResult Index()
        {
            var cidade = db.cidades;

            return View(cidade);
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CADASTRAR CIDADE //
        public ActionResult Adicionar()
        {

            return View();
            // ACTION MOSTRA O FORMULARIO
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POSTA OS DADOS P/ O SERVIDOR //
        [HttpPost]
        public ActionResult Adicionar(cidades cidade)
        {
            if (ModelState.IsValid)
            {
                db.cidades.Add(cidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cidade);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult Editar(long id)
        {
            cidades cidade = db.cidades.Find(id);
            return View(cidade);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public ActionResult Editar(cidades cidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cidade);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Excluir(long id)
        {
            try
            {
                cidades cidade = db.cidades.Find(id);
                db.cidades.Remove(cidade);
                db.SaveChanges();
                return Boolean.TrueString;
            }
            catch
            {
                return Boolean.FalseString;
            }
        }

    }
}
