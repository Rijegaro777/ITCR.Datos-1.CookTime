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
    public partial class BoardEmpresa : ContentPage
    {
        public BoardEmpresa(Empresa empresa, Usuario usuario)
        {
            InitializeComponent();
            if (!usuario.get_empresas().Contains(empresa.get_id()))
            {
                Button seguir = new Button();
                seguir.Text = "Seguir";
                //seguir.Clicked += async (sender, args) => await Cliente.get_instance().seguir_usuario(dueno_perfil, grid_perfil, seguir);
                Grid.SetColumnSpan(seguir, 2);
                Grid.SetRow(seguir, 4);

                grid_perfil.Children.Add(seguir);
            }
            else
            {
                Button crear_receta = new Button();
                crear_receta.Text = "Añadir receta";
                //seguir.Clicked += async (sender, args) => await Cliente.get_instance().seguir_usuario(dueno_perfil, grid_perfil, seguir);
                Grid.SetColumnSpan(crear_receta, 2);
                Grid.SetRow(crear_receta, 4);

                grid_perfil.Children.Add(crear_receta);
            }
            nombre.Text = empresa.get_nombre();
            horario.Text = "Horario de atención: " + empresa.get_horario();
            contacto.Text = "Método de contacto: " +empresa.get_contacto();
        }
    }
}