using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Función que se ejecuta cuando se hace tap sobre el botón para iniciar sesión.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void login(object sender, EventArgs e)
        {
            if (entry_email.Text == null || entry_contrasena.Text == null)
            {
                await DisplayAlert("Aviso", "Llene todos los campos", "Ok");
                return;
            }
            string email = entry_email.Text;
            string contrasena = entry_contrasena.Text;

            string contrasena_encriptada = Cliente.get_instance().hash_MD5(contrasena);

            string response = await Cliente.get_instance().get_client()
                .GetStringAsync("rest/servicios/login?email=" + email + "&contrasena=" + contrasena_encriptada);

            string final_response = response.ToString();

            if (final_response != "")
            {
                Cliente.get_instance().set_usuario(JsonConvert.DeserializeObject<Usuario>(final_response));
                Application.Current.MainPage = new ContenedorPrincipal();
            }
            else
            {
                await DisplayAlert("Error", "Correo electrónico o contraseña erróneos", "Ok");
            }
        }

        /// <summary>
        /// Función que se ejecuta cuando se hace tap sobre el botón para registrarse.
        /// Cambia a la página de registro.
        /// </summary>
        private async void ventana_registro(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }
    }
}