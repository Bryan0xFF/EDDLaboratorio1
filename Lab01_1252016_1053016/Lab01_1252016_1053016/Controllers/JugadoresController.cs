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

        DoubleLinkedList<Jugador> JugadorDLLGenerica = new DoubleLinkedList<Jugador>();
        LinkedList<Jugador> JugadorDLLNET = new LinkedList<Jugador>();
        List<Jugador> listaSearch = new List<Jugador>();
        Stopwatch sw = new Stopwatch();
        List<string> logs = new List<string>();
        string path;
        int contador;
        bool[] opcion = new bool[2];
        bool[] opcionBusqueda = new bool[4];
        public delegate Jugador SearchByName(DoubleLinkedList<Jugador> list, string value);
        public delegate Jugador SearchBySalary(DoubleLinkedList<Jugador> list, string value, string opcion);
        public delegate Jugador SearchByPosition(DoubleLinkedList<Jugador> list, string value);
        public delegate Jugador SearchByClub(DoubleLinkedList<Jugador> list, string value);

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
            Session["BoolOpcionBusqueda"] = Session["BoolOpcionBusqueda"] ?? opcionBusqueda;
            return View();
        }

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
                    PrintTimeEllapsed(logs); 
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
        [HttpGet]
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

        public ActionResult EditRedirectioner()
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
                    return View("GenericSuccess",JugadorDLLGenerica);
            }
            else
            {
                if (JugadorDLLNET.Count == 0)
                    return RedirectToAction("Menu");
                else
                    return View("NETSuccess", JugadorDLLNET);
            }
        }

        public ActionResult DeleteRedirectioner()
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
                    return View("GenericSuccess", JugadorDLLGenerica);
            }
            else
            {
                if (JugadorDLLNET.Count == 0)
                    return RedirectToAction("Menu");
                else
                    return View("NETSuccess", JugadorDLLNET);
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
                    PrintTimeEllapsed(logs);
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
                    PrintTimeEllapsed(logs);
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

        public ActionResult Busqueda()
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
                    return View("MenuBusqueda");
            }
            else
            {
                if (JugadorDLLNET.Count == 0)
                    return RedirectToAction("Menu");
                else
                    return View("MenuBusqueda");
            }
        }

        [HttpGet]
        public ActionResult BusquedaNombre()
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
                    return View("BusquedaNombre");
            }
            else
            {
                if (JugadorDLLNET.Count == 0)
                    return RedirectToAction("Menu");
                else
                    return View("BusquedaNombre");
            }
        }

        [HttpPost]
        public ActionResult BusquedaNombre(string nombre, string apellido)
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
                    return View("BusquedaNombre");
            }
            else
            {
                if (JugadorDLLNET.Count == 0)
                    return RedirectToAction("Menu");
                else
                    return View("BusquedaNombre");
            }
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
                //PrintTimeEllapsed(logs);
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
                                Jugador searchingPlayer = new Jugador();
                                var linea = reader.ReadLine();
                                var values = linea.Split(';');

                                searchingPlayer.Nombre = values[0];
                                searchingPlayer.Apellido = values[1];
                                searchingPlayer.Posicion = values[2];
                                searchingPlayer.Salario = Convert.ToDouble(values[3]);
                                searchingPlayer.Club = values[4];
                                SearchAndDelete(searchingPlayer.Nombre, searchingPlayer.Apellido, searchingPlayer.Club);  
                            }
                            Session["ListaGenerica"] = JugadorDLLGenerica;
                            Session["Contador"] = contador;
                        }
                        else if (opcion[1] == true)
                        {
                            while (!reader.EndOfStream)
                            {
                                Jugador searchingPlayer = new Jugador();
                                var linea = reader.ReadLine();
                                var values = linea.Split(';');

                                searchingPlayer.Nombre = values[0];
                                searchingPlayer.Apellido = values[1];
                                searchingPlayer.Posicion = values[2];
                                searchingPlayer.Salario = Convert.ToDouble(values[3]);
                                searchingPlayer.Club = values[4];
                                SearchAndDelete(searchingPlayer.Nombre, searchingPlayer.Apellido, searchingPlayer.Club);
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
                PrintTimeEllapsed(logs); 
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

        private void PrintTimeEllapsed(List<string> logs)
        {
            StreamWriter writer = new StreamWriter(@"C:\Users\Bryan Mejía\Desktop",false);
            
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
        /// 
        

        private void SearchAndDelete(string nombre, string apellido, string club)
        {
            opcion = (bool[])Session["BoolOpcion"]; 

            if (opcion[0] == true)
            {
                JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];

                for (int i = 0; i < JugadorDLLGenerica.size(); i++)
                {
                    string Nombre = JugadorDLLGenerica.GetElementAtPos(i).Nombre;
                    string Apellido = JugadorDLLGenerica.GetElementAtPos(i).Apellido;
                    string Club = JugadorDLLGenerica.GetElementAtPos(i).Club;
                    Node<Jugador> delete = JugadorDLLGenerica.GetAnyNode(i);

                    if (Nombre == nombre && Apellido == apellido && Club == club)                    
                        JugadorDLLGenerica.remove(delete);
                                      
                }
            }
            else
            if (opcion[1] == true)
            {
                JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];

                for (int i = 0; i < JugadorDLLNET.Count; i++)
                {
                    string Nombre = JugadorDLLNET.ElementAt(i).Nombre;
                    string Apellido = JugadorDLLNET.ElementAt(i).Apellido;
                    string Club = JugadorDLLNET.ElementAt(i).Club;
                    Jugador delete = JugadorDLLNET.ElementAt(i);


                    if (JugadorDLLNET.ElementAt(i).Nombre == nombre && JugadorDLLNET.ElementAt(i).Apellido
                        == apellido && JugadorDLLNET.ElementAt(i).Club == club)
                        JugadorDLLNET.Remove(delete); 
                                       
                }
                Session["BusquedaGenerica"] = listaSearch;
            }       
        }

        [HttpGet]
        public ActionResult Search()
        {
            opcion = (bool[])Session["BoolOpcion"];

            if (opcion[0] == true)
            {
                JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
                ViewData["Generic"] = JugadorDLLGenerica;
                return View(JugadorDLLGenerica);
            }
            else if (opcion[1] == true)
            {
                JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
                ViewData["Generic"] = JugadorDLLGenerica;
                return View(JugadorDLLNET);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Crear DropDownList para seleccionar campo a buscar
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            //Aqui esta lo que te digo Alex, necesito que los guardes en este orden!!!!
            string value = form.GetKey(0);
            string searchBy = form.GetKey(1);
            string extra = form.GetKey(2);
            JugadorDLLGenerica = (DoubleLinkedList<Jugador>)Session["ListaGenerica"];
            JugadorDLLNET = (LinkedList<Jugador>)Session["ListaNET"];
            DoubleLinkedList<Jugador> tempList = new DoubleLinkedList<Jugador>();

            //esta vacia la lista generica y crea una lista temporal generica con los datos de los jugadores
            if (JugadorDLLGenerica.isEmpty())
            {
                for (int i = 0; i < JugadorDLLNET.Count; i++)
                {
                    tempList.addLast(JugadorDLLNET.ElementAt(i));
                }
            }

            if (searchBy == "NOMBRE")
            {
                SearchByName searchByName = new SearchByName(Jugador.SearchByNames);
                tempList.Search(searchByName, value);
            }
            if (searchBy == "POSICION")
            {
                SearchByPosition searchByPosition = new SearchByPosition(Jugador.SearchByPosicion);
                tempList.Search(searchByPosition, value);
            }

            if (searchBy == "Salario")
            {
                SearchBySalary searchBySalary = new SearchBySalary(Jugador.SearchBySalario);
                tempList.Search(searchBySalary, value);
            }

            return View(tempList);
        }

    }
}
