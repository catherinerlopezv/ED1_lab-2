using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEstructure
{
    public class ArbolAVL
    {
        public string Linea;
        public int Altura;
        public string Nombre_farmaco;
        public ArbolAVL NodoIz;
        public ArbolAVL NodoDer;
        public ArbolAVL NodoPadre;
        int contador;
        string OrdenamientoPre;
        string OrdenamientoIn;
        string OrdenamientoPost;

        public ArbolAVL()
        {
        }

        //Constructor
        public ArbolAVL(string Linea, string No_farma, ArbolAVL Izquierdo, ArbolAVL Derecho, ArbolAVL Padre)
        {
            this.Linea = Linea;
            this.Nombre_farmaco = No_farma;
            NodoIz = Izquierdo;
            NodoDer = Derecho;
            NodoPadre = Padre;
            this.Altura = 0;
        }
        //Buscar un valor en el arbol
        public string buscar(string valorBuscar, ArbolAVL Raiz)
        {
            if (Raiz == null) return null;
            if (string.Compare(valorBuscar, Raiz.Nombre_farmaco) == -1) { return buscar(valorBuscar, Raiz.NodoIz); }
            if (string.Compare(valorBuscar, Raiz.Nombre_farmaco) == 1) { return buscar(valorBuscar, Raiz.NodoDer); }
            return Raiz.Linea;
        }  
        //Funcion para insertar un nuevo valor en el arbol AVL
        public ArbolAVL Insertar(string NLinea, string No_farma, ArbolAVL Raiz)
        {

            if(Raiz == null)
            {
                Raiz = new ArbolAVL(NLinea, No_farma, null, null, null);
            }else if (string.Compare(No_farma, Raiz.Nombre_farmaco) == -1)
            {
                Raiz.NodoIz = Insertar(NLinea, No_farma, Raiz.NodoIz);
            }else if(string.Compare(No_farma, Raiz.Nombre_farmaco) == 1)
            {
                Raiz.NodoDer = Insertar(NLinea, No_farma, Raiz.NodoDer);
            }
            else
            {
                //MessageBox.Show("Valor Existente en el Arbol", "Error", MessageBoxButtons.OK);
            }

            //Realiza las rotaciones simples o dobles segun el caso
            if (Alturas(Raiz.NodoIz) - Alturas(Raiz.NodoDer) == 2)
            {
                
                if (string.Compare(No_farma, Raiz.NodoIz.Nombre_farmaco) == -1)
                    Raiz = RotacionIzquierdaSimple(Raiz);
                else
                    Raiz = RotacionIzquierdaDoble(Raiz);
            }
            if (Alturas(Raiz.NodoDer) - Alturas(Raiz.NodoIz) == 2)
            {
                if (string.Compare(No_farma, Raiz.NodoDer.Nombre_farmaco) == 1)
                    Raiz = RotacionDerechaSimple(Raiz);
                else
                    Raiz = RotacionDerechaDoble(Raiz);
            }

            Raiz.Altura = Max(Alturas(Raiz.NodoIz), Alturas(Raiz.NodoDer)) + 1;
            contador++;
            return Raiz;
        }

        ArbolAVL nodoE, nodoP;
        public ArbolAVL Eliminar(string valorEliminar, ref ArbolAVL Raiz)
        {

            if (Raiz != null)
            {

                if (string.Compare(valorEliminar , Raiz.NodoDer.Nombre_farmaco) == -1)
                {

                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoIz);
                }
                else
                {
                    if (string.Compare(valorEliminar, Raiz.NodoDer.Nombre_farmaco) == 1)
                    {

                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDer);
                    }
                    else
                    {
                        //Posicionado sobre el elemento a eliminar
                        ArbolAVL NodoEliminar = Raiz;
                        if (NodoEliminar.NodoDer == null)
                        {
                            Raiz = NodoEliminar.NodoIz;

                            if (Alturas(nodoE.NodoIz) - Alturas(nodoE.NodoDer) == 2)
                            {
                                //MessageBox.Show("nodoE" + nodoE.valor.ToString());
                                if (string.Compare(valorEliminar, Raiz.Nombre_farmaco) == -1)
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaSimple(nodoE);
                            }
                            if (Alturas(nodoE.NodoDer) - Alturas(nodoE.NodoIz) == 2)
                            {
                                if (string.Compare(valorEliminar, Raiz.Nombre_farmaco) == 1)
                                    nodoE = RotacionDerechaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaDoble(nodoE);
                                nodoP = RotacionDerechaSimple(nodoE);
                            }
                        }
                        else
                        {
                            if (NodoEliminar.NodoIz == null)
                            {
                                Raiz = NodoEliminar.NodoDer;
                            }
                            else
                            {
                                if (Alturas(Raiz.NodoIz) - Alturas(Raiz.NodoDer) > 0)
                                {
                                    ArbolAVL AuxiliarNodo = null;
                                    ArbolAVL Auxiliar = Raiz.NodoIz;
                                    bool Bandera = false;
                                    while (Auxiliar.NodoDer != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDer;
                                        Bandera = true;
                                    }
                                    Raiz.Nombre_farmaco = Auxiliar.Nombre_farmaco;
                                    NodoEliminar = Auxiliar;
                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.NodoDer = Auxiliar.NodoIz;
                                    }
                                else
{
                                        Raiz.NodoIz = Auxiliar.NodoIz;
                                    }
                                    //Realiza las rotaciones simples o dobles segun el caso
                                }
                                else
                                {
                                    if (Alturas(Raiz.NodoDer) - Alturas(Raiz.NodoIz) > 0)
                                    {
                                        ArbolAVL AuxiliarNodo = null;
                                        ArbolAVL Auxiliar = Raiz.NodoDer;
                                        bool Bandera = false;
                                        while (Auxiliar.NodoIz != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoIz;
                                            Bandera = true;
                                        }
                                        Raiz.Nombre_farmaco = Auxiliar.Nombre_farmaco;
                                        NodoEliminar = Auxiliar;
                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.NodoIz = Auxiliar.NodoDer;
                                        }
                                        else
                                        {
                                            Raiz.NodoDer = Auxiliar.NodoDer;
                                        }
                                    }
                                    else
                                    {
                                        if (Alturas(Raiz.NodoDer) - Alturas(Raiz.NodoIz) == 0)
                                        {
                                            ArbolAVL AuxiliarNodo = null;
                                            ArbolAVL Auxiliar = Raiz.NodoIz;
                                            bool Bandera = false;
                                            while (Auxiliar.NodoDer != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.NodoDer;
                                                Bandera = true;
                                            }
                                            Raiz.Nombre_farmaco = Auxiliar.Nombre_farmaco;
                                            NodoEliminar = Auxiliar;
                                            if (Bandera == true)
                                            {
                                                AuxiliarNodo.NodoDer = Auxiliar.NodoIz;
                                            }
                                            else
                                            {
                                                Raiz.NodoIz = Auxiliar.NodoIz;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } 
            else
            {
                // MessageBox.Show("Nodo inexistente en el arbol", "Error", MessageBoxButtons.OK);
            }
            return nodoP;

        }
        //Función para obtener que rama es mayor
        private static int Max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }
        //Seccion de funciones de rotaciones
        //Rotacion Izquierda Simple
        private static ArbolAVL RotacionIzquierdaSimple(ArbolAVL k2)
        {
            ArbolAVL k1 = k2.NodoIz;
            k2.NodoIz = k1.NodoDer;
            k1.NodoDer = k2;
            k2.Altura = Max(Alturas(k2.NodoIz), Alturas(k2.NodoDer)) + 1;
            k1.Altura = Max(Alturas(k1.NodoIz), k2.Altura) + 1;
            return k1;
        }

        //Rotacion Derecha Simple
        private static ArbolAVL RotacionDerechaSimple(ArbolAVL k1)
        {
            ArbolAVL k2 = k1.NodoDer;
            k1.NodoDer = k2.NodoIz;
            k2.NodoIz = k1;
            k1.Altura = Max(Alturas(k1.NodoIz), Alturas(k1.NodoDer)) + 1;
            k2.Altura = Max(Alturas(k2.NodoDer), k1.Altura) + 1;
            return k2;
        }
        //Doble Rotacion Izquierda
        private static ArbolAVL RotacionIzquierdaDoble(ArbolAVL k3)
        {
            k3.NodoIz = RotacionDerechaSimple(k3.NodoIz);
            return RotacionIzquierdaSimple(k3);
        }
        //Doble Rotacion Derecha
        private static ArbolAVL RotacionDerechaDoble(ArbolAVL k1)
        {
            k1.NodoDer = RotacionIzquierdaSimple(k1.NodoDer);
            return RotacionDerechaSimple(k1);
        }
        //Funcion para obtener la altura del arbol
        public int getAltura(ArbolAVL nodoActual)
        {
            if (nodoActual == null)
                return 0;
            else
                return 1 + Math.Max(getAltura(nodoActual.NodoIz), getAltura(nodoActual.NodoDer));
        }

        private static int Alturas(ArbolAVL Raiz)
        {
            return Raiz == null ? -1 : Raiz.Altura;
        }

        public string RecorridoPreorden(ArbolAVL Raiz)
        {
            if (Raiz != null)
            {
                OrdenamientoPre += Raiz.Nombre_farmaco+" - ";
                RecorridoPreorden(Raiz.NodoIz);
                RecorridoPreorden(Raiz.NodoDer);
            }
            return OrdenamientoPre;      
        }

        public string RecorridoInorden(ArbolAVL Raiz)
        {
            if (Raiz != null)
            {
                RecorridoInorden(Raiz.NodoIz);
                OrdenamientoIn += Raiz.Nombre_farmaco + " - ";
                RecorridoInorden(Raiz.NodoDer);
            }
            return OrdenamientoIn;
        }

        public string RecorridoPostorden(ArbolAVL Raiz)
        {
            if (Raiz != null)
            {
                RecorridoPostorden(Raiz.NodoIz);
                RecorridoPostorden(Raiz.NodoDer);
                OrdenamientoPost += Raiz.Nombre_farmaco + " - ";
            }
            return OrdenamientoPost;
        }
    }
}
