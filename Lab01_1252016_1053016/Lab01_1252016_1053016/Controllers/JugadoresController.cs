using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListasDLL;
using Lab01_1252016_1053016.Models;
using System.IO;
using System.Diagnostics; 

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
            Session["Contador"] = Session["Contador"] ?? contador;
            Session["Logs"] = Session["Logs"] ?? logs;
            Session["BusquedaGenerica"] = Session["BusquedaGenerica"] ?? listaSearch;
            return View();
        }

        DoubleLinkedList<Jugador> JugadorDLLGenerica = new DoubleLinkedList<Jugador>();
        LinkedList<Jugador> JugadorDLLNET = new LinkedList<Jugador>();
        DoubleLinkedList<Jugador> listaSearch = new DoubleLinkedList<Jugador>();
        Stopwatch sw = new Stopwatch();
        List<string> logs = new List<string>();          
        string path;
        int contador;
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
        public ActionResult Details()
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
                sw.Restart(); 
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    opcion = (bool[])Session["BoolOpcion"];
                    JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                    JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                    contador = (int)Session["Contador"];
                    logs = (List<string>)Session["Logs"]; 

                    //se esta trabajando en la lista generica
                    if (opcion[0]== true)
                    {
                        jugadorCrear.id = contador;
                        contador = contador++;
                        if(JugadorDLLGenerica.isEmpty())
                        {
                            JugadorDLLGenerica.addFirst(jugadorCrear);
                        }
                        else
                        {
                            JugadorDLLGenerica.addLast(jugadorCrear); 
                        }
                        
                    }
                    else if (opcion[1] == true)
                    {
                        jugadorCrear.id = contador;
                        contador = contador++;
                        if (JugadorDLLNET.Count() == 0)
                        {
                            JugadorDLLNET.AddFirst(jugadorCrear);
                        }
                        else
                        {
                            JugadorDLLNET.AddLast(jugadorCrear);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    sw.Stop();
                    logs.Add("El tiempo tardado para crear fue: " + sw.Elapsed.ToString());                    
                    PrintCreateTimeEllapsed(logs); 
                }
                else
                {
                    throw new Exception("No tiene ninguna lista seleccionada"); 
                }
                //if para retornar la view que haya elegido el usuario
                if (opcion[0] == true)                
                    return View("GenericSuccess", JugadorDLLGenerica);                
                else      
                    return View("NETSuccess", JugadorDLLNET);                
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugadores/Edit/5
        public ActionResult Edit()
        {
            opcion = (bool[])Session["BoolOpcion"];
            JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
            JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
            contador = (int)Session["Contador"];

            if (opcion[0] == true)
            {
                if (JugadorDLLGenerica.size() == 0)
                    return RedirectToAction("Menu");
                else
                    return View();
            }
            else
            {
                if (JugadorDLLNET.Count == 0)
                    return RedirectToAction("Menu");
                else
                    return View();
            }
        }

        // POST: Jugadores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Jugador jugadorEditar)
        {
            try
            {
                sw.Restart(); 
                
                // TODO: Add insert logic here          
                {
                    opcion = (bool[])Session["BoolOpcion"];
                    JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                    JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                    contador = (int)Session["Contador"];
                    logs = (List<string>)Session["Logs"];

                    //se esta trabajando en la lista generica
                    if (opcion[0] == true)
                    {
                       replaceGeneric(id, jugadorEditar); 
                    }
                    else if (opcion[1] == true)
                    {
                        replaceNET(id, jugadorEditar); 
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    sw.Stop();
                    logs.Add("El tiempo tardado para editar fue: " + sw.Elapsed.ToString());
                    PrintCreateTimeEllapsed(logs);
                }
                //if para retornar la view que haya elegido el usuario
                if (opcion[0] == true)
                    return View("GenericSuccess", JugadorDLLGenerica);
                else
                    return View("NETSuccess", JugadorDLLNET);
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugadores/Delete/5
        public ActionResult Delete()
        {
            opcion = (bool[])Session["BoolOpcion"];
            JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
            JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
            contador = (int)Session["Contador"];

            if (opcion[0] == true)
            {
                if (JugadorDLLGenerica.size() == 0)
                    return RedirectToAction("Menu");
                else
                    return View();
            }
            else
            {
                if (JugadorDLLNET.Count == 0)
                    return RedirectToAction("Menu");
                else
                    return View();
            };
        }

        // POST: Jugadores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                sw.Restart(); 
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    opcion = (bool[])Session["BoolOpcion"];
                    JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                    JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                    contador = (int)Session["Contador"];
                    logs = (List<string>)Session["Logs"];

                    //se esta trabajando en la lista generica
                    if (opcion[0] == true)
                    {
                        Node<Jugador> delete = JugadorDLLGenerica.GetAnyNode(id); 
                        JugadorDLLGenerica.remove(delete);                      
                    }
                    else if (opcion[1] == true)
                    {
                        Jugador delete = JugadorDLLNET.ElementAt(id);
                        JugadorDLLNET.Remove(delete); 
                    }
                    else
                    {
                        return RedirectToAction("Menu");
                    }
                    sw.Stop();
                    logs.Add("El tiempo tardado para eliminar fue: " + sw.Elapsed.ToString());
                    PrintCreateTimeEllapsed(logs);
                }
                else
                {
                    throw new Exception("No tiene ninguna lista seleccionada");
                }
                //if para retornar la view que haya elegido el usuario
                if (opcion[0] == true)
                    return View("GenericSuccess", JugadorDLLGenerica);
                else
                    return View("NETSuccess", JugadorDLLNET);
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

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("application/vnd.ms-excel");
        }

        //Jugadores/LecturaArchivo
        [HttpGet]
        public ActionResult LecturaArchivo()
        {
            //aqui se abre una vista para poder subir el archivo
            return View();
        }

        [HttpPost]
        public ActionResult LecturaArchivo(HttpPostedFileBase File)
        {
            sw.Restart();
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("Index");
            }
            else
            {
                if (!isValidContentType(File.ContentType))
                {
                    ViewBag.Error = "Solo archivos CSV son válidos para la entrada";
                    return View("Index");
                }

                if (File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/csv"), fileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    File.SaveAs(path);               
                    using (var reader = new StreamReader(path))
                    {
                        //Seleccion de tipo de lista utilizar
                        opcion = (bool[])Session["BoolOpcion"];
                        JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                        JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                        contador = (int)Session["Contador"];
                        logs = (List<string>)Session["Logs"];

                        //Realizar if donde dependiendo el booleano es la lista que se va a seleccionar                    
                        if (opcion[0] == true)
                        {
                            while (!reader.EndOfStream)
                            {

                                Jugador newJugador = new Jugador();
                                var linea = reader.ReadLine();
                                var values = linea.Split(';');
                                newJugador.Nombre = values[0];
                                newJugador.Apellido = values[1];
                                newJugador.Posicion = values[2];
                                newJugador.Salario = Convert.ToDouble(values[3]);
                                newJugador.Club = values[4];
                                newJugador.id = contador;
                                contador = contador++;
                                if (JugadorDLLGenerica.isEmpty())
                                {
                                    JugadorDLLGenerica.addFirst(newJugador);
                                }
                                else
                                {
                                    JugadorDLLGenerica.addLast(newJugador);
                                }
                                
                            }
                            Session["ListaGenerica"] = JugadorDLLGenerica;
                            Session["Contador"] = contador;
                        }
                        else if (opcion[1] == true)
                        {
                            while (!reader.EndOfStream)
                            {
                                Jugador newJugador = new Jugador();
                                var linea = reader.ReadLine();
                                var values = linea.Split(';');
                                newJugador.Nombre = values[0];
                                newJugador.Apellido = values[1];
                                newJugador.Posicion = values[2];
                                newJugador.Salario = Convert.ToDouble(values[3]);
                                newJugador.Club = values[4];
                                newJugador.id = contador;
                                contador = contador++;
                                if (JugadorDLLNET.Count() == 0)
                                {
                                    JugadorDLLNET.AddFirst(newJugador);
                                }
                                else
                                {
                                    JugadorDLLNET.AddLast(newJugador);
                                }
                                
                            }
                            Session["ListaNET"] = JugadorDLLNET;
                            Session["Contador"] = contador;
                        }
                        // no es ninguno de los 2 
                        else
                        {
                            return RedirectToRoute("Jugadores/Index");
                        }
                    }
                }
                sw.Stop();
                logs.Add("El tiempo tardado para leer archivo y crear fue: " + sw.Elapsed.ToString());
                PrintCreateTimeEllapsed(logs);
            }

            if (opcion[0] == true)
            {
                JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                return View("GenericSuccess",JugadorDLLGenerica);
            }
            else
            {
                JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                return View("NETSuccess",JugadorDLLNET);
            }
        }

        [HttpGet]
        public ActionResult LecturaArchivoDelete()
        {
            //aqui se abre una vista para poder subir el archivo
            return View();
        }

        [HttpPost]
        public ActionResult LecturaArchivoDelete(HttpPostedFileBase File)
        {
            sw.Restart();
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("Menu");
            }
            else
            {
                if (!isValidContentType(File.ContentType))
                {
                    ViewBag.Error = "Solo archivos CSV son válidos para la entrada";
                    return View("Menu");
                }

                if (File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/csv"), fileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    File.SaveAs(path);
                    using (var reader = new StreamReader(path))
                    {
                        //Seleccion de tipo de lista utilizar
                        opcion = (bool[])Session["BoolOpcion"];
                        JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                        JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                        contador = (int)Session["Contador"];
                        logs = (List<string>)Session["Logs"];

                        //Realizar if donde dependiendo el booleano es la lista que se va a seleccionar                    
                        if (opcion[0] == true)
                        {
                            while (!reader.EndOfStream)
                            {

                                // buscar nodo para poder eliminar
                               // JugadorDLLGenerica.remove(delete); 

                            }
                            Session["ListaGenerica"] = JugadorDLLGenerica;
                            Session["Contador"] = contador;
                        }
                        else if (opcion[1] == true)
                        {
                            while (!reader.EndOfStream)
                            {
                                // buscar nodo para poder eliminar
                                // JugadorDLLNET.remove(delete); 
                            }
                            Session["ListaNET"] = JugadorDLLNET;
                            Session["Contador"] = contador;
                        }
                        // no es ninguno de los 2 
                        else
                        {
                            return RedirectToRoute("Jugadores/Index");
                        }
                    }
                }
                sw.Stop();
                logs.Add("El tiempo tardado para leer archivo y eliminar fue: " + sw.Elapsed.ToString());

            }

            if (opcion[0] == true)
            {
                JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                return View("GenericSuccess", JugadorDLLGenerica);
            }
            else
            {
                JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                return View("NETSuccess", JugadorDLLNET);
            }
        }

        private void replaceNET(int position, Jugador newData)
        {
            Jugador updating = JugadorDLLNET.ElementAt(position);            
            updating.Salario = newData.Salario;
            updating.Club = newData.Club;
        }

        private void replaceGeneric(int position, Jugador newData)
        {
            Jugador updating = JugadorDLLGenerica.GetElementAtPos(position);
            updating.Salario = newData.Salario;
            updating.Club = newData.Club;
        }

        private void PrintCreateTimeEllapsed(List<string> logs)
        {
            StreamWriter writer = new StreamWriter(@"..\\..\\..\\log.txt",false);
            
            for (int i = 0; i <logs.Count; i++)
            {
                writer.WriteLine(logs.ElementAt(i));
            }
            writer.Close(); 
        }

        /// <summary>
        /// guarda en la listaSearch los datos indexados que cumplen las condiciones requeridas
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        private void SearchByNames(string nombre, string apellido)
        {
            opcion = (bool[])Session["BoolOpcion"];

            if (opcion[0] == true)
            {
                JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];

                for (int i = 0; i < JugadorDLLGenerica.size(); i++)
                {
                    if (JugadorDLLGenerica.GetElementAtPos(i).Nombre == nombre && JugadorDLLGenerica.GetElementAtPos(i).Apellido
                        == apellido)
                    {
                        Jugador jugador = JugadorDLLGenerica.GetElementAtPos(i);
                        listaSearch.addLast(jugador);
                    }
                }
            }
            if (opcion[1] == true)
            {
                JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];

                for (int i = 0; i < JugadorDLLGenerica.size(); i++)
                {
                    if (JugadorDLLNET.ElementAt(i).Nombre == nombre && JugadorDLLNET.ElementAt(i).Apellido
                        == apellido)
                    {
                        Jugador jugador = JugadorDLLNET.ElementAt(i);
                        listaSearch.addLast(jugador);
                    }
                }
                Session["BusquedaGenerica"] = listaSearch;
            }
            else
            {
                throw new ArgumentNullException("No se ha creado ninguna lista");
            }
        }

        /// <summary>
        /// guarda en la listaSearch los datos indexados que cumplen las condiciones requeridas
        /// </summary>
        /// <param name="posicion"></param>
        private void SearchByPosicion(string posicion)
        {
            opcion = (bool[])Session["BoolOpcion"];

            if (opcion[0] == true)
            {
                JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];

                for (int i = 0; i < JugadorDLLGenerica.size(); i++)
                {
                    if (JugadorDLLGenerica.GetElementAtPos(i).Posicion == posicion)
                    {
                        listaSearch.addLast(JugadorDLLGenerica.GetElementAtPos(i));
                    }
                }

                Session["BusquedaGenerica"] = listaSearch;
            }
            if (opcion[1] == true)
            {
                JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];

                for (int i = 0; i < JugadorDLLGenerica.size(); i++)
                {
                    if (JugadorDLLGenerica.GetElementAtPos(i).Posicion == posicion)
                    {
                        listaSearch.addLast(JugadorDLLNET.ElementAt(i));
                    }
                }
                Session["BusquedaGenerica"] = listaSearch;
            }
            else
            {
                throw new ArgumentNullException("No hay ninguna lista seleccionada");
            }
        }

        private void SearchBySalario(string argumento, double rangoSalarial)
        {

            opcion = (bool[])Session["BoolOpcion"];

            //poner argumento a todo mayusculas para evitar errores por sintaxis
            switch (argumento)
            {
                case "MAYOR":
                    if (opcion[0] == true)
                    {
                        JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];

                        for (int i = 0; i < JugadorDLLGenerica.size(); i++)
                        {
                            if (JugadorDLLGenerica.GetElementAtPos(i).Salario > rangoSalarial)
                            {
                                listaSearch.addLast(JugadorDLLGenerica.GetElementAtPos(i));
                            }
                        }

                        Session["BusquedaGenerica"] = listaSearch;
                    }
                    if (opcion[1] == true)
                    {
                        JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];

                        for (int i = 0; i < JugadorDLLGenerica.size(); i++)
                        {
                            if (JugadorDLLGenerica.GetElementAtPos(i).Salario > rangoSalarial)
                            {
                                listaSearch.addLast(JugadorDLLNET.ElementAt(i));
                            }
                        }
                        Session["BusquedaGenerica"] = listaSearch;
                    }
                    else
                    {
                        throw new ArgumentNullException("No hay ninguna lista seleccionada");
                    }

                    break;

                case "MENOR":
                    //logica caso menor
                    break;

                case "IGUAL":
                    //logica caso igual
                    break;
                default:
                    break;
            }
        }
    }
}
