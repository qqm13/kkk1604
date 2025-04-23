using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.Model
{
    public class Grave
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public int FormId { get; set; }
        public int MaterialId { get; set; }
        public int Price { get; set; }

        public Material Material { get; set; }
        public Form Form { get; set; }
    }
}
