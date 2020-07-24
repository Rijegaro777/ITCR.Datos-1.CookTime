using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardEmpresa : ContentPage
    {
        List<Receta> recetas = new List<Receta>();
        public IList listRecipes { get; private set; }
        List<string> lista_nombres = new List<string>();
        Empresa empresa_actual;
        Label puntuacion;

        /// <summary>
        /// Muestra el board de una empresa.
        /// </summary>
        /// <param name="empresa">La empresa que se quiere mostrar.</param>
        /// <param name="usuario">El usuarion que desea abrir la empresa.</param>
        public BoardEmpresa(Empresa empresa, Usuario usuario)
        {
            InitializeComponent();

            puntuacion = new Label{ Text = "Puntuación: " + empresa.get_promedio_calificacion(),
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Fill
            };

            Grid.SetColumnSpan(puntuacion, 2);
            Grid.SetRow(puntuacion, 4);
            grid_empresa.Children.Add(puntuacion);

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
            .puntuar_empresa(empresa_actual, picker_puntuacion.SelectedItem.ToString(), grid_empresa, picker_puntuacion, boton_puntuar, puntuacion);

            Grid.SetRow(picker_puntuacion, 5);
            Grid.SetColumn(picker_puntuacion, 0);

            Grid.SetRow(boton_puntuar, 5);
            Grid.SetColumn(boton_puntuar, 1);

            grid_empresa.Children.Add(picker_puntuacion);
            grid_empresa.Children.Add(boton_puntuar);

            empresa_actual = empresa;

            if(empresa_actual.get_logo() != "vacio.jpg")
            {
                logo_empresa.Source = empresa_actual.get_logo();
            }
            if (!usuario.get_empresas().Contains(empresa.get_id()))
            {
                Button seguir = new Button();
                seguir.Text = "Seguir";
                seguir.Clicked += async (sender, args) => await Cliente.get_instance().seguir_empresa(empresa, grid_empresa, seguir);
                Grid.SetColumnSpan(seguir, 2);
                Grid.SetRow(seguir, 6);

                grid_empresa.Children.Add(seguir);
            }
            else
            {
                Button crear_receta = new Button();
                crear_receta.Text = "Añadir receta";
                crear_receta.Clicked += async (sender, args) => await Navigation.PushModalAsync(new CrearReceta(empresa_actual));
                Grid.SetColumnSpan(crear_receta, 2);
                Grid.SetRow(crear_receta, 7);

                grid_empresa.Children.Add(crear_receta);
            }
            nombre.Text = empresa.get_nombre();
            horario.Text = "Horario de atención: " + empresa.get_horario();
            contacto.Text = "Método de contacto: " +empresa.get_contacto();

            buscar_recetas(empresa_actual);
        }

        public async void buscar_recetas(Empresa empresa)
        {
            for (int i = 0; i < empresa.get_recetas().Count; i++)
            {
                string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_receta/" + empresa.get_recetas()[i].ToString());
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
                    dificultad = "Dificultad: " + recetas[i].get_dificultad(),
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
        /// Muestra la ubicación de la empresa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ver_ubicacion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Mapa(empresa_actual));
        }
    }
}