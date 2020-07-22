using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Syncfusion.DataSource.Extensions;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class Perfil : ContentPage
    {
        List<Receta> recetas = new List<Receta>();
        Usuario dueno;
        Label puntuacion;
        List<string> lista_nombres = new List<string>();
        public IList listRecipes { get; private set; }

        /// <summary>
        /// Muestra el perfil de un usuario.
        /// </summary>
        /// <param name="dueno_perfil">El usuario al que le pertenece el perfil.</param>
        /// <param name="es_ajeno">Indica si el perfil le pertenece al usuario que está utilizando la app.</param>
        public Perfil(Usuario dueno_perfil, bool es_ajeno)
        {
            InitializeComponent();

            dueno = dueno_perfil;

            if (dueno_perfil.get_foto() != "vacio.jpg")
            {
                foto_perfil.Source = dueno_perfil.get_foto();
            }
            if (dueno_perfil.is_chef)
            {
                puntuacion = new Label { Text = "Puntuación: " + dueno.get_promedio_calificacion(),
                    FontSize = 18,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Fill
                };

                Grid.SetColumnSpan(puntuacion, 2);
                Grid.SetRow(puntuacion, 3);
                grid_perfil.Children.Add(puntuacion);
            }
            if (es_ajeno && !Cliente.get_instance().get_usuario().get_seguidos().Contains(dueno_perfil.get_id()))
            {
                Button seguir = new Button();
                seguir.Text = "Seguir";
                seguir.Clicked += async (sender, args) => await Cliente.get_instance().seguir_usuario(dueno_perfil, grid_perfil, seguir);
                Grid.SetColumnSpan(seguir, 2);
                Grid.SetRow(seguir, 4);
                grid_perfil.Children.Add(seguir);
            }
            if (es_ajeno && dueno_perfil.is_chef)
            {
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
                .puntuar_usuario(dueno_perfil, picker_puntuacion.SelectedItem.ToString(), grid_perfil, picker_puntuacion, boton_puntuar, puntuacion);

                Grid.SetRow(picker_puntuacion, 5);
                Grid.SetColumn(picker_puntuacion, 0);

                Grid.SetRow(boton_puntuar, 5);
                Grid.SetColumn(boton_puntuar, 1);

                grid_perfil.Children.Add(picker_puntuacion);
                grid_perfil.Children.Add(boton_puntuar);
            }
            nombre.Text = dueno_perfil.get_nombre() + " " + dueno_perfil.get_apellido();

            buscar_recetas(dueno_perfil);
        }

        public async void buscar_recetas(Usuario usuario) {
            for (int i = 0; i < usuario.get_recetas().Count; i++)
            {
                string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_receta/" + usuario.get_recetas()[i].ToString());
                string final_response = response.ToString();

                Receta receta = JsonConvert.DeserializeObject<Receta>(final_response);

                recetas.Add(receta);
            }

            listRecipes = new List<Recipes>();

            for (int i = 0; i < recetas.Count; i++)
            {
                listRecipes.Add(new Recipes
                {
                    nombre = recetas[i].get_nombre(),
                    foto = recetas[i].get_foto(),
                    dificultad = recetas[i].get_dificultad(),
                    fecha = recetas[i].get_fecha()
                });
                lista_nombres.Add(recetas[i].get_nombre());
            }
            BindingContext = this;

        }
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            int pos = lista_nombres.IndexOf(lista_recetas.SelectedItem.ToString());
            await Navigation.PushModalAsync(new NavigationPage(new BoardReceta(recetas[pos])));
        }

        /// <summary>
        /// Muestra la lista de usuarios seguidos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void mostrar_seguidos(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosSeguidos(dueno));
        }

        /// <summary>
        /// Muestra la lista de usuarios seguidores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void mostrar_seguidores(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosSeguidores(dueno));
        }
    }
}