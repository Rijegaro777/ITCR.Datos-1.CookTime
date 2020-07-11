using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : TabbedPage
    {
        private Usuario usuario = Cliente.get_instance().get_usuario();

        /// <summary>
        /// Ventana principal donde se muestran las diferentes secciones de la aplicación.
        /// </summary>
        public PaginaPrincipal()
        {
            NavigationPage perfil = new NavigationPage(new Perfil(usuario, false));
            perfil.Title = "Perfil";

            NavigationPage newsfeed = new NavigationPage(new Newsfeed());
            newsfeed.Title = "Newsfeed";

            NavigationPage busqueda = new NavigationPage(new Busqueda());
            busqueda.Title = "Buscar";

            NavigationPage notificaciones = new NavigationPage(new Busqueda());
            notificaciones.Title = "Notificaciones";

            NavigationPage crear_receta = new NavigationPage(new Busqueda());
            crear_receta.Title = "+";

            Children.Add(perfil);
            Children.Add(newsfeed);
            Children.Add(busqueda);
            Children.Add(notificaciones);
            Children.Add(crear_receta);
        }
    }
}