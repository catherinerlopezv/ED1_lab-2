using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEstructure
{
    public class Pila<T>
    {

        public Nodo<T> Inicial;
        public Nodo<T> Final;

        public Pila()
        {
            Inicial = null;
            Final = null;
        }

        public void Insertar_Inicial(Nodo<T> nuevo)
        {
            if (EstaVacia())
            {
                Inicial = nuevo;
                Final = nuevo;
            }
            else
            {
                nuevo.Siguiente = Inicial;
                Inicial = nuevo;
            }
        }

        void Eliminar_Final()
        {
            if (EstaVacia())
            {
                return;
            }

            Nodo<T> actual = Inicial;

            //Tenemos un solo Nodo<T>
            if (actual.Siguiente == null)
            {
                Inicial = null;
                Final = null;
                return;
            }

            while (actual.Siguiente != Final)
            {
                actual = actual.Siguiente;
            }

            actual.Siguiente = null;
            Final = actual;
        }

        public bool EstaVacia()
        {
            return (Inicial == null) && (Final == null);
        }

        public T Mostrar()
        {
            Nodo<T> Aux = Inicial;
            return Aux.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Nodo<T> current = Inicial;

            while (current != null)
            {
                yield return current.Data;
                current = current.Siguiente;
            }
        }
    }
}
