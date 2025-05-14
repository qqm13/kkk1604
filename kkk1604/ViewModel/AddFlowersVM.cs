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
        private Flower flowerHere;
        private string flowerHereTitle;
        private int flowerHereCount;
        private int flowerHerePrice;

        public ObservableCollection<Flower> Flowers
        {
            get => flowers;
            set
            {
                flowers = value;
                Signal();
            }
        }
        public Flower FlowerHere 
        {
            get => flowerHere;
            set
            {
                flowerHere = value;
                if(flowerHere != null)
                {
                    FlowerHereTitle = flowerHere.Title;
                    FlowerHereCount = flowerHere.Price;
                    FlowerHerePrice = flowerHere.Count;
                }
                else
                {

                    FlowerHereTitle = "";
                    FlowerHereCount = 0;
                    FlowerHerePrice = 0;
                }
                Signal(); 
            } 
        }

        public CommandVM UpdateFlower { get; set; }
        public CommandVM RemoveFlower { get; set; }
        public CommandVM AddFlower { get; set; }

        public string FlowerHereTitle { get => flowerHereTitle; set { flowerHereTitle = value; Signal(); } }
        public int FlowerHereCount { get => flowerHereCount; set { flowerHereCount = value; Signal(); } }
        public int FlowerHerePrice { get => flowerHerePrice; set { flowerHerePrice = value; Signal(); } }

        public AddFlowersVM()
        {
            SelectAll();

            UpdateFlower = new CommandVM(() =>
            {
                FlowerHere.Title = FlowerHereTitle;
                FlowerHere.Count = FlowerHereCount;
                FlowerHere.Price = FlowerHerePrice;
                FlowersDB.GetDb().Update(FlowerHere);
                SelectAll();
            }, () => FlowerHere != null && FlowerHereCount != 0 && FlowerHerePrice != 0 && string.IsNullOrWhiteSpace(FlowerHereTitle) == false);

            RemoveFlower = new CommandVM(() =>
            {
                FlowersDB.GetDb().Remove(FlowerHere);
                SelectAll();
            }, () => FlowerHere != null);

            AddFlower = new CommandVM(() =>
            {
                Flower flowerAdd = new Flower();
                flowerAdd.Title = FlowerHereTitle;
                flowerAdd.Count = FlowerHereCount;
                flowerAdd.Price = FlowerHerePrice;

                FlowersDB.GetDb().Insert(flowerAdd);
                SelectAll();
            }, () =>  FlowerHereCount != 0 && FlowerHerePrice != 0 && string.IsNullOrWhiteSpace(FlowerHereTitle) == false);


        }


        private void SelectAll()
        {
            Flowers = new ObservableCollection<Flower>(FlowersDB.GetDb().SelectAll());
        }
    }
}
