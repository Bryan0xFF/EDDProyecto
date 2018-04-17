using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ArbolB
{
    public abstract class ArbolBusqueda<TLlave, T> where TLlave : IComparable
    {
        public int Tamaño { get; protected set; }

        public abstract void Agregar(TLlave llave, T dato, ref int count);

        public abstract void Eliminar(TLlave llave);

        public abstract T Obtener(TLlave llave);

        public abstract bool Contiene(TLlave llave);

        public abstract string RecorrerPreOrden();

        public abstract string RecorrerInOrden();

        public abstract string RecorrerPostOrden();

        public abstract int ObtenerAltura();

        public abstract void Cerrar();
    }
}
