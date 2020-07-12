using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosSeguidos : ContentPage
    {
        List<string> lista_nombres = new List<string>();
        List<Usuario> lista_usuarios = new List<Usuario>();
        public UsuariosSeguidos(Usuario usuario)
        {
            InitializeComponent();
            Usuario usuario_actual = usuario;
            buscar_seguidos(usuario);
        }

        /// <summary>
        /// /// Solicita los datos de los seguidos al server y los muestra.
        /// </summary>
        /// <param name="usuario">El usuario cuyos seguidores se desean consultar</param>
        private async void buscar_seguidos(Usuario usuario)
        {
            for (int i = 0; i < usuario.get_seguidos().Count; i++)
            {
                string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_usuario/" + usuario.get_seguidos()[i].ToString());
                string final_response = response.ToString();

                Usuario usuario_seguido = JsonConvert.DeserializeObject<Usuario>(final_response);

                lista_usuarios.Add(usuario_seguido);
            }
            for (int i = 0; i < lista_usuarios.Count; i++)
            {
                lista_nombres.Add(lista_usuarios[i].get_nombre() + " " + lista_usuarios[i].get_apellido());
            }
            lista_seguidos.ItemsSource = lista_nombres;
        }


        /// <summary>
        /// /// Muestra el perfil del seguidos que fue seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lista_seguidos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!lista_nombres.Contains("No se encontraron usuarios"))
            {
                int pos = lista_nombres.IndexOf(lista_seguidos.SelectedItem.ToString());
                
                if(lista_usuarios[pos] != Cliente.get_instance().get_usuario())
                {
                    await Navigation.PushModalAsync(new NavigationPage(new Perfil(lista_usuarios[pos], true)));
                }
                else
                {
                    await Navigation.PushModalAsync(new NavigationPage(new Perfil(lista_usuarios[pos], false)));
                }
            }
        }
    }
}
