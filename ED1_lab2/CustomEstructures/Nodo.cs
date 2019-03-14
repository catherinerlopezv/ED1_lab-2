using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEstructure
{
    public class Nodo<T>
    {
        public T Data;
        public Nodo<T> Siguiente;

        public Nodo(T value)
        {
            this.Data = value;
            Siguiente = null;
        }
    }
}
