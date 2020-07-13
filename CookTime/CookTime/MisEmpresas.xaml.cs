using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisEmpresas : ContentPage
    {
        List<Empresa> empresas = new List<Empresa>();
        List<string> lista_nombres = new List<string>();
        Usuario usuario_actual;
        public MisEmpresas(Usuario usuario)
        {
            InitializeComponent();
            usuario_actual = usuario;
            buscar_empresas(usuario);
        }

        /// <summary>
        /// /// Solicita los datos de las empresas al server y las muestra.
        /// </summary>
        /// <param name="usuario">El usuario cuyas empresas se desean consultar</param>
        private async void buscar_empresas(Usuario usuario)
        {
            for (int i = 0; i < usuario.get_empresas().Count; i++)
            {
                string response = await Cliente.get_instance().get_client().GetStringAsync("rest/servicios/buscar_empresa/" + usuario.get_empresas()[i].ToString());
                string final_response = response.ToString();

                Empresa empresa = JsonConvert.DeserializeObject<Empresa>(final_response);

                empresas.Add(empresa);
            }
            for (int i = 0; i < empresas.Count; i++)
            {
                lista_nombres.Add(empresas[i].get_nombre());
            }
            lista_empresas.ItemsSource = lista_nombres;
        }

        /// <summary>
        /// /// Muestra el board de la empresa que fue seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lista_empresas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            int pos = lista_nombres.IndexOf(lista_empresas.SelectedItem.ToString());
            await Navigation.PushModalAsync(new NavigationPage(new BoardEmpresa(empresas[pos], usuario_actual)));
        }
    }
}