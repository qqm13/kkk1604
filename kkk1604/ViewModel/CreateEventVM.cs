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
    class CreateEventVM : BaseVM
    {
        public Organization OrganizationAdd { get; set; } = new Organization();
        public CommandVM CreateEvent { get; set; }

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





        public CreateEventVM()
        {
            SelectAll();

            

            CreateEvent = new CommandVM(() =>
            {
                OrganizationAdd.Date = SelectedDate;
                OrganizationAdd.Place = SelectedPlace;
                OrganizationAdd.PlaceId = SelectedPlace.Id;
                OrganizationAdd.GuestCount = GuestCount;
                
                if( DateTime.Now.Year <= OrganizationAdd.Date.Year && DateTime.Now.Month <= OrganizationAdd.Date.Month && DateTime.Now.Day <= OrganizationAdd.Date.Day)
                {
                    OrganizationAdd.Status = false;
                }
                else
                {
                
                    OrganizationAdd.Status = true;
                }

                OrganizationAdd.Necrology = Necrolog;
                OrganizationAdd.LastDinner = LastDiner;
                OrganizationAdd.LastSlideshow = LastVideo;
                OrganizationAdd.GuestBus = GuestBus;
                OrganizationAdd.Catafalque = Catafalque;
                OrganizationAdd.Priest = Priest;

               if (SelectedDeathPlace != null)
                {
                    OrganizationAdd.DeathPlace = SelectedDeathPlace;
                    OrganizationAdd.DeathPlaceId = SelectedDeathPlace.Id;
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
                    OrganizationAdd.DeathPlace = DeathPlaces.Last();
                    OrganizationAdd.DeathPlaceId = OrganizationAdd.DeathPlace.Id;
                }

                int totalPriceUsligi = 0;

              

                if(Necrolog == true)
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



                OrganizationAdd.Price = OrganizationAdd.DeathPlace.Price + OrganizationAdd.Place.Price + totalPriceUsligi;

                AllPrice = $"Цена места:{OrganizationAdd.Place.Price} + Цена Услуг:{totalPriceUsligi} + Цена организации похоронного места:{OrganizationAdd.DeathPlace.Price}(Гроб:{OrganizationAdd.DeathPlace.Coffin.Price}Памятник:{OrganizationAdd.DeathPlace.Grave.Price}Цветы:{OrganizationAdd.DeathPlace.Flower.Price}) = Общая цена:{OrganizationAdd.Price}";


                OrganizationsDB.GetDb().Insert(OrganizationAdd);
                

            }, () =>true);


        }

      

        internal void SetOrganization(Organization organization)
        {
            this.OrganizationAdd = organization;
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
