using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearReceta : ContentPage
    {
        Usuario usuario = Cliente.get_instance().get_usuario();

        public CrearReceta()
        {
            InitializeComponent();
        }

        private async void boton_crear_receta_Clicked(object sender, EventArgs e)
        {
            string nombre_receta = entry_nombre.Text;
            string contacto = entry_contacto.Text;
            string horario = entry_horario.Text;

            Receta receta = new Receta(nombre_receta, contacto, horario);
            usuario.get_recetas().Add(receta.get_id());

            string receta_json = JsonConvert.SerializeObject(receta) + "%" + usuario.get_id();
            var userData = new StringContent(receta_json, Encoding.UTF8, "text/plain");

            var response = await Cliente.get_instance().get_client().PostAsync("rest/servicios/crear_receta", userData);
            response.EnsureSuccessStatusCode();

            string result = response.StatusCode.ToString();

            await DisplayAlert("Exitoso", "Receta creada exitosamente", "Ok");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}