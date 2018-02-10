using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab01_1252016_1053016.Models
{
    public class Jugador
    {
        [Display(Name = "Nombre del Jugador:"), Required]
        public string Nombre { get; set; }
        [Display(Name = "Apellido del Jugador:"), Required]
        public string Apellido { get; set; }
        [Display(Name = "Posicion:"), Required]
        public string Posicion { get; set; }
        [Display(Name = "Salario:"), Required]
        public double Salario { get; set; }
        [Display(Name = "Club actual:"), Required]
        public string Club { get; set; }

        public Jugador(string nombre, string apellido, string posicion, double salario, string club)
        {
            Nombre = nombre;
            Apellido = apellido;
            Posicion = posicion;
            Salario = salario;
            Club = club;
        }
    }
}