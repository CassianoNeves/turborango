using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurboRango.Dominio;
using TurboRango.Web.Models;

namespace TurboRango.Web.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservas
        public ActionResult Index()
        {
            List<ReservaRestaurante> reservasRestaurantes = new List<ReservaRestaurante>();
            var reservas = db.Reservas.ToList();

            foreach( var reserva in reservas )
            {
                var restaurante = db.Restaurantes.Find(reserva.idRestaurante);

                reservasRestaurantes.Add(new ReservaRestaurante
                    {
                        Reserva = reserva,
                        Restaurante = restaurante
                    }
                );
            }
            return View(reservasRestaurantes);
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create/5
        public ActionResult Create(int? id)
        {
            ReservaRestaurante ReservaRestaurante =
                new ReservaRestaurante
                {
                    Restaurante = db.Restaurantes.Find(id)
                };

            return View(ReservaRestaurante);
        }


        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRestaurante,NomeCliente,QtdPessoas,ValorTotal,Data,Turno")] Reserva Reserva)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(Reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Reserva);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QtdPessoas,ValorTotal,Data,Turno,Restaurante")] ReservaRestaurante reservaRestaurante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservaRestaurante.Reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservaRestaurante.Reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Reservas/Restaurantes
        public JsonResult Restaurantes()
        {
            var todos = db.Restaurantes.ToList();

            return Json(new
            {
                restaurantes = todos         
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Reservas/Restaurante/s
        public JsonResult Restaurante(string nome)
        {
            var restaurante = db.Restaurantes.ToList().Where(x => x.Nome == nome);

            return Json(new
            {
                restaurante = restaurante
            }, JsonRequestBehavior.AllowGet);
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
