using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.Model
{
    public class Organization
    {
        public int Id { get; set; } 
        public int DeathPlaceId {  get; set; }
        public int GuestCount { get; set; }
        public bool Necrology { get; set; }
        public bool LastDinner { get; set; }
        public bool LastSlideshow { get; set; }
        public bool GuestBus { get; set; }
        public bool Catafalque { get; set; }
        public bool Priest {  get; set; }
        public DateTime Date { get; set; }  
        public int PlaceId { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }

        public Place Place { get; set; }
        public DeathPlace DeathPlace { get; set; }
    }
}
