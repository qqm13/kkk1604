using kkk1604.Model;
using kkk1604.Model.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class AddSizeVM : BaseVM
    {
        private ObservableCollection<Size> sizes = new ObservableCollection<Size>();
        private Size sizeHere;
        private string sizeHereTitle;
        private int sizeHerePriceModiffier;

        public ObservableCollection<Size> Sizes 
        {
            get => sizes;
            set
            {
                sizes = value;
                Signal();
            }
        }
        public Size SizeHere { get => sizeHere; set 
            { 
                sizeHere = value;
                if (sizeHere != null)
                {
                    SizeHereTitle = sizeHere.Title;
                    SizeHerePriceModiffier = sizeHerePriceModiffier;
                }
                Signal(); 
            }
        }

        public CommandVM UpdateSize { get; set; }
        public CommandVM RemoveSize { get; set; }
        public CommandVM AddSize { get; set; }

        public string SizeHereTitle { get => sizeHereTitle; set { sizeHereTitle = value;Signal(); } }
        public int SizeHerePriceModiffier { get => sizeHerePriceModiffier; set { sizeHerePriceModiffier = value; Signal(); } }


        public AddSizeVM()
        {
            SelectAll();

            UpdateSize = new CommandVM(() =>
            {
                SizeHere.Title = SizeHereTitle;
                SizeHere.PriceModiffier = SizeHerePriceModiffier;

                SizesDB.GetDb().Update(SizeHere);
                SelectAll();
            }, () => SizeHere != null && SizeHereTitle!= null && SizeHerePriceModiffier !=0 );

            RemoveSize = new CommandVM(() =>
            {
                SizesDB.GetDb().Remove(SizeHere);
                SelectAll();
            }, () => SizeHere != null);

            AddSize = new CommandVM(() =>
            {
                Size sizeadd = new Size();
                sizeadd.Title = SizeHereTitle;
                sizeadd.PriceModiffier = SizeHerePriceModiffier;

                SizesDB.GetDb().Insert(sizeadd);
                SelectAll();
            }, () => SizeHereTitle != null && SizeHerePriceModiffier != 0);


        }


        private void SelectAll()
        {
            Sizes = new ObservableCollection<Size>(SizesDB.GetDb().SelectAll());
        }
    }
}
