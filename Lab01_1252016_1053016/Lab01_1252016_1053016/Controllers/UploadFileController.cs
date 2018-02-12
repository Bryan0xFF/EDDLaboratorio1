using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using Lab01_1252016_1053016.Models; 

namespace Lab01_1252016_1053016.Controllers
{
    public class UploadFileController : Controller
    {       
        List<Jugador> listaJugadores = new List<Jugador>();

        public ActionResult Index()
        {
            return View();
        }

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("application/vnd.ms-excel");
        }

        [HttpPost]
        public ActionResult Load(HttpPostedFileBase File)
        {
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("Index");
            }          
            else
                if (!isValidContentType(File.ContentType))
                {
                ViewBag.Error = "Solo archivos CSV son válidos para la entrada";
                return View("Index");
                }
            else 
                if(File.ContentLength > 0)
                {
                var fileName = Path.GetFileName(File.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/csv"), fileName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path); 
                File.SaveAs(path);
                //ViewBag.fileName = File.FileName;
                using (var reader = new StreamReader(path))
                {
                    //Seleccion de tipo de lista utilizar
                    //Realizar if donde dependiendo el booleano es la lista que se va a seleccionar                    
                    
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
                        listaJugadores.Add(newJugador);                        
                    }
                }  
                ViewBag.listaJugadores = listaJugadores;
            
            }
            return View("Success"); 
        }

    }
}
