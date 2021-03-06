﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuración : ContentPage
    {
        public Configuración()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Muestra la configuración seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void item_seleccionado(object sender, ItemTappedEventArgs e)
        {
            if (lista_configuracion.SelectedItem.ToString() == "Cambiar foto de perfil")
            {
                await Navigation.PushAsync(new NavigationPage(new CambioFotoPerfil()));
            }
            else
            {
                await Navigation.PushAsync(new NavigationPage(new CambioContrasena()));
            }
        }
    }
}