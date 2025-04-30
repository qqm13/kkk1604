using kkk1604.Model;
using kkk1604.Model.Db;
using kkk1604.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class PriceTestVM : BaseVM
    {
        private int totalPrice;

        public ObservableCollection<Grave> Graves { get; set; } = new ObservableCollection<Grave>();
        public ObservableCollection<Coffin> Coffins { get; set; } = new ObservableCollection<Coffin>();
        public ObservableCollection<Flower> Flowers { get; set; } = new ObservableCollection<Flower>();

        public ObservableCollection<DeathPlace> DeathPlaces { get; set; } = new ObservableCollection<DeathPlace>();


        public Grave SelectedGrave { get; set; }
        public Coffin SelectedCoffin { get; set; }
        public Flower SelectedFlower { get; set; }

        public DeathPlace SelectedDeathPlace { get; set; }

        public int TotalPrice { get => totalPrice; set { totalPrice = value; Signal(); } }

        public int GuestCount { get; set; }
        public bool Necrolog {  get; set; } = false;
        public bool LastDiner {  get; set; } = false;
        public bool LastVideo { get; set; } = false;
        public bool GuestBus { get; set; } = false;
        public bool Catafalque { get; set; } = false;
        public bool Priest { get; set; } = false;

        public CommandVM Calculate { get; set; }


        public PriceTestVM()
        {
            SelectAll();

            Calculate = new CommandVM(() =>
            {
                TotalPrice = 0;
                TotalPrice += GuestCount * 150;

                bool[] list = new bool[6];
                list[0] = Necrolog;
                list[1] = LastDiner;
                list[2] = LastVideo;
                list[3] = GuestBus;
                list[4] = Catafalque;
                list[5] = Priest;

                foreach (bool item in list)
                {
                    if(item == true)
                    {
                        TotalPrice += 500;
                    }
                }

                if(SelectedDeathPlace != null)
                {
                    TotalPrice += SelectedDeathPlace.Price;
                }
                else
                {
                    TotalPrice += SelectedCoffin.Price + SelectedGrave.Price + SelectedFlower.Price;
                }

                SelectAll();
            }, () => true);
        }


        private void SelectAll()
        {
            DeathPlaces = new ObservableCollection<DeathPlace>(DeathPlacesDB.GetDb().SelectAll());
            Coffins = new ObservableCollection<Coffin>(CoffinsDB.GetDb().SelectAll());
            Graves = new ObservableCollection<Grave>(GravesDB.GetDb().SelectAll());
            Flowers = new ObservableCollection<Flower>(FlowersDB.GetDb().SelectAll());
        }
    }
}
