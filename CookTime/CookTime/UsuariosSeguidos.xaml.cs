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
    public partial class UsuariosSeguidos : ContentPage
    {
        public UsuariosSeguidos(Usuario usuario)
        {
            InitializeComponent();
            //List<string> nombres_seguidos = buscar_seguidos(usuario).Result;
            lista_seguidos.ItemsSource = usuario.get_seguidos();
        }

        private async Task<List<string>> buscar_seguidos(Usuario usuario)
        {
            if (usuario.get_seguidos().Count != 0)
            {
                List<string> nombres_seguidos = new List<string>();
                for (int i = 0; i < usuario.get_seguidos().Count; i++)
                {
                    var response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_usuario_por_id?id=" + usuario.get_seguidos()[i].ToString());
                    string final_response = response.ToString();
                    Usuario seguido = JsonConvert.DeserializeObject<Usuario>(final_response);
                    nombres_seguidos.Add(seguido.get_nombre() + " " + seguido.get_apellido());
                }
                return nombres_seguidos;
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
