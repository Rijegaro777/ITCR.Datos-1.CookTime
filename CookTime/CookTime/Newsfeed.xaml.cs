using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Newsfeed : ContentPage
    {
        List<string> lista_nombres = new List<string>();
        List<string> list_users = new List<string>();
        List<Usuario> lista_usuarios = new List<Usuario>();
        List<Empresa> lista_empresas = new List<Empresa>();
        List<Receta> recetas = new List<Receta>();
        int a = 0, u = 0;
        private Usuario usuario_actual;
        public IList listRecipes { get; private set; }

        /// <summary>
        /// Muestra una lista con todos los seguidos del usuario.
        /// </summary>
        /// <param name="usuario">El usuario cuya lista de seguidos se mostrará</param>
        public Newsfeed(Usuario usuario_seguido)
        {
            InitializeComponent();
            usuario_actual = usuario_seguido;
            buscar_recetas_seguidos(usuario_seguido);
        }

        /// <summary>
        /// /// Solicita los datos de los seguidos al server y los muestra.
        /// </summary>
        /// <param name="usuario">El usuario cuyos seguidores se desean consultar</param>
        private async void buscar_recetas_seguidos(Usuario usuario)
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

            listRecipes = new List<Recipes>();


            for (int i = 0; i < lista_usuarios.Count; i++)
            {
                for (int j = 0; j < lista_usuarios[i].get_recetas().Count; j++)
                {
                    string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_receta/" + lista_usuarios[i].get_recetas()[j].ToString());
                    string final_response = response.ToString();

                    Receta receta = JsonConvert.DeserializeObject<Receta>(final_response);

                    recetas.Add(receta);

                    listRecipes.Add(new Recipes
                    {
                        dieta = lista_usuarios[i].get_foto(),
                        tipo = lista_usuarios[i].get_nombre() + lista_usuarios[i].get_apellido(),
                        nombre = recetas[a].get_nombre(),
                        foto = recetas[a].get_foto(),
                        fecha = recetas[a].get_fecha()
                    });
                    lista_nombres.Add(recetas[a].get_nombre());

                    a++;
                }
                u++;
                list_users.Add(lista_usuarios[i].get_nombre());
            }
            BindingContext = this;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            int pos = lista_nombres.IndexOf(lista_recetas.SelectedItem.ToString());
            await Navigation.PushModalAsync(new NavigationPage(new BoardReceta(recetas[pos])));
        }

        private async void compartir(object sender, EventArgs e)
        {
            int pos = lista_nombres.IndexOf(lista_recetas.SelectedItem.ToString());
            usuario_actual.get_recetas().Add(recetas[pos].get_id());

            await DisplayAlert("Exitoso", "Receta compartida", "Ok");
        }
        private async void user(object sender, EventArgs e)
        {
            int pos = list_users.IndexOf(lista_recetas.SelectedItem.ToString());
            await Navigation.PushAsync(new NavigationPage(new Perfil(lista_usuarios[pos], false)));
        }
    }
}