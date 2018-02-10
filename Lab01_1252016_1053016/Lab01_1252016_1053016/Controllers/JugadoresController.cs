using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab01_1252016_1053016.Controllers
{
    public class JugadoresController : Controller
    {
        // GET: Jugadores
        public ActionResult Index()
        {
            return View();
        }

        internal ActionResult ListaGenerica()
        {

            return View();
        }

        internal ActionResult ListaNET()
        {
            return View();
        }

        // GET: Jugadores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Jugadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jugadores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugadores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Jugadores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugadores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Jugadores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
