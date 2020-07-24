using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime
{
    class Recipes
    {
        public string nombre { get; set; }

        public string tipo { get; set; }

        public string porciones { get; set; }

        public string duracion { get; set; }

        public string tiempo { get; set; }

        public string dificultad { get; set; }

        public string dieta { get; set; }

        public string foto { get; set; }

        public string ingredientes { get; set; }

        public string pasos { get; set; }

        public string precio { get; set; }

        public string fecha { get; set; }

        public string valoracion { get; set; }

        public int id { get; set; }

        public override string ToString()
        {
            return nombre;
        }
    }
}
