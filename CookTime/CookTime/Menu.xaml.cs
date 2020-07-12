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
            else if (lista_menu.SelectedItem.ToString() == "Crear Receta")
            {
                await Navigation.PushModalAsync(new NavigationPage(new CrearReceta()));
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new Configuración()));
            }
        }
    }
}