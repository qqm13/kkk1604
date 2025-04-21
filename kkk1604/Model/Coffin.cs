using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.Model
{
    public class Coffin
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int MaterialId { get; set; }
        public int SizeId { get; set; }
        public int Price { get; set; }

        public Material Material { get; set; }
        public Size Size { get; set; }
    }
}
