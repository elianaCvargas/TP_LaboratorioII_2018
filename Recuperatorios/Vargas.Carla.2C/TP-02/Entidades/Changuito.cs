﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_2017
{
    /// <summary>
    /// No podrá tener clases heredadas.
    /// </summary>
    public sealed class Changuito
    {
        List<Producto> _productos;
        int _espacioDisponible;
        public enum ETipo
        {
            Dulce, Leche, Snacks, Todos
        }

        #region "Constructores"
        private Changuito()
        {
            this._productos = new List<Producto>();
        }
        public Changuito(int espacioDisponible)
            :this()
        {
            this._espacioDisponible = espacioDisponible;
        }
        #endregion

        #region "Sobrecargas"
        /// <summary>
        /// Muestro la concecionaria y TODOS los Productos
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Changuito.Mostrar(this, ETipo.Todos);
        }
        #endregion

        #region "Métodos"

        /// <summary>
        /// Expone los datos del elemento y su lista (incluidas sus herencias)
        /// SOLO del tipo requerido
        /// </summary>
        /// <param name="c">Elemento a exponer</param>
        /// <param name="ETipo">Tipos de ítems de la lista a mostrar</param>
        /// <returns></returns>
        public static string Mostrar(Changuito c, ETipo tipo) //quitar static
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Tenemos {0} lugares ocupados de un total de {1} disponibles", c._productos.Count, c._espacioDisponible);
            sb.AppendLine("");
            foreach (Producto v in c._productos)
            {
                switch (tipo)
                {
                    case ETipo.Snacks:
                        if (v is Snacks)
                        {
                            Snacks snacks = (Snacks)v;
                            sb.AppendLine(snacks.Mostrar());
                        }
                        
                        break;
                    case ETipo.Dulce:
                        if (v is Dulce)
                        {
                            Dulce dulce = (Dulce)v;
                            sb.AppendLine(dulce.Mostrar());
                        }
                        break;
                    case ETipo.Leche:
                        if (v is Leche)
                        {
                            Leche leche = (Leche)v;
                            sb.AppendLine(leche.Mostrar());
                        }
                        break;
                    default:
                        sb.AppendLine(v.Mostrar());
                        break;
                }
            }

            return sb.ToString();
        }
        #endregion

        #region "Operadores"
        /// <summary>
        /// Agregará un elemento a la lista
        /// </summary>
        /// <param name="c">Objeto donde se agregará el elemento</param>
        /// <param name="p">Objeto a agregar</param>
        /// <returns></returns>
        public static Changuito operator +(Changuito c, Producto p)
        {
            int len = c._productos.Count;
                foreach (Producto v in c._productos)
                {
                    if (v == p)
                        return c;
                }
            if (len < c._espacioDisponible)
            {
                c._productos.Add(p);
             
            }
            return c;
        }
        /// <summary>
        /// Quitará un elemento de la lista
        /// </summary>
        /// <param name="c">Objeto donde se quitará el elemento</param>
        /// <param name="p">Objeto a quitar</param>
        /// <returns></returns>
        public static Changuito operator -(Changuito c, Producto p)
        {
            int i = 0;
            int len = c._productos.Count;
            for ( i = 0; i < len; i++)
            {
                if (p == c._productos[i])
                {
                    c._productos.Remove(p);
                    break;
                }
                
            }
                return c;
        }
        #endregion
    }
}
