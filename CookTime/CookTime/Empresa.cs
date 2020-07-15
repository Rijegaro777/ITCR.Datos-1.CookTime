using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CookTime
{
    public class Empresa
    {
        public String nombre, logo, contacto, horario, ubicacion;
        public int id;

        /// <summary>
        /// Clase que representa una empresa.
        /// </summary>
        /// <param name="nombre">El nombre de la empresa.</param>
        /// <param name="contacto">El método de contacto de la empresa.</param>
        /// <param name="horario">El horario de atención de la empresa.</param>
        public Empresa(String nombre, String contacto, String horario, string ubicacion)
        {
            this.nombre = nombre;
            this.logo = "vacio.jpg";
            this.contacto = contacto;
            this.horario = horario;
            this.ubicacion = ubicacion;
            this.id = Math.Abs(nombre.GetHashCode());
        }

        public String get_nombre()
        {
            return nombre;
        }

        public String get_logo()
        {
            return logo;
        }

        public String get_contacto()
        {
            return contacto;
        }

        public String get_horario()
        {
            return horario;
        }

        public String get_ubicacion()
        {
            return ubicacion;
        }

        public int get_id()
        {
            return id;
        }
    }
}
