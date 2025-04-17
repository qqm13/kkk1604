﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.Model
{
    public class Organization
    {
        public int id { get; set; } 
        public int DeathPlaceId {  get; set; }
        public int GuestCount { get; set; }
        public bool Necrology { get; set; }
        public bool LastDinner { get; set; }
        public bool LastSlideshow { get; set; }
        public bool GuestBus { get; set; }
        public bool Catafalque { get; set; }
        public bool Priest {  get; set; }
        public int FlowersId { get; set; }
        public DateOnly Date { get; set; }  
        public int PlaceID { get; set; }
        public int Price { get; set; }
    }
}
