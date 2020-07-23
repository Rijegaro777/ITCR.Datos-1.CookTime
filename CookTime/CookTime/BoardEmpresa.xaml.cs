using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardEmpresa : ContentPage
    {
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
                //seguir.Clicked += async (sender, args) => await Cliente.get_instance().seguir_usuario(dueno_perfil, grid_perfil, seguir);
                Grid.SetColumnSpan(crear_receta, 2);
                Grid.SetRow(crear_receta, 7);

                grid_empresa.Children.Add(crear_receta);
            }
            nombre.Text = empresa.get_nombre();
            horario.Text = "Horario de atención: " + empresa.get_horario();
            contacto.Text = "Método de contacto: " +empresa.get_contacto();
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