using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosSeguidos : ContentPage
    {
        Usuario usuario_actual;
        List<string> lista_nombres = new List<string>();
        List<Usuario> lista_usuarios = new List<Usuario>();
        List<Empresa> lista_empresas = new List<Empresa>();

        /// <summary>
        /// Muestra una lista con todos los seguidos del usuario.
        /// </summary>
        /// <param name="usuario">El usuario cuya lista de seguidos se mostrará</param>
        public UsuariosSeguidos(Usuario usuario)
        {
            InitializeComponent();
            usuario_actual = usuario;
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
                Debug.WriteLine(usuario.get_seguidos()[i]);
                if (usuario.get_seguidos()[i] < 0)
                {
                    string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_empresa/" + (usuario.get_seguidos()[i] * -1).ToString());
                    string final_response = response.ToString();

                    Empresa empresa_seguida = JsonConvert.DeserializeObject<Empresa>(final_response);

                    lista_empresas.Add(empresa_seguida);
                }
                else
                {
                    string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_usuario/" + usuario.get_seguidos()[i].ToString());
                    string final_response = response.ToString();

                    Usuario usuario_seguido = JsonConvert.DeserializeObject<Usuario>(final_response);

                    lista_usuarios.Add(usuario_seguido);
                }
            }
            for (int i = 0; i < lista_empresas.Count; i++)
            {
                lista_nombres.Add(lista_empresas[i].get_nombre());
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
        private async void seguido_seleccionado(object sender, ItemTappedEventArgs e)
        {
            if (!lista_nombres.Contains("No se encontraron usuarios"))
            {
                int pos = lista_nombres.IndexOf(lista_seguidos.SelectedItem.ToString());

                if(lista_empresas.Count > 0)
                {
                    if (pos < lista_empresas.Count)
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new BoardEmpresa(lista_empresas[pos], usuario_actual)));
                    }
                    else if (lista_usuarios[pos - lista_empresas.Count] != Cliente.get_instance().get_usuario())
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new Perfil(lista_usuarios[pos - lista_empresas.Count], true)));
                    }
                    else
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new Perfil(lista_usuarios[pos - lista_empresas.Count], false)));
                    }
                }
                else
                {
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
}
