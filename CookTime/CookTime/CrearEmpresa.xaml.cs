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
    public partial class CrearEmpresa : ContentPage
    {
        Usuario usuario = Cliente.get_instance().get_usuario();

        public CrearEmpresa()
        {
            InitializeComponent();
        }

        private async void boton_crear_empresa_Clicked(object sender, EventArgs e)
        {
            string nombre_empresa = entry_nombre.Text;
            string contacto = entry_contacto.Text;
            string horario = entry_horario.Text;

            Empresa empresa = new Empresa(nombre_empresa, contacto, horario);
            string empresa_json = JsonConvert.SerializeObject(empresa) + "%" + usuario.get_id();
            var userData = new StringContent(empresa_json, Encoding.UTF8, "text/plain");

            var response = await Cliente.get_instance().get_client().PostAsync("rest/servicios/crear_empresa", userData);
            response.EnsureSuccessStatusCode();

            string result = response.StatusCode.ToString();

            usuario.get_empresas().Add(empresa.get_id());

            await DisplayAlert("Exitoso", "Empresa creada exitosamente", "Ok");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}