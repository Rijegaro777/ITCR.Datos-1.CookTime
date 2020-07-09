using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        private Usuario dueno;
        public Perfil(Usuario dueno_perfil, bool es_ajeno)
        {
            InitializeComponent();

            dueno = dueno_perfil;

            if (es_ajeno && !Cliente.get_instance().get_usuario().get_seguidos().Contains(dueno_perfil.get_id()))
            {
                Button seguir = new Button();
                seguir.Text = "Seguir";
                seguir.Clicked += async (sender, args) => await Cliente.get_instance().seguir_usuario(dueno_perfil, grid_perfil, seguir);
                Grid.SetColumnSpan(seguir, 2);
                Grid.SetRow(seguir, 2);

                grid_perfil.Children.Add(seguir);
            }
            nombre.Text = dueno_perfil.get_nombre() + " " + dueno_perfil.get_apellido();
        }

        private async void mostrar_seguidos(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosSeguidos(dueno));
        }

        private async void mostrar_seguidores(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosSeguidores(dueno));
        }
    }
}