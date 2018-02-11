using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab01_1252016_1053016.Models
{
    public class Jugador: IComparable<Jugador>
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
            
        private static int CompareByName(Jugador x, Jugador y)
        {
            return x.Nombre.CompareTo(y.Nombre);
        }
        private static int CompareByLastName(Jugador x, Jugador y)
        {
            return x.Apellido.CompareTo(y.Apellido);
        }
        private static int CompareByPosicion(Jugador x, Jugador y)
        {
            return x.Posicion.CompareTo(y.Posicion);
        }
        private static int CompareBySalary(Jugador x, Jugador y)
        {
            return x.Salario.CompareTo(y.Salario);
        }
        private static int CompareByClub(Jugador x, Jugador y)
        {
            return x.Club.CompareTo(y.Club);
        }

        public int CompareTo(Jugador other)
        {
            return this.Nombre.CompareTo(other.Nombre);
        }
    }
}