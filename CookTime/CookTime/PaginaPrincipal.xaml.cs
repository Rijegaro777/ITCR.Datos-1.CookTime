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
            NavigationPage newsfeed = new NavigationPage(new Newsfeed(usuario));
            newsfeed.Title = "Newsfeed";

            NavigationPage notificaciones = new NavigationPage(new Views.Notification.SocialNotificationPage());
            notificaciones.Title = "Notificaciones";

            NavigationPage busqueda = new NavigationPage(new Busqueda());
            busqueda.Title = "Buscar";

            Children.Add(newsfeed);
            Children.Add(notificaciones);
            Children.Add(busqueda);
        }
    }
}