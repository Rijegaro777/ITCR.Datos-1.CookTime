using Microsoft.Azure.Storage.Blob.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CookTime
{
    public class Receta
    {
        public String nombre, tipo, porciones, duracion, tiempo, dificultad, dieta, foto, ingredientes, pasos, precio, fecha;
        public int id;
        DateTime dia = DateTime.Today;
        public List<float> calificaciones;
        public float promedio_calificaciones;

        /// <summary>
        /// Clase que representa una receta.
        /// </summary>
        /// <param name="nombre">El nombre de la receta.</param>
        public Receta(String nombre, String tipo, String porciones, String duracion, String tiempo, String dificultad, String dieta, String foto, String ingredientes, String pasos, String precio)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.porciones = porciones;
            this.duracion = duracion;
            this.tiempo = tiempo;
            this.dificultad = dificultad;
            this.dieta = dieta;
            this.foto = foto;
            this.ingredientes = ingredientes;
            this.pasos = pasos;
            this.precio = precio;
            this.fecha = dia.ToShortDateString().ToString();
            this.id = Math.Abs(nombre.GetHashCode());
            this.calificaciones = new List<float>();
            this.promedio_calificaciones = 0;
        }

        public String get_nombre()
        {
            return nombre;
        }

        public String get_tipo()
        {
            return tipo;
        }

        public String get_porciones()
        {
            return porciones;
        }

        public String get_duracion()
        {
            return duracion;
        }

        public String get_tiempo()
        {
            return tiempo;
        }

        public String get_dificultad()
        {
            return dificultad;
        }

        public String get_dieta()
        {
            return dieta;
        }

        public String get_foto()
        {
            return foto;
        }

        public String get_ingredientes()
        {
            return ingredientes;
        }

        public String get_pasos()
        {
            return pasos;
        }

        public String get_precio()
        {
            return precio;
        }
        public String get_fecha()
        {
            return fecha;
        }
        public int get_id()
        {
            return id;
        }

        public List<float> get_calificaciones()
        {
            return calificaciones;
        }

        public float get_promedio_calificaciones()
        {
            return promedio_calificaciones;
        }
    }
}
