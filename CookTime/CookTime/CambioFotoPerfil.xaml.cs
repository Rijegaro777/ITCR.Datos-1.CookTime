using Azure.Storage.Blobs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CambioFotoPerfil : ContentPage
    {
        public CambioFotoPerfil()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Muestra una ventana para seleccionar una foto de la galería y la pone como foto de perfil.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void boton_seleccionar_foto_clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "Archivo no soportado", "Ok");
                return;
            }
            var media_options = new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            };
            var imagen_seleccionada = await CrossMedia.Current.PickPhotoAsync(media_options);

            if(imagen_seleccionada == null)
            {
                await DisplayAlert("Error", "Seleccione una imagen", "Ok");
                return;
            }
            var stream_foto = imagen_seleccionada.GetStream();
            foto_perfil.Source = ImageSource.FromStream(() => stream_foto);
            stream_foto = imagen_seleccionada.GetStream();
            ImageUploader.get_instance_photos().subir_imagen(stream_foto, "photos");
            await DisplayAlert("Éxito", "Foto cambiada exitósamente", "Ok");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}