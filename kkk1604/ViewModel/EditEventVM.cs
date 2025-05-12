using kkk1604.Model.Db;
using kkk1604.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private int guestCount;
        private bool necrolog = false;
        private bool lastDiner = false;
        private bool lastVideo = false;
        private bool guestBus = false;
        private bool catafalque = false;
        private bool priest = false;
        private Grave selectedGrave;
        private Coffin selectedCoffin;
        private Flower selectedFlower;

        public ObservableCollection<Grave> Graves { get; set; } = new ObservableCollection<Grave>();
        public ObservableCollection<Coffin> Coffins { get; set; } = new ObservableCollection<Coffin>();
        public ObservableCollection<Flower> Flowers { get; set; } = new ObservableCollection<Flower>();
        public ObservableCollection<Place> Places { get; set; } = new ObservableCollection<Place>();

        public ObservableCollection<DeathPlace> DeathPlaces { get; set; } = new ObservableCollection<DeathPlace>();


        public Grave SelectedGrave { get => selectedGrave; set { selectedGrave = value; Signal(); } }
        public Coffin SelectedCoffin { get => selectedCoffin; set { selectedCoffin = value; Signal(); } }
        public Flower SelectedFlower { get => selectedFlower; set { selectedFlower = value; Signal(); } }
        public Place SelectedPlace
        {
            get => selectedPlace;
            set
            {
                selectedPlace = value;
                if(selectedPlace != null)
                {
                    Sector = selectedPlace.CemeterySectorNumber;
                    Plot = selectedPlace.CemeteryPlotNumber;
                }
                Signal();
            }
        }

        public DeathPlace SelectedDeathPlace { get; set; }

        public int TotalPrice { get => totalPrice; set { totalPrice = value; Signal(); } }

        public int GuestCount { get => guestCount; set { guestCount = value; Signal(); } }
        public bool Necrolog { get => necrolog; set { necrolog = value; Signal(); } }
        public bool LastDiner { get => lastDiner; set { lastDiner = value; Signal(); } }
        public bool LastVideo { get => lastVideo; set { lastVideo = value; Signal(); } }
        public bool GuestBus { get => guestBus; set { guestBus = value;  Signal(); } }
        public bool Catafalque { get => catafalque; set { catafalque = value; Signal(); } }
        public bool Priest { get => priest; set { priest = value; Signal(); } }
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

                if (DateTime.Now.Year <= OrganizationUpdate.Date.Year && DateTime.Now.Month <= OrganizationUpdate.Date.Month && DateTime.Now.Day <= OrganizationUpdate.Date.Day)
                {
                    OrganizationUpdate.Status = false;
                }
                else
                {

                    OrganizationUpdate.Status = true;
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
                    custom.Title = SelectedFlower.Title + " " + SelectedCoffin.Title + " " + SelectedGrave.Title;
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

                if (Necrolog == true)
                {
                    totalPriceUsligi += 2000;
                }
                if (LastDiner == true)
                {
                    totalPriceUsligi += GuestCount * 300;
                }
                if (LastVideo == true)
                {
                    totalPriceUsligi += 2000;
                }
                if (GuestBus == true)
                {
                    totalPriceUsligi += GuestCount * 700;
                }
                if (Catafalque == true)
                {
                    totalPriceUsligi += 5000;
                }
                if (Priest == true)
                {
                    totalPriceUsligi += 10000;
                }


                OrganizationUpdate.Price = OrganizationUpdate.DeathPlace.Price + OrganizationUpdate.Place.Price + totalPriceUsligi;

                AllPrice = $"Цена места:{OrganizationUpdate.Place.Price} + Цена Услуг:{totalPriceUsligi} + Цена организации похоронного места:{OrganizationUpdate.DeathPlace.Price}(Гроб:{OrganizationUpdate.DeathPlace.Coffin.Price}Памятник:{OrganizationUpdate.DeathPlace.Grave.Price}Цветы:{OrganizationUpdate.DeathPlace.Flower.Price}) = Общая цена:{OrganizationUpdate.Price}";

              

                OrganizationsDB.GetDb().Update(OrganizationUpdate);


            }, () => true);


        }



        internal void SetOrganization(Organization organization)
        {
            this.OrganizationUpdate = organization;
            GuestCount = organization.GuestCount;
            Necrolog = organization.Necrology;
            LastDiner = organization.LastDinner;
            LastVideo = organization.LastSlideshow;
            GuestBus = organization.GuestBus;
            Catafalque = organization.Catafalque;
            Priest = organization.Priest;
            SelectedDate = organization.Date;
            SelectedPlace = organization.Place;
            SelectedCoffin = organization.DeathPlace.Coffin;
            SelectedGrave = organization.DeathPlace.Grave;
            SelectedFlower = organization.DeathPlace.Flower;
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
