using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime
{
    class Recipes
    {
        public string nombre { get; set; }

        public string logo { get; set; }

        public string contacto { get; set; }

        public string horario { get; set; }

        public string ubicacion { get; set; }

        public int id { get; set; }

        public override string ToString()
        {
            return nombre;
        }
    }
}
