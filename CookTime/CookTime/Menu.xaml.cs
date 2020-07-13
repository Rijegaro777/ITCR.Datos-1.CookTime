using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        private Usuario usuario = Cliente.get_instance().get_usuario();
        public Menu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Muestra la ventana del menu que se seleccione.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lista_menu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(lista_menu.SelectedItem.ToString() == "Perfil")
            {
                await Navigation.PushModalAsync(new NavigationPage(new Perfil(usuario, false)));
            }
            else if (lista_menu.SelectedItem.ToString() == "Mis empresas")
            {
                await Navigation.PushModalAsync(new NavigationPage(new MisEmpresas(usuario)));
            }
            else if (lista_menu.SelectedItem.ToString() == "Crear receta")
            {
                await Navigation.PushModalAsync(new NavigationPage(new CrearReceta()));
            }
            else if (lista_menu.SelectedItem.ToString() == "Crear empresa")
            {
                await Navigation.PushModalAsync(new NavigationPage(new CrearEmpresa()));
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new Configuración()));
            }
        }
    }
}