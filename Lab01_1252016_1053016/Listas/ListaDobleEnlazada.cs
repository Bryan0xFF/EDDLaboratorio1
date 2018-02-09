using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas
{
    public class NodoDoble<T>
    {
        public T value = default(T);
        public NodoDoble<T> anterior = default(NodoDoble<T>);
        public NodoDoble<T> siguiente = default(NodoDoble<T>);
    }
    public class ListaDobleEnlazada<T> where T: IComparable, ILista<T>
    {
        
    }
}
