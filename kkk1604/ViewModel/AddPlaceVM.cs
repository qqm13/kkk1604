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
        private Place placeHere = new Place();

        public ObservableCollection<Place> Places
        {
            get => places;
            set
            {
                places = value;
                Signal();
            }
        }
        public Place PlaceHere { get => placeHere; set { placeHere = value; Signal(); } }

        public CommandVM UpdatePlace { get; set; }
        public CommandVM RemovePlace { get; set; }
        public CommandVM AddPlace { get; set; }


        public AddPlaceVM()
        {
            SelectAll();

            UpdatePlace = new CommandVM(() =>
            {
                PlacesDB.GetDb().Update(PlaceHere);
                SelectAll();
            }, () => PlaceHere != null);

            RemovePlace = new CommandVM(() =>
            {
                PlacesDB.GetDb().Remove(PlaceHere);
                SelectAll();
            }, () => PlaceHere != null);

            AddPlace = new CommandVM(() =>
            {
                PlacesDB.GetDb().Insert(PlaceHere);
                SelectAll();
            }, () => true);


        }


        private void SelectAll()
        {
            Places = new ObservableCollection<Place>(PlacesDB.GetDb().SelectAll());
        }
    }
}
