using kkk1604.Model.Db;
using kkk1604.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class AddDeathPlaceVM : BaseVM
    {
        private ObservableCollection<DeathPlace> deathPlaces = new ObservableCollection<DeathPlace>();
        private DeathPlace deathPlaceHere = new DeathPlace();

        private ObservableCollection<Grave> graves = new ObservableCollection<Grave>();
        public ObservableCollection<Grave> Graves
        {
            get => graves;
            set
            {
                graves = value;
                Signal();
            }
        }
        private ObservableCollection<Coffin> coffins = new ObservableCollection<Coffin>();
        private ObservableCollection<Flower> flowers = new ObservableCollection<Flower>();
        private Grave selectedGrave;
        private Coffin selectedCoffin;
        private Flower selectedFlower;
        private int priceHere;
        private string deathPlaceHereTitle;

        public ObservableCollection<Coffin> Coffins
        {
            get => coffins;
            set
            {
                coffins = value;
                Signal();
            }
        }

        public ObservableCollection<DeathPlace> DeathPlaces
        {
            get => deathPlaces;
            set
            {
                deathPlaces = value;
                Signal();
            }
        }

        public ObservableCollection<Flower> Flowers
        {
            get => flowers;
            set
            {
                flowers = value;
                Signal();
            }
        }
        public DeathPlace DeathPlaceHere
        {
            get => deathPlaceHere;
            set
            {
                deathPlaceHere = value;

                Signal();
            }
        }
        public int PriceHere
        {
            get => priceHere;
            set
            {
                priceHere = value;
                Signal();
            }
        }

        public string DeathPlaceHereTitle { get => deathPlaceHereTitle; set { deathPlaceHereTitle = value; Signal(); } }

        public Grave SelectedGrave
        {
            get => selectedGrave;
            set
            {
                selectedGrave = value;
                if (SelectedGrave != null && SelectedCoffin != null && SelectedFlower != null)
                {
                    PriceHere = SelectedGrave.Price + SelectedFlower.Price + SelectedCoffin.Price;
                }
                Signal();
            }
        }

        public Coffin SelectedCoffin
        {
            get => selectedCoffin;
            set
            {
                selectedCoffin = value;
                if (SelectedGrave != null && SelectedCoffin != null && SelectedFlower != null)
                {
                    PriceHere = SelectedGrave.Price + SelectedFlower.Price + SelectedCoffin.Price;
                }
                Signal();
            }
        }

        public Flower SelectedFlower
        {
            get => selectedFlower;
            set
            {
                selectedFlower = value;
                if (SelectedGrave != null && SelectedCoffin != null && SelectedFlower != null)
                {
                    PriceHere = SelectedGrave.Price + SelectedFlower.Price + SelectedCoffin.Price;
                }
                Signal();
            }
        }


        public CommandVM UpdateDeathPlace { get; set; }
        public CommandVM RemoveDeathPlace { get; set; }
        public CommandVM AddDeathPlace { get; set; }



        public AddDeathPlaceVM()
        {
            SelectAll();

            UpdateDeathPlace = new CommandVM(() =>
            {
                DeathPlaceHere.Coffin = SelectedCoffin;
                DeathPlaceHere.Grave = SelectedGrave;
                DeathPlaceHere.Flower = SelectedFlower;
                DeathPlaceHere.Title = DeathPlaceHere.Coffin.Title + " " + DeathPlaceHere.Grave.Title +  " " + DeathPlaceHere.Flower.Title;
                DeathPlaceHere.Price = PriceHere;

                DeathPlacesDB.GetDb().Update(DeathPlaceHere);
                SelectAll();
            }, () => DeathPlaceHere != null && SelectedCoffin != null && SelectedGrave != null && SelectedFlower != null); 

            RemoveDeathPlace = new CommandVM(() =>
            {
                DeathPlacesDB.GetDb().Remove(DeathPlaceHere);
                SelectAll();
            }, () => DeathPlaceHere != null);

            AddDeathPlace = new CommandVM(() =>
            {
                DeathPlace deathPlaceAdd = new DeathPlace();
                deathPlaceAdd.Coffin = SelectedCoffin;
                deathPlaceAdd.Grave = SelectedGrave;
                deathPlaceAdd.Flower = SelectedFlower;
                deathPlaceAdd.Price = PriceHere;
                deathPlaceAdd.Title = deathPlaceAdd.Coffin.Title + " " + deathPlaceAdd.Grave.Title + " " + deathPlaceAdd.Flower.Title;

                DeathPlacesDB.GetDb().Insert(deathPlaceAdd);
                SelectAll();
            }, () => SelectedCoffin != null && SelectedGrave != null && SelectedFlower != null);


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
