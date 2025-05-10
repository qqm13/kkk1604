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
        private int sizeHereHeight;
        private int sizeHereWidth;
        private int sizeHereLength;

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
                    SizeHereHeight = sizeHere.Height;
                    SizeHereWidth = sizeHere.Width;
                    SizeHereLength = sizeHere.Length;
                }
                else 
                    {
                    SizeHereTitle = " ";
                    SizeHereHeight = 0;
                    SizeHereWidth =0;
                    SizeHereLength =0;
                }
                Signal(); 
            }
        }

        public CommandVM UpdateSize { get; set; }
        public CommandVM RemoveSize { get; set; }
        public CommandVM AddSize { get; set; }

        public string SizeHereTitle { get => sizeHereTitle; set { sizeHereTitle = value;Signal(); } }
        public int SizeHereHeight { get => sizeHereHeight; set { sizeHereHeight = value; Signal(); } }
        public int SizeHereWidth { get => sizeHereWidth; set { sizeHereWidth = value; Signal(); } }
        public int SizeHereLength { get => sizeHereLength; set { sizeHereLength = value; Signal(); } }



        public AddSizeVM()
        {
            SelectAll();

            UpdateSize = new CommandVM(() =>
            {
                SizeHere.Title = SizeHereTitle;
                SizeHere.Height = SizeHereHeight;
                SizeHere.Width = SizeHereWidth;
                SizeHere.Length = SizeHereLength;

                SizesDB.GetDb().Update(SizeHere);
                SelectAll();
            }, () => SizeHere != null && string.IsNullOrWhiteSpace(SizeHereTitle) == false && SizeHereHeight != 0 && SizeHereWidth != 0 && SizeHereLength != 0);

            RemoveSize = new CommandVM(() =>
            {
                SizesDB.GetDb().Remove(SizeHere);
                SelectAll();
            }, () => SizeHere != null);

            AddSize = new CommandVM(() =>
            {
                Size sizeadd = new Size();
                sizeadd.Title = SizeHereTitle;
                sizeadd.Height = SizeHereHeight;
                sizeadd.Width = SizeHereWidth;
                sizeadd.Length = SizeHereLength;

                SizesDB.GetDb().Insert(sizeadd);
                SelectAll();
            }, () => SizeHereHeight != 0 && SizeHereWidth != 0 && SizeHereLength != 0 && string.IsNullOrWhiteSpace(SizeHereTitle) == false); 


        }


        private void SelectAll()
        {
            Sizes = new ObservableCollection<Size>(SizesDB.GetDb().SelectAll());
        }
    }
}
