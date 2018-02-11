using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListasDLL;
using Lab01_1252016_1053016.Models;

namespace Lab01_1252016_1053016.Controllers
{
    public class JugadoresController : Controller
    {
        

        // GET: Jugadores
        public ActionResult Index()
        {
            Session["ListaGenerica"] = Session["ListaGenerica"] ?? JugadorDLLGenerica;
            Session["ListaNET"] = Session["ListaNET"] ?? JugadorDLLNET;
            Session["BoolOpcion"] = Session["BoolOpcion"] ?? opcion;
            Session["Path"] = Session["Path"] ?? path;
            return View();
        }

        DoubleLinkedList<Jugador> JugadorDLLGenerica = new DoubleLinkedList<Jugador>();
        LinkedList<Jugador> JugadorDLLNET = new LinkedList<Jugador>();
        string path;
        bool[] opcion = new bool[2];

        public ActionResult ListaGenerica()
        {
            // opcion 0 es la seleccion de lista generica mientras que la 1 es la lista de .NET

            opcion = (bool[])Session["BoolOpcion"];

            if (opcion[1] == true)
            {
                opcion[1] = false;
            }
            opcion[0] = true;

            Session["BoolOpcion"] = opcion;

            return RedirectToAction("Menu");

        }

        public ActionResult ListaNET()
        {
            opcion = (bool[])Session["BoolOpcion"];

            if (opcion[0] == true)
            {
                opcion[0] = false;
            }
            opcion[1] = true;

            Session["BoolOpcion"] = opcion;

            return RedirectToAction("Menu");
        }

        
        public ActionResult Menu()
        {
            opcion = (bool[])Session["BoolOpcion"];

            if (opcion[0] == false && opcion[1] == false)
            {
                //no se ha seleccionado ninguna lista y se redirige a index
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: Jugadores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Jugadores/Create
        [HttpGet]
        public ActionResult Create()
        {
            Jugador jugador = new Jugador();
            return View(jugador);
        }

        // POST: Jugadores/Create
        [HttpPost]
        public ActionResult Create(Jugador jugadorCrear)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                }
                else
                {

                }
                    

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

        public ActionResult Buscar()
        {
            throw new NotImplementedException();
        }

        //Jugadores/LecturaArchivo
        [HttpGet]
        public ActionResult LecturaArchivo()
        {
            //aqui se abre una vista para poder subir el archivo
            return View();
        }


        [HttpPost]
        public ActionResult LecturaArchivo(HttpFileCollectionBase file)
        {
            //aqui se obtiene el path del archivo se abre y se guarda en cualquiera de las 2 listas
            //el HttpFileCollectionBase es para recibir el archivo que subiste.
            path = (string)Session["Path"];

            //Alex aqui agregas la ruta y la guardas
            path = "";

            throw new NotImplementedException();
        }
    }
}
