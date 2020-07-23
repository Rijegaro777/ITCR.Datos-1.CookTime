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

        /// <summary>
        /// Muestra el board de una empresa.
        /// </summary>
        /// <param name="receta">La receta que se quiere mostrar.</param>
        /// <param name="usuario">El usuario que desea abrir la empresa.</param>
        public BoardReceta(Receta receta)
        {
            InitializeComponent();

            listRecipes = new List<Recipes>();

            listRecipes.Add(new Recipes
            {
                nombre = receta.get_nombre(),
                fecha = receta.get_fecha(),
                precio = receta.get_precio(),

                tipo = receta.get_tipo(),
                dieta = receta.get_dieta(),
                tiempo = receta.get_tiempo(),
                duracion = receta.get_duracion(),
                dificultad = receta.get_dificultad(),
                porciones = receta.get_porciones(),
                ingredientes = receta.get_ingredientes(),
                pasos = receta.get_pasos()
            });

            BindingContext = this;

            receta_actual = receta;

            if (receta_actual.get_foto() != "Lasagna.jpg")
            {
                foto_receta.Source = receta_actual.get_foto();
            }
        }
    }
}