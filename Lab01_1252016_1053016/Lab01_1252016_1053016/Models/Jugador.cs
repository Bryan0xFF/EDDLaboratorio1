using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab01_1252016_1053016.Models
{
    public class Jugador: IComparable<Jugador>, IEnumerable<Jugador>
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
        public int id { get; set; }

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

        IEnumerator<Jugador> IEnumerable<Jugador>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}