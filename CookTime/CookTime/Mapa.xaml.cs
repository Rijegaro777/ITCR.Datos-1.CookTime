using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Mapa(Empresa empresa)
        {
            InitializeComponent();
            double latitud = double.Parse(empresa.get_ubicacion().Split('?')[0]);
            double longitud = double.Parse(empresa.get_ubicacion().Split('?')[1]);

            Position posicion_mapa = new Position(latitud, longitud);
            MapSpan map_span = new MapSpan(posicion_mapa, 0.05, 0.05);
            Map mapa = new Map(map_span);
            mapa.Pins.Add(new Pin
            {
                Label = "ubicacion",
                Position = posicion_mapa
            });
            Content = mapa;
        }
    }
}