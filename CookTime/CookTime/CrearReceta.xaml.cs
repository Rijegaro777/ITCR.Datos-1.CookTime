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
            string tipo = entry_tipo.Text;
            string porciones = entry_porciones.Text;
            string duracion = entry_duracion.Text;
            string tiempo = entry_tiempo.Text;
            string dificultad = entry_dificultad.Text;
            string dieta = entry_dieta.Text;
            string foto = entry_foto.Text + ".jpg";
            string ingredientes = entry_ingredientes.Text;
            string pasos = entry_pasos.Text;
            string precio = entry_precio.Text;

            Receta receta = new Receta(nombre_receta, tipo, porciones, duracion, tiempo, dificultad, dieta, foto, ingredientes, pasos, precio);
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