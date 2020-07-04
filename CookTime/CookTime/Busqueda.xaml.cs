using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Busqueda : ContentPage
    {
        public Busqueda()
        {
            InitializeComponent();
        }

        private async void buscar_personas(object sender, EventArgs e)
        {
            string busqueda;

            try
            {
                busqueda = search_bar_personas.Text;
            }
            catch
            {
                busqueda = search_bar_personas.Text.Split(' ')[0];
            }

            string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_usuario?busqueda=" + busqueda);

            string final_response = response.ToString();

            List<Usuario> sugerencias = JsonConvert.DeserializeObject<List<Usuario>>(final_response);
            List<string> nombres_sugerencias = new List<string>();

            try
            {
                if (sugerencias.Count == 0)
                {
                    nombres_sugerencias.Add("No se encontraron usuarios");
                }
                else
                {
                    int i = 0;
                    while (i < sugerencias.Count)
                    {
                        nombres_sugerencias.Add(sugerencias[i].nombre + " " + sugerencias[i].apellido);
                        i++;
                    }
                }
            }
            catch
            {
                nombres_sugerencias.Add("No se encontraron usuarios");
            }

            list_personas.ItemsSource = nombres_sugerencias;
        }
    }
}