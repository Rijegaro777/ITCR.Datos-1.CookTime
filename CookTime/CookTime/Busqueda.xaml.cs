using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Busqueda : ContentPage
    {
        private List<Usuario> sugerencias;
        private List<string> nombres_sugerencias;

        public Busqueda()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Busca el texto ingresado en el search bar en el archivo JSON del server cuando se presiona el botón de buscar..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            sugerencias = JsonConvert.DeserializeObject<List<Usuario>>(final_response);
            nombres_sugerencias = new List<string>();

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
                        nombres_sugerencias.Add(sugerencias[i].get_nombre() + " " + sugerencias[i].get_apellido());
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

        /// <summary>
        /// Muestra el perfil del usuario seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void list_personas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!nombres_sugerencias.Contains("No se encontraron usuarios"))
            {
                int pos = nombres_sugerencias.IndexOf(list_personas.SelectedItem.ToString());
                await Navigation.PushModalAsync(new NavigationPage(new Perfil(sugerencias[pos], true)));
            }
        }
    }
}