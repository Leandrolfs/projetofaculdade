using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscarMedico.Models;
using System.Data;

namespace BuscarMedico.Controllers
{
    public class EspecialidadeController : Controller
    {
        private Entitiescademeumedicobd db = new Entitiescademeumedicobd();

        public ActionResult Index()
        {
            var especialidade = db.especialidades;

            return View(especialidade);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Adicionar()
        {
            return View();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public ActionResult Adicionar(especialidades especialidade)
        {
            if (ModelState.IsValid)
            {
                db.especialidades.Add(especialidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Editar(long id)
        {
            especialidades especialidade = db.especialidades.Find(id);
            return View(especialidade);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [HttpPost]
        public ActionResult Editar(especialidades especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public string Excluir(long id)
        {
            try
            {
                especialidades especialidade = db.especialidades.Find(id);
                db.especialidades.Remove(especialidade);
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
