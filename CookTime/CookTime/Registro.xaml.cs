using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Función que envía la información del usuario al servidor cuando se hace tap sobre el botón 
        /// de registrarse y haber llenado los datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button_registro_clicked(object sender, EventArgs e)
        {
            string email = entry_email.Text;
            string nombre = entry_nombre.Text;
            string apellido = entry_apellido.Text;
            string contrasena = entry_contrasena.Text;
            int edad = int.Parse(entry_edad.Text);

            Usuario usuario1 = new Usuario(email, nombre, apellido, contrasena, edad);
            string usuario = JsonConvert.SerializeObject(usuario1);
            var userData = new StringContent(usuario, Encoding.UTF8, "text/plain");

            var response = await Cliente.get_instance().get_client().PostAsync("rest/servicios/registro", userData);
            response.EnsureSuccessStatusCode();

            string result = response.StatusCode.ToString();

            await DisplayAlert("Exitoso", "Registro completado exitosamentre", "Ok");
            await Navigation.PopToRootAsync();
        }
    }
}