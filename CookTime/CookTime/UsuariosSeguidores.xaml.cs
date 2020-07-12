using Newtonsoft.Json;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosSeguidores : ContentPage
    {
        List<string> lista_nombres = new List<string>();
        List<Usuario> lista_usuarios = new List<Usuario>();
        public UsuariosSeguidores(Usuario usuario)
        {
            InitializeComponent();
            buscar_seguidores(usuario);
        }

        /// <summary>
        /// Solicita los datos de los seguidores al server y los muestra.
        /// </summary>
        /// <param name="usuario">El usuario cuyos seguidores se desean consultar</param>
        private async void buscar_seguidores(Usuario usuario)
        {
            for (int i = 0; i < usuario.get_seguidores().Count; i++)
            {
                string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_usuario/" + usuario.get_seguidores()[i].ToString());
                string final_response = response.ToString();

                Usuario usuario_seguidor = JsonConvert.DeserializeObject<Usuario>(final_response);

                lista_usuarios.Add(usuario_seguidor);
            }
            for (int i = 0; i < lista_usuarios.Count; i++)
            {
                lista_nombres.Add(lista_usuarios[i].get_nombre() + " " + lista_usuarios[i].get_apellido());
            }
            lista_seguidores.ItemsSource = lista_nombres;
        }

        /// <summary>
        /// Muestra el perfil del seguidor que fue seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lista_seguidores_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!lista_nombres.Contains("No se encontraron usuarios"))
            {
                int pos = lista_nombres.IndexOf(lista_seguidores.SelectedItem.ToString());

                if (lista_usuarios[pos] != Cliente.get_instance().get_usuario())
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
