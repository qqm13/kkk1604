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
    class EditEventVM : BaseVM
    {
        public Organization OrganizationUpdate { get; set; } = new Organization();
        public CommandVM UpdateEvent { get; set; }

        private int totalPrice;
        private Place selectedPlace;
        private int sector;
        private int plot;
        private string allPrice;

        public ObservableCollection<Grave> Graves { get; set; } = new ObservableCollection<Grave>();
        public ObservableCollection<Coffin> Coffins { get; set; } = new ObservableCollection<Coffin>();
        public ObservableCollection<Flower> Flowers { get; set; } = new ObservableCollection<Flower>();
        public ObservableCollection<Place> Places { get; set; } = new ObservableCollection<Place>();

        public ObservableCollection<DeathPlace> DeathPlaces { get; set; } = new ObservableCollection<DeathPlace>();


        public Grave SelectedGrave { get; set; }
        public Coffin SelectedCoffin { get; set; }
        public Flower SelectedFlower { get; set; }
        public Place SelectedPlace
        {
            get => selectedPlace;
            set
            {
                selectedPlace = value;
                Sector = selectedPlace.CemeterySectorNumber;
                Plot = selectedPlace.CemeteryPlotNumber;
            }
        }

        public DeathPlace SelectedDeathPlace { get; set; }

        public int TotalPrice { get => totalPrice; set { totalPrice = value; Signal(); } }

        public int GuestCount { get; set; }
        public bool Necrolog { get; set; } = false;
        public bool LastDiner { get; set; } = false;
        public bool LastVideo { get; set; } = false;
        public bool GuestBus { get; set; } = false;
        public bool Catafalque { get; set; } = false;
        public bool Priest { get; set; } = false;

        public DateTime SelectedDate { get; set; } = DateTime.Now;

        public int Sector { get => sector; set { sector = value; Signal(); } }
        public int Plot { get => plot; set { plot = value; Signal(); } }

        public string TitleCustomDeathPlace { get; set; }

        public string AllPrice { get => allPrice; set { allPrice = value; Signal(); } }





        public EditEventVM()
        {
            SelectAll();
            UpdateEvent = new CommandVM(() =>
            {
                OrganizationUpdate.Date = SelectedDate;
                OrganizationUpdate.Place = SelectedPlace;
                OrganizationUpdate.PlaceId = SelectedPlace.Id;
                OrganizationUpdate.GuestCount = GuestCount;

                if (OrganizationUpdate.Date.Year > DateTime.Now.Year && OrganizationUpdate.Date.Month > DateTime.Now.Month && OrganizationUpdate.Date.Date > DateTime.Now.Date)
                {
                    OrganizationUpdate.Status = false;
                }

                OrganizationUpdate.Necrology = Necrolog;
                OrganizationUpdate.LastDinner = LastDiner;
                OrganizationUpdate.LastSlideshow = LastVideo;
                OrganizationUpdate.GuestBus = GuestBus;
                OrganizationUpdate.Catafalque = Catafalque;
                OrganizationUpdate.Priest = Priest;

                if (SelectedDeathPlace != null)
                {
                    OrganizationUpdate.DeathPlace = SelectedDeathPlace;
                    OrganizationUpdate.DeathPlaceId = SelectedDeathPlace.Id;
                }
                else
                {
                    DeathPlace custom = new DeathPlace();
                    custom.Title = TitleCustomDeathPlace;
                    custom.Coffin = SelectedCoffin;
                    custom.CoffinTypeId = SelectedCoffin.Id;
                    custom.Grave = SelectedGrave;
                    custom.GraveTypeId = SelectedGrave.Id;
                    custom.Flower = SelectedFlower;
                    custom.FlowersId = SelectedFlower.Id;
                    custom.Price = custom.Coffin.Price + custom.Grave.Price + custom.Flower.Price;

                    DeathPlacesDB.GetDb().Insert(custom);

                    DeathPlaces = new ObservableCollection<DeathPlace>(DeathPlacesDB.GetDb().SelectAll());
                    OrganizationUpdate.DeathPlace = DeathPlaces.Last();
                    OrganizationUpdate.DeathPlaceId = OrganizationUpdate.DeathPlace.Id;
                }

                int totalPriceUsligi = 0;
                int totalPriceGuests = GuestCount * 150;

                bool[] list = new bool[6];
                list[0] = Necrolog;
                list[1] = LastDiner;
                list[2] = LastVideo;
                list[3] = GuestBus;
                list[4] = Catafalque;
                list[5] = Priest;

                foreach (bool item in list)
                {
                    if (item == true)
                    {
                        totalPriceUsligi += 500;
                    }
                }

                OrganizationUpdate.Price = OrganizationUpdate.DeathPlace.Price + OrganizationUpdate.Place.Price + totalPriceUsligi + totalPriceGuests;

                AllPrice = $"Цена места:{OrganizationUpdate.Place.Price} + Цена Услуг:{totalPriceUsligi} + Цена за гостя:{totalPriceGuests} + Цена организации похоронного места:{OrganizationUpdate.DeathPlace.Price}(Гроб:{OrganizationUpdate.DeathPlace.Coffin.Price}Памятник:{OrganizationUpdate.DeathPlace.Grave.Price}Цветы:{OrganizationUpdate.DeathPlace.Flower.Price}) = Общая цена:{OrganizationUpdate.Price}";


                OrganizationsDB.GetDb().Update(OrganizationUpdate);


            }, () => true);


        }



        internal void SetOrganization(Organization organization)
        {
            this.OrganizationUpdate = organization;
        }

        private void SelectAll()
        {
            DeathPlaces = new ObservableCollection<DeathPlace>(DeathPlacesDB.GetDb().SelectAll());
            Coffins = new ObservableCollection<Coffin>(CoffinsDB.GetDb().SelectAll());
            Graves = new ObservableCollection<Grave>(GravesDB.GetDb().SelectAll());
            Flowers = new ObservableCollection<Flower>(FlowersDB.GetDb().SelectAll());
            Places = new ObservableCollection<Place>(PlacesDB.GetDb().SelectAll());
        }
    }
}
