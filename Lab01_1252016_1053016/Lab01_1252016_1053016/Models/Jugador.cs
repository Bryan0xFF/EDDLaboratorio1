﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab01_1252016_1053016.Models
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Posicion { get; set; }
        public double Salario { get; set; }
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