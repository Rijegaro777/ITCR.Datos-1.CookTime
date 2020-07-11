using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime
{
    class Recipes
    {
        public string Name { get; set; }

        public string Date { get; set; }

        public string Difficulty { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
