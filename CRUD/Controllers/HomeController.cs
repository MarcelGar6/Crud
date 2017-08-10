using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAlunos()
        {
            using (CrudDatabaseEntities dc = new CrudDatabaseEntities())
            {
                var alunos = dc.Alunos.OrderBy(a => a.Nome).ToList();
                return Json(new { data = alunos }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (CrudDatabaseEntities dc = new CrudDatabaseEntities())
            {
                var v = dc.Alunos.Where(a => a.IdAluno == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        public ActionResult Save(Aluno alu)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (CrudDatabaseEntities dc = new CrudDatabaseEntities())
                {
                    if(alu.IdAluno > 0)
                    {
                        //Edit
                        var v = dc.Alunos.Where(a => a.IdAluno == alu.IdAluno).FirstOrDefault();
                        if(v != null)
                        {
                            v.Nome = alu.Nome;
                            v.Sobrenome = alu.Sobrenome;
                            v.Email = alu.Email;
                            v.Cidade = alu.Cidade;
                            v.Pais = alu.Pais;
                        }
                    }
                    else
                    {
                        //Save
                        dc.Alunos.Add(alu);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (CrudDatabaseEntities dc = new CrudDatabaseEntities())
            {
                var v = dc.Alunos.Where(a => a.IdAluno == id).FirstOrDefault();
                if(v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteAluno(int id)
        {
            bool status = false;
            using (CrudDatabaseEntities dc = new CrudDatabaseEntities())
            {
                var v = dc.Alunos.Where(a => a.IdAluno == id).FirstOrDefault();
                if(v != null)
                {
                    dc.Alunos.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}