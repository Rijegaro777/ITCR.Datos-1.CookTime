﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime
{
    public class Usuario
    {
        public string contrasena;
        public string email, nombre, apellido, foto;
        public int edad, id;
        public float promedio_calificacion;
        public List<int> seguidos, seguidores, recetas, empresas;
        public List<float> calificaciones;
        public bool is_chef;

        /// <summary>
        /// Clase que representa a un usuario.
        /// </summary>
        /// <param name="email">
        /// El e-mail del usuario.
        /// </param>
        /// <param name="nombre">
        /// El nombre del usuario.
        /// </param>
        /// <param name="apellido">
        /// El apellido del usuario.
        /// </param>
        /// <param name="contrasena">
        /// La contraseña del usuario.
        /// </param>
        /// <param name="edad">
        /// La edad del usuario.
        /// </param>
        public Usuario(String email, String nombre, String apellido, String contrasena, int edad)
        {
            this.email = email;
            this.nombre = nombre;
            this.apellido = apellido;
            this.contrasena = contrasena;
            this.foto = "vacio.jpg";
            this.edad = edad;
            this.id = -1;
            this.promedio_calificacion = 0;
            this.seguidos = new List<int>();
            this.seguidores = new List<int>();
            this.recetas = new List<int>();
            this.empresas = new List<int>();
            this.calificaciones = new List<float>();
            this.is_chef = false;
        }

        public String get_email()
        {
            return email;
        }

        public String get_nombre()
        {
            return nombre;
        }

        public String get_apellido()
        {
            return apellido;
        }

        public String get_contrasena()
        {
            return contrasena;
        }

        public int get_edad()
        {
            return edad;
        }

        public String get_foto()
        {
            return foto;
        }

        public int get_id()
        {
            return id;
        }

        public List<int> get_seguidos()
        {
            return seguidos;
        }

        public List<int> get_seguidores()
        {
            return seguidores;
        }

        public List<int> get_recetas()
        {
            return recetas;
        }

        public List<int> get_empresas()
        {
            return empresas;
        }

        public float get_promedio_calificacion()
        {
            return promedio_calificacion;
        }

        public List<float> get_calificaiones()
        {
            return calificaciones;
        }
    }
}
