using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearEmpresa : ContentPage
    {
        private Usuario usuario = Cliente.get_instance().get_usuario();
        private Empresa empresa_actual;
        private MediaFile imagen_seleccionada;
        private double latitud = 0;
        private double longitud = 0;
        private Pin pin;

        public CrearEmpresa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crea la empresa y solicita al server que se actualicen los JSON.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void boton_crear_empresa_clicked(object sender, EventArgs e)
        {
            if(mapa.Pins.Count != 0)
            {
                string nombre_empresa = entry_nombre.Text;
                string contacto = entry_contacto.Text;
                string horario = entry_horario.Text;
                string ubicacion = pin.Position.Latitude.ToString() + "?" + pin.Position.Longitude.ToString();

                Empresa empresa = new Empresa(nombre_empresa, contacto, horario, ubicacion);
                empresa_actual = empresa;
                usuario.get_empresas().Add(empresa.get_id());

                string empresa_json = JsonConvert.SerializeObject(empresa) + "%" + usuario.get_id();
                var userData = new StringContent(empresa_json, Encoding.UTF8, "text/plain");

                var response = await Cliente.get_instance().get_client().PostAsync("rest/servicios/crear_empresa", userData);
                response.EnsureSuccessStatusCode();

                string result = response.StatusCode.ToString();
                
                var stream_logo = imagen_seleccionada.GetStream();
                ImageUploader.get_instance_logos().subir_imagen(stream_logo, "logos&" + empresa_actual.get_id().ToString());

                await DisplayAlert("Exitoso", "Empresa creada exitosamente", "Ok");
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Faltan datos", "Ingrese la ubicación de su empresa", "Ok");
            }
        }

        /// <summary>
        /// Coloca un pin en la posición clickeada del mapa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colocar_pin(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            mapa.Pins.Clear();
            latitud = e.Position.Latitude;
            longitud = e.Position.Longitude;
            pin = new Pin
            {
                Label = "ubicacion",
                Type = PinType.Place,
                Position = new Position(latitud, longitud)
            };
            mapa.Pins.Add(pin);
        }

        private async void subir_logo(object sender, EventArgs e)
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
            
            imagen_seleccionada = await CrossMedia.Current.PickPhotoAsync(media_options);

            if (imagen_seleccionada == null)
            {
                await DisplayAlert("Error", "Seleccione una imagen", "Ok");
                return;
            }
        }
    }
}