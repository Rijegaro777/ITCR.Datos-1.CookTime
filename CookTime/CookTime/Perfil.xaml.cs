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
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            Usuario usuario = Cliente.get_instance().get_usuario();
            nombre.Text = usuario.get_nombre() + " " + usuario.get_apellido();
        }

        private async void mostrar_seguidos(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosSeguidos());
        }

        private async void mostrar_seguidores(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosSeguidos());
        }
    }
}