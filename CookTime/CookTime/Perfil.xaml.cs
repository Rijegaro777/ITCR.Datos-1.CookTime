using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class Perfil : ContentPage
    {
        private Usuario dueno;
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

            if (es_ajeno && !Cliente.get_instance().get_usuario().get_seguidos().Contains(dueno_perfil.get_id()))
            {
                Button seguir = new Button();
                seguir.Text = "Seguir";
                seguir.Clicked += async (sender, args) => await Cliente.get_instance().seguir_usuario(dueno_perfil, grid_perfil, seguir);
                Grid.SetColumnSpan(seguir, 2);
                Grid.SetRow(seguir, 2);

                grid_perfil.Children.Add(seguir);
            }
            nombre.Text = dueno_perfil.get_nombre() + " " + dueno_perfil.get_apellido();

            listRecipes = new List<Recipes>();

            listRecipes.Add(new Recipes
            {
                Name = "Lasagna",
                Date = "06 de Julio",
                Difficulty = "Dificultad: " + "1",
                Url = "Lasagna.jpg"
            });

            listRecipes.Add(new Recipes
            {
                Name = "Pasta",
                Date = "07 de Julio",
                Difficulty = "Dificultad: " + "2",
                Url = "Pasta.jpg"
            });

            listRecipes.Add(new Recipes
            {
                Name = "Ensalada",
                Date = "06 de Julio",
                Difficulty = "Dificultad: " + "3",
                Url = "Salad.jpg"
            });

            listRecipes.Add(new Recipes
            {
                Name = "Huevos con Tocino",
                Date = "07 de Julio",
                Difficulty = "Dificultad: " + "4",
                Url = "Huevo_y_tosino.jpg"
            });

            listRecipes.Add(new Recipes
            {
                Name = "Pasta",
                Date = "06 de Julio",
                Difficulty = "Dificultad: " + "5",
                Url = "Pasta.jpg"
            });

            BindingContext = this;

        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Recipes selectedItem = e.SelectedItem as Recipes;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Recipes tappedItem = e.Item as Recipes;
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