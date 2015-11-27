using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscarMedico.Models;
using System.Data;
using System.StubHelpers;

namespace BuscarMedico.Controllers
{
    public class MedicosController : Controller
    {
        private Entitiescademeumedicobd db = new Entitiescademeumedicobd();  // CRIANDO UMA INSTANCIA DE COMUNICAÇÃO DO MODELO COM O BANCO DE DADOS..


        public ActionResult Index() // CHAMA O LINK P/ SER CLICADO P/ ABRIR O FORMULARIO ...
        {
            // OBTENDO UMA LISTA COM TODOS OS MEDICOS CADASTRADOS NO BANCO DE DADOS...
            // LISTANDO TODOS MEDICOS E OBTENDO SUAS RESPECTIVAS ESPECIALIDADES E CIDADES.. 
            var medico = db.medicos.Include("Cidades").Include("Especialidades"); //.ToList(); // "LISTA"
            return View(medico);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CADASTRAR MEDICOS //
        public ActionResult Adicionar()
        { // ADCIONAR NOVO MEDICO.. VIEWBAG = TRANSFERE DADOS DOS CONTROLES P/ AS VIEWS, SELECTLIST REPRESENTA UM COMBOBOX...
            ViewBag.IDCidade = new SelectList(db.cidades, "IDCidade", "Nome");
            ViewBag.IDEspecialidade = new SelectList(db.especialidades, "IDEspecialidade", "Nome");
            // OBS: ESSA ACTION MOSTRA O FORMULARIO
            return View();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }
        [HttpPost] // DIZ AO FRAMEWORK QUAL ACTION ELE DEVE EXECUTAR QUANDO A REQUISISÇÃO POSSUIR O " VERBO POST "
        public ActionResult Adicionar(medicos medico)
        {
            if (ModelState.IsValid) // TESTA SE O FORMULARIO É VALÍDO ..
            {
                db.medicos.Add(medico); // SE VALÍDO, ADICIONA ..
                db.SaveChanges(); // SALVA OS DADOS DO FORMULARIO ..
                return RedirectToAction("Index"); // E RETORNA P/ A ACTION INDEX ..
            }
            ViewBag.IDCidade = new SelectList(db.cidades, "IDCidade", "Nome", medico.IDCidade); // ADICIONANDO A CIDADE DO MEDICO NA LISTA
            ViewBag.IDEspecialidade = new SelectList(db.especialidades, "IDEspecialidades", "Nome", medico.IDEspecialidade); // ADICIONANDO A ESPECIALIDADE DO MEDICO..

            return View(medico);
            // OBS: ESSA ACTION ENVIA O FORMULARIO
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // EDITAR DADOS DOS MEDICOS CADASTRADOS //
        public ActionResult Editar(long id)
        {
            medicos medico = db.medicos.Find(id);
            ViewBag.IDCidade = new SelectList(db.cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(db.especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);

            return View(medico);
            // EDITO OS DADOS ...
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public ActionResult Editar(medicos medico)
        {                                          // VERIFICA ...
            if (ModelState.IsValid)  // SE TIVER OK MANDA AS INFORMAÇÕES EDITADAS PARA O SERVIDOR..
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // MANDA P/ O SERVIDOR ....
            ViewBag.IDCidade = new SelectList(db.cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(db.especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);
            return View(medico);
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // EXCLUIR DADOS DOS MEDICOS CADASTRADOS //
        /*   [HttpGet]
           public ActionResult Excluir(long id) // DELETA MEDICOS CADASTRADOS ...
           {
               medicos medico = db.medicos.Find(id);
               if (medico != null)
               {
                   db.medicos.Remove(medico);
                   db.SaveChanges();
               }
               return RedirectToAction("Index");
           }*/

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Excluir(long id)  // DELETA MEDICOS CADASTRADOS ...
        {
            try
            {
                medicos medico = db.medicos.Find(id);
                db.medicos.Remove(medico);
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
