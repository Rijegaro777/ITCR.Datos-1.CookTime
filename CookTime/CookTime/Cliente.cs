using System;
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
            this.uri = "http://192.168.1.10:8080/";

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
        /// Cambia la foto de perfil del usuario que está usando la aplicación.
        /// </summary>
        /// <param name="uri">La Uri donde está alojada la foto nueva.</param>
        public async void cambiar_foto_usuario(Uri uri)
        {
            usuario_actual.foto = uri.ToString();
            string mensaje = uri.ToString() + "&" + usuario_actual.get_id().ToString();
            var mensaje_http = new StringContent(mensaje, Encoding.UTF8, "text/plain");

            var response = await client.PostAsync("rest/servicios/cambiar_foto", mensaje_http);
            response.EnsureSuccessStatusCode();

            string final_respose = response.StatusCode.ToString();
        }

        /// <summary>
        /// Agrega al usuario recibido a la lista de seguidos del usuario que está usando la aplicación y
        /// envía la solicitud al servidor para actualizar el archivo JSON. Elimina la opción de seguir usuarios
        /// que ya están en la lista de seguidos.
        /// </summary>
        /// <param name="usuario">El usuario que será seguido.</param>
        /// <param name="grid">El grid del perfil del usuario a seguir.</param>
        /// <param name="button">El botón para seguir al usuario.</param>
        /// <returns></returns>
        public async Task seguir_usuario(Usuario usuario, Grid grid, Button button)
        {
            grid.Children.Remove(button);
            usuario_actual.get_seguidos().Add(usuario.get_id());
            usuario.get_seguidores().Add(usuario_actual.get_id());

            var response = await client
                .GetStringAsync("/rest/servicios/seguir_usuario?usuario=" + usuario_actual.get_id().ToString() + "&usuario_seguido=" + usuario.get_id().ToString());
        }

        /// <summary>
        /// Puntúa al usuario recibido y elimina las opciones para puntuarlo de su perfil. Envía la solicitud al servidor para actualizar el archivo JSON
        /// </summary>
        /// <param name="usuario">El usuario que va a recibir la calificación.</param>
        /// <param name="puntuacion">La puntuación recibida por el usuario.</param>
        /// <param name="grid">El grid del perfil del usuario que será puntuado.</param>
        /// <param name="picker">El objeto que permite seleccionar la calificación.</param>
        /// <param name="boton">El botón para puntuat al usuario.</param>
        /// <param name="label">El label que muestra el promedio de puntuaciones del usuario.</param>
        /// <returns></returns>
        public async Task puntuar_usuario(Usuario usuario, string puntuacion, Grid grid, Picker picker, Button boton, Label label)
        {
            grid.Children.Remove(picker);
            grid.Children.Remove(boton);

            string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/puntuar_usuario/" + usuario.get_id().ToString() + "%" + puntuacion);
            string final_response = response.ToString();

            usuario.promedio_calificacion = float.Parse(final_response);

            label.Text = "Puntuación: " + final_response;
        }

        /// <summary>
        /// Cambia el logo de una empresa.
        /// </summary>
        /// <param name="uri">La Uri donde está alojado el nuevo logo.</param>
        /// <param name="empresa_id">El id de la empresa cuyo logo cambiará.</param>
        public async void cambiar_logo(Uri uri, string empresa_id)
        {
            usuario_actual.foto = uri.ToString();
            string mensaje = uri.ToString() + "&" + empresa_id;
            var mensaje_http = new StringContent(mensaje, Encoding.UTF8, "text/plain");

            var response = await client.PostAsync("rest/servicios/cambiar_logo", mensaje_http);
            response.EnsureSuccessStatusCode();

            string final_respose = response.StatusCode.ToString();
        }

        /// <summary>
        /// Puntúa a la empresa recibida y elimina las opciones para puntuarlo de su board. Envía la solicitud al servidor para actualizar el archivo JSON
        /// </summary>
        /// <param name="empresa">La empresa que va a recibir la calificación.</param>
        /// <param name="puntuacion">La puntuación recibida por la empresa.</param>
        /// <param name="grid">El grid del board de la empresa que será puntuada.</param>
        /// <param name="picker">El objeto que permite seleccionar la calificación.</param>
        /// <param name="boton">El botón para puntuar a la empresa.</param>
        /// <param name="label">El label que muestra el promedio de puntuaciones de la empresa.</param>
        /// <returns></returns>
        public async Task puntuar_empresa(Empresa empresa, string puntuacion, Grid grid, Picker picker, Button boton, Label label)
        {
            grid.Children.Remove(picker);
            grid.Children.Remove(boton);

            string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/puntuar_empresa/" + empresa.get_id().ToString() + "%" + puntuacion);
            string final_response = response.ToString();

            empresa.promedio_calificacion = float.Parse(final_response);

            label.Text = "Puntuación: " + final_response;
        }

        /// <summary>
        /// Agrega a la empresa recibida a la lista de seguidos del usuario que está usando la aplicación y
        /// envía la solicitud al servidor para actualizar el archivo JSON. Elimina la opción de seguir empresas
        /// que ya están en la lista de seguidos.
        /// </summary>
        /// <param name="empresa">La empresa que será seguida.</param>
        /// <param name="grid">El grid de la empresa a seguir.</param>
        /// <param name="button">El botón para seguir a la empresa.</param>
        /// <returns></returns>
        public async Task seguir_empresa(Empresa empresa, Grid grid, Button button)
        {
            grid.Children.Remove(button);
            usuario_actual.get_seguidos().Add(empresa.get_id() * -1);

            var response = await client
                .GetStringAsync("/rest/servicios/seguir_empresa?usuario=" + usuario_actual.get_id().ToString() + "&empresa_seguida=" + (empresa.get_id() * -1).ToString());
        }

        /// <summary>
        /// Cambia la foto de una receta.
        /// </summary>
        /// <param name="uri">La Uri donde está alojada la foto nueva.</param>
        public async void cambiar_foto_receta(Uri uri, String id_receta)
        {
            string mensaje = uri.ToString() + "&" + id_receta;
            var mensaje_http = new StringContent(mensaje, Encoding.UTF8, "text/plain");

            var response = await client.PostAsync("rest/servicios/cambiar_foto_receta", mensaje_http);
            response.EnsureSuccessStatusCode();

            string final_respose = response.StatusCode.ToString();
        }

        /// <summary>
        /// Puntúa a la empresa recibida y elimina las opciones para puntuarlo de su board. Envía la solicitud al servidor para actualizar el archivo JSON
        /// </summary>
        /// <param name="receta">La empresa que va a recibir la calificación.</param>
        /// <param name="puntuacion">La puntuación recibida por la empresa.</param>
        /// <param name="grid">El grid del board de la empresa que será puntuada.</param>
        /// <param name="picker">El objeto que permite seleccionar la calificación.</param>
        /// <param name="boton">El botón para puntuar a la empresa.</param>
        /// <param name="label">El label que muestra el promedio de puntuaciones de la empresa.</param>
        /// <returns></returns>
        public async Task puntuar_receta(Receta receta, string puntuacion, Grid grid, Picker picker, Button boton, Label label)
        {
            grid.Children.Remove(picker);
            grid.Children.Remove(boton);

            string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/puntuar_receta/" + receta.get_id().ToString() + "%" + puntuacion);
            string final_response = response.ToString();

            receta.promedio_calificaciones = float.Parse(final_response);

            label.Text = "Puntuación: " + final_response;
        }
    }
}
