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
        private List<Usuario> sugerencias_usuarios;
        private List<Empresa> sugerencias_empresas;
        private List<string> nombres_sugerencias;

        public Busqueda()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Busca el texto ingresado en el search bar en el archivo JSON del server cuando se presiona el botón de buscar.
        /// Dependiendo de la opción seleccionada en el picker, buscará usuarios, empresas, recetas o una búsqueda aleatoria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buscar(object sender, EventArgs e)
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

            if(elegir_busqueda.SelectedItem.ToString() == "Personas")
            {
                buscar_personas(busqueda);
            }
            else if (elegir_busqueda.SelectedItem.ToString() == "Empresas")
            {
                buscar_empresas(busqueda);
            }
            else if (elegir_busqueda.SelectedItem.ToString() == "Recetas")
            {
                buscar_recetas(busqueda);
            }
            else
            {
                buscar_aleatorio();
            }
        }

        private void buscar_aleatorio()
        {
            throw new NotImplementedException();
        }

        private void buscar_recetas(string busqueda)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Busca el string ingresado en el JSON que contiene las empresas.
        /// </summary>
        /// <param name="busqueda">El string a buscar</param>
        private async void buscar_empresas(string busqueda)
        {
            string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_empresa?busqueda=" + busqueda);

            string final_response = response.ToString();

            sugerencias_empresas = JsonConvert.DeserializeObject<List<Empresa>>(final_response);
            nombres_sugerencias = new List<string>();

            try
            {
                if (sugerencias_empresas.Count == 0)
                {
                    nombres_sugerencias.Add("No se encontraron usuarios");
                }
                else
                {
                    int i = 0;
                    while (i < sugerencias_empresas.Count)
                    {
                        nombres_sugerencias.Add(sugerencias_empresas[i].get_nombre());
                        i++;
                    }
                }
            }
            catch
            {
                nombres_sugerencias.Add("No se encontraron usuarios");
            }

            lista_busqueda.ItemsSource = nombres_sugerencias;
        }

        /// <summary>
        /// Busca el string ingresado en el JSON que contiene los usuarios.
        /// </summary>
        /// <param name="busqueda">El string a buscar</param>
        private async void buscar_personas(string busqueda)
        {
            string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_usuario?busqueda=" + busqueda);

            string final_response = response.ToString();

            sugerencias_usuarios = JsonConvert.DeserializeObject<List<Usuario>>(final_response);
            nombres_sugerencias = new List<string>();

            try
            {
                if (sugerencias_usuarios.Count == 0)
                {
                    nombres_sugerencias.Add("No se encontraron usuarios");
                }
                else
                {
                    int i = 0;
                    while (i < sugerencias_usuarios.Count)
                    {
                        nombres_sugerencias.Add(sugerencias_usuarios[i].get_nombre() + " " + sugerencias_usuarios[i].get_apellido());
                        i++;
                    }
                }
            }
            catch
            {
                nombres_sugerencias.Add("No se encontraron usuarios");
            }

            lista_busqueda.ItemsSource = nombres_sugerencias;
        }


        /// <summary>
        /// Muestra el resultado de la busqueda seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lista_busqueda_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!nombres_sugerencias.Contains("No se encontraron usuarios"))
            {
                int pos = nombres_sugerencias.IndexOf(lista_busqueda.SelectedItem.ToString());
                if (elegir_busqueda.SelectedItem.ToString() == "Personas")
                {
                    await Navigation.PushModalAsync(new NavigationPage(new Perfil(sugerencias_usuarios[pos], true)));
                }
                else if (elegir_busqueda.SelectedItem.ToString() == "Empresas")
                {
                    await Navigation.PushModalAsync(new NavigationPage(new BoardEmpresa(sugerencias_empresas[pos], Cliente.get_instance().get_usuario())));
                }
                else if (elegir_busqueda.SelectedItem.ToString() == "Recetas")
                {
                    await DisplayAlert("Error", "No implementado", "ok");
                }
            }
        }
    }
}