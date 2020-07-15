﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookTime
{
    class Cliente
    {
        private static Cliente instance = null;
        private string uri;
        private HttpClient client;
        private Usuario usuario_actual;

        /// <summary>
        /// Clase singleton de un cliente que se comunica con el servidor.
        /// </summary>
        private Cliente()
        {
<<<<<<< Updated upstream
            this.uri = "http://192.168.1.2:8080/";
=======
            this.uri = "http://192.168.50.13:8080/";
>>>>>>> Stashed changes

            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(this.uri);
            this.client.Timeout = new TimeSpan(0, 0, 15);
        }

        /// <summary>
        /// Retorna una instancia única de Cliente.
        /// </summary>
        /// <returns>
        /// Clase única Cliente.
        /// </returns>
        public static Cliente get_instance()
        {
            if (instance == null)
            {
                instance = new Cliente();
            }
            return instance;
        }

        /// <summary>
        /// Retorna el HttpClient que se usará para comunicarse con el servidor.
        /// </summary>
        /// <returns>
        /// Un HttpServer.
        /// </returns>
        public HttpClient get_client()
        {
            return client;
        }


        /// <summary>
        /// Encripta el string ingresado utilizando hashMD5.
        /// </summary>
        /// <param text>
        /// El string a encriptar.
        /// <returns>
        /// El string original encriptado.
        /// </returns>
        public string hash_MD5(string text)
        {
            byte[] text_bytes = ASCIIEncoding.ASCII.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] text_bytes_MD5 = md5.ComputeHash(text_bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text_bytes_MD5.Length; i++)
            {
                sb.Append(text_bytes_MD5[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// Retorna el uri que se usará para comunicarse con el servidor.
        /// </summary>
        /// <returns>
        /// Un string con la uri.
        /// </returns>
        public string get_uri()
        {
            return uri;
        }

        /// <summary>
        /// Retorna el usuario que está usando la aplicación.
        /// </summary>
        /// <returns>
        /// Un usuario.
        /// </returns>
        public Usuario get_usuario()
        {
            return usuario_actual;
        }

        /// <summary>
        /// Asigna el usuario recibido como el usuario que usa la aplicación.
        /// </summary>
        /// <param name="usuario">El usuario que será asignado</param>
        public void set_usuario(Usuario usuario)
        {
            this.usuario_actual = usuario;
        }

        /// <summary>
        /// Agrega al usuario recibido a la lista de seguidos del usuario que está usando la aplicación y
        /// envía la solicitud al servidor para actualizar el archivo JSON. Elimina la opción de seguir usuarios
        /// que ya están en la lista de seguidos.
        /// </summary>
        /// <param name="usuario">El usuario que será seguido</param>
        /// <param name="grid">El grid del perfil del usuario a seguir</param>
        /// <param name="button">El botón para seguir al usuario</param>
        /// <returns></returns>
        public async Task seguir_usuario(Usuario usuario, Grid grid, Button button)
        {
            grid.Children.Remove(button);
            usuario_actual.get_seguidos().Add(usuario.get_id());
            usuario.get_seguidores().Add(usuario_actual.get_id());

            var response = await client
                .GetStringAsync("/rest/servicios/seguir_usuario?usuario="+usuario_actual.get_id().ToString()+"&usuario_seguido="+usuario.get_id().ToString());
        }

        public async Task puntuar_usuario(Usuario dueno_perfil, string puntuacion, Grid grid, Picker picker, Button boton, Label label)
        {
            grid.Children.Remove(picker);
            grid.Children.Remove(boton);

            string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/puntuar_usuario/" + dueno_perfil.get_id().ToString() + "%" + puntuacion);
            string final_response = response.ToString();

            dueno_perfil.promedio_calificacion = float.Parse(final_response);

            label.Text = "Puntuación: " + final_response;
        }
    }
}
