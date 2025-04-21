using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.Model
{
    public class DeathPlace
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int GraveTypeId { get; set; }
        public int CoffinTypeId { get; set; }
        public int FlowersId { get; set; }
        public int Price { get; set; }


        public Flower Flower { get; set; }
        public Grave Grave { get; set; }
        public Coffin Coffin { get; set; }
    }
}
