using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using ListasDLL;

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
        public int id { get; set; }       

        private static int CompareByName(Jugador x, Jugador y)
        {
            return x.Nombre.CompareTo(y.Nombre);
        }
        private static int CompareByLastName(Jugador x, Jugador y)
        {
            return x.Apellido.CompareTo(y.Apellido);
        }
        public static Jugador SearchByNames(DoubleLinkedList<Jugador> lista,string nombre)
        {
            Jugador jugador = new Jugador();

            if (!lista.isEmpty())
            {

                for (int i = 0; i < lista.size(); i++)
                {
                    if (lista.GetElementAtPos(i).Nombre == nombre)
                    {
                        jugador = lista.GetElementAtPos(i);
                        break;
                    }
                }

                return jugador;
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
        public static  Jugador SearchByPosicion(DoubleLinkedList<Jugador> lista,string posicion)
        {
            Jugador jugador = new Jugador();

            if (!lista.isEmpty())
            {

                for (int i = 0; i < lista.size(); i++)
                {
                    if (lista.GetElementAtPos(i).Posicion == posicion)
                    {
                        jugador = lista.GetElementAtPos(i);
                        break;
                    }
                }
                return jugador;
            }
            else
            {
                throw new ArgumentNullException("No hay ninguna lista seleccionada");
            }
        }

        public static Jugador SearchBySalario(DoubleLinkedList<Jugador> lista, string argumento, string rangoSalarial)
        {

            Jugador jugador = new Jugador();
            double rango = double.Parse(rangoSalarial);
            //poner argumento a todo mayusculas para evitar errores por sintaxis
            switch (argumento)
            {
                case "MAYOR":
                    if (!lista.isEmpty())
                    {

                        for (int i = 0; i < lista.size(); i++)
                        {
                            if (lista.GetElementAtPos(i).Salario > rango)
                            {
                                jugador = lista.GetElementAtPos(i);
                                break;
                            }
                        }
                    }
                    
                    else
                    {
                        throw new ArgumentNullException("No hay ninguna lista seleccionada");
                    }

                    break;

                case "MENOR":
                    if (!lista.isEmpty())
                    {

                        for (int i = 0; i < lista.size(); i++)
                        {
                            if (lista.GetElementAtPos(i).Salario < rango)
                            {
                                jugador = lista.GetElementAtPos(i);
                                break;
                            }
                        }
                    }

                    else
                    {
                        throw new ArgumentNullException("No hay ninguna lista seleccionada");
                    }
                    break;

                case "IGUAL":
                    if (!lista.isEmpty())
                    {

                        for (int i = 0; i < lista.size(); i++)
                        {
                            if (lista.GetElementAtPos(i).Salario == rango)
                            {
                                jugador = lista.GetElementAtPos(i);
                                break;
                            }
                        }
                    }

                    else
                    {
                        throw new ArgumentNullException("No hay ninguna lista seleccionada");
                    }
                    break;
                default:
                    break;
            }
            return jugador;
        }

        public static  Jugador SearchByClub(DoubleLinkedList<Jugador> lista,string club)
        {
            Jugador jugador = new Jugador();

            if (!lista.isEmpty())
            {

                for (int i = 0; i < lista.size(); i++)
                {
                    if (lista.GetElementAtPos(i).Club == club)
                    {
                        jugador = lista.GetElementAtPos(i);
                    }
                }

                return jugador;
            }
            else
            {
                throw new ArgumentNullException("No hay ninguna lista seleccionada");
            }
        }

        public int CompareTo(Jugador other)
        {
            throw new NotImplementedException();
        }

        
    }
}