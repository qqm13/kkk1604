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

                if (Necrolog == true)
                {
                    TotalPrice += 2000;
                }
                if (LastDiner == true)
                {
                    TotalPrice += GuestCount * 300;
                }
                if (LastVideo == true)
                {
                    TotalPrice += 2000;
                }
                if (GuestBus == true)
                {
                    TotalPrice += GuestCount * 700;
                }
                if (Catafalque == true)
                {
                    TotalPrice += 5000;
                }
                if (Priest == true)
                {
                    TotalPrice += 10000;
                }


                if (SelectedDeathPlace != null)
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
