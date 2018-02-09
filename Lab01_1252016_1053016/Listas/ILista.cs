using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas
{
    public interface ILista<T> where T: IComparable
    {
        T Agregar(T value);
        bool Eliminar(T datoEliminar);
        T Buscar(T datoBuscar);
        T Sort();
    }
}
