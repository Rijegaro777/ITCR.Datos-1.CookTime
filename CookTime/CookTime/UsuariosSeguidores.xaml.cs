using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosSeguidores : ContentPage
    {
        public UsuariosSeguidores(Usuario usuario)
        {
            InitializeComponent();
            List<string> nombres_seguidores = buscar_seguidos(usuario).Result;
            lista_seguidores.ItemsSource = nombres_seguidores;
        }

        private async Task<List<string>> buscar_seguidos(Usuario usuario)
        {
            List<string> nombres_seguidores = new List<string>();
            for (int i = 0; i == usuario.get_seguidores().Count; i++)
            {
                string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_usuario_por_id?id=" + usuario.get_seguidores()[i].ToString());
                Usuario seguidor = JsonConvert.DeserializeObject<Usuario>(response.ToString());
                nombres_seguidores.Add(seguidor.get_nombre() + " " + seguidor.get_apellido());
            }
            return nombres_seguidores;
        }
    }
}
