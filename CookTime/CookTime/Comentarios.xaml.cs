using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Comentarios : ContentPage
    {
        public Comentarios()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Envia el comentario a la lista de comentarios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void comentar(object sender, EventArgs e)
        {
            string comentario = entry_comentarios.Text;
        }
    }
}