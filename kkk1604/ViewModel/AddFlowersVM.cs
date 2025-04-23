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
    class AddFlowersVM : BaseVM
    {
        private ObservableCollection<Flower> flowers = new ObservableCollection<Flower>();
        private Flower flowerHere = new Flower();

        public ObservableCollection<Flower> Flowers
        {
            get => flowers;
            set
            {
                flowers = value;
                Signal();
            }
        }
        public Flower FlowerHere { get => flowerHere; set { flowerHere = value; Signal(); } }

        public CommandVM UpdateFlower { get; set; }
        public CommandVM RemoveFlower { get; set; }
        public CommandVM AddFlower { get; set; }


        public AddFlowersVM()
        {
            SelectAll();

            UpdateFlower = new CommandVM(() =>
            {
                FlowersDB.GetDb().Update(FlowerHere);
                SelectAll();
            }, () => FlowerHere != null);

            RemoveFlower = new CommandVM(() =>
            {
                FlowersDB.GetDb().Remove(FlowerHere);
                SelectAll();
            }, () => FlowerHere != null);

            AddFlower = new CommandVM(() =>
            {
                FlowersDB.GetDb().Insert(FlowerHere);
                SelectAll();
            }, () => true);


        }


        private void SelectAll()
        {
            Flowers = new ObservableCollection<Flower>(FlowersDB.GetDb().SelectAll());
        }
    }
}
