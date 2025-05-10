using kkk1604.Model;
using kkk1604.Model.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class AddPlaceVM : BaseVM
    {
        private ObservableCollection<Place> places = new ObservableCollection<Place>();
        private Place placeHere;
        private string placeHereCemetaryAdress;
        private int placeHereCemeterySectorNumber;
        private int placeHereCemeteryPlotNumber;
        private int placeHerePrice;

        public ObservableCollection<Place> Places
        {
            get => places;
            set
            {
                places = value;
                Signal();
            }
        }
        public Place PlaceHere
        {
            get => placeHere;
            set
            {
                placeHere = value;
                if (placeHere != null)
                {
                    PlaceHereCemetaryAdress = placeHere.CemetaryAdress;
                    PlaceHereCemeterySectorNumber = placeHere.CemeterySectorNumber;
                    PlaceHereCemeteryPlotNumber = placeHere.CemeteryPlotNumber;
                    PlaceHerePrice = placeHere.Price;
                }
                else
                {
                    PlaceHereCemetaryAdress = "";
                    PlaceHereCemeterySectorNumber = 0;
                    PlaceHereCemeteryPlotNumber = 0;
                    PlaceHerePrice = 0;
                }
                Signal();
            }
        }

        public CommandVM UpdatePlace { get; set; }
        public CommandVM RemovePlace { get; set; }
        public CommandVM AddPlace { get; set; }

        public string PlaceHereCemetaryAdress { get => placeHereCemetaryAdress; set { placeHereCemetaryAdress = value; Signal(); } }
        public int PlaceHereCemeterySectorNumber { get => placeHereCemeterySectorNumber; set { placeHereCemeterySectorNumber = value; Signal(); } }
        public int PlaceHereCemeteryPlotNumber { get => placeHereCemeteryPlotNumber; set { placeHereCemeteryPlotNumber = value; Signal(); } }
        public int PlaceHerePrice { get => placeHerePrice; set{ placeHerePrice = value; Signal(); } }



        public AddPlaceVM()
        {
            SelectAll();

            UpdatePlace = new CommandVM(() =>
            {
                PlaceHere.CemetaryAdress = PlaceHereCemetaryAdress;
                PlaceHere.CemeteryPlotNumber = PlaceHereCemeteryPlotNumber;
                PlaceHere.Price = PlaceHerePrice;
                PlaceHere.CemeterySectorNumber = PlaceHereCemeterySectorNumber;

                PlacesDB.GetDb().Update(PlaceHere);
                SelectAll();
            }, () => PlaceHere != null && PlaceHereCemeterySectorNumber != 0 && PlaceHereCemeteryPlotNumber != 0 && PlaceHerePrice != 0 && string.IsNullOrWhiteSpace(PlaceHereCemetaryAdress) == false);

            RemovePlace = new CommandVM(() =>
            {
                PlacesDB.GetDb().Remove(PlaceHere);
                SelectAll();
            }, () => PlaceHere != null);

            AddPlace = new CommandVM(() =>
            {
                Place addplace = new Place();
                addplace.CemetaryAdress = PlaceHereCemetaryAdress;
                addplace.CemeteryPlotNumber = PlaceHereCemeteryPlotNumber;
                addplace.Price = PlaceHerePrice;
                addplace.CemeterySectorNumber = PlaceHereCemeterySectorNumber;
                PlacesDB.GetDb().Insert(addplace);
                SelectAll();
            }, () =>  PlaceHereCemeterySectorNumber != 0 && PlaceHereCemeteryPlotNumber != 0 && PlaceHerePrice != 0 && string.IsNullOrWhiteSpace(PlaceHereCemetaryAdress) == false);


        }


        private void SelectAll()
        {
            Places = new ObservableCollection<Place>(PlacesDB.GetDb().SelectAll());
        }
    }
}
