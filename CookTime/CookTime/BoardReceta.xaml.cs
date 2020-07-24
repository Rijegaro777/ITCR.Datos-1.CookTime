using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardReceta : ContentPage
    {
        public IList listRecipes { get; private set; }
        Receta receta_actual;
        Label puntuacion;

        /// <summary>
        /// Muestra el board de una empresa.
        /// </summary>
        /// <param name="receta">La receta que se quiere mostrar.</param>
        /// <param name="usuario">El usuario que desea abrir la empresa.</param>
        public BoardReceta(Receta receta)
        {
            InitializeComponent();

            puntuacion = new Label
            {
                Text = "Puntuación: " + receta.get_promedio_calificaciones(),
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Fill
            };

            Grid.SetColumnSpan(puntuacion, 2);
            Grid.SetRow(puntuacion, 1);
            grid_receta.Children.Add(puntuacion);

            List<int> calificaciones = new List<int>();
            calificaciones.Add(1);
            calificaciones.Add(2);
            calificaciones.Add(3);
            calificaciones.Add(4);
            calificaciones.Add(5);

            Picker picker_puntuacion = new Picker();
            picker_puntuacion.Title = "Puntuación";
            picker_puntuacion.ItemsSource = calificaciones;

            Button boton_puntuar = new Button();
            boton_puntuar.Text = "Puntuar";
            boton_puntuar.Clicked += async (sender, args) => await Cliente.get_instance()
            .puntuar_receta(receta, picker_puntuacion.SelectedItem.ToString(), grid_receta, picker_puntuacion, boton_puntuar, puntuacion);

            Grid.SetRow(picker_puntuacion, 2);
            Grid.SetColumn(picker_puntuacion, 0);

            Grid.SetRow(boton_puntuar, 2);
            Grid.SetColumn(boton_puntuar, 1);

            grid_receta.Children.Add(picker_puntuacion);
            grid_receta.Children.Add(boton_puntuar);

            listRecipes = new List<Recipes>();

            listRecipes.Add(new Recipes
            {
                nombre = receta.get_nombre(),
                fecha = receta.get_fecha(),
                precio = receta.get_precio(),
                tipo = receta.get_tipo(),
                dieta = receta.get_dieta(),
                foto = receta.get_foto(),
                tiempo = receta.get_tiempo(),
                duracion = receta.get_duracion(),
                dificultad = receta.get_dificultad(),
                porciones = receta.get_porciones(),
                ingredientes = receta.get_ingredientes(),
                pasos = receta.get_pasos()
            }); 

            BindingContext = this;

            receta_actual = receta;
            foto_receta.Source = receta_actual.get_foto();
        }
    }
}