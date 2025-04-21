using kkk1604.Model;
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
        private Size sizeHere = new Size();

        public ObservableCollection<Size> Sizes 
        {
            get => sizes;
            set
            {
                sizes = value;
                Signal();
            }
        }
        public Size SizeHere { get => sizeHere; set { sizeHere = value; Signal(); } }

        public CommandVM UpdateSize { get; set; }
        public CommandVM RemoveSize { get; set; }
        public CommandVM AddSize { get; set; }


        public AddSizeVM()
        {
            SelectAll();

            UpdateSize = new CommandVM(() =>
            {
                SizesDB.GetDb().Update(SizeHere);
                SelectAll();
            }, () => SizeHere != null);

            RemoveSize = new CommandVM(() =>
            {
                SizesDB.GetDb().Remove(SizeHere);
                SelectAll();
            }, () => SizeHere != null);

            AddSize = new CommandVM(() =>
            {
                SizesDB.GetDb().Insert(SizeHere);
                SelectAll();
            }, () => true);


        }


        private void SelectAll()
        {
            Sizes = new ObservableCollection<Size>(SizesDB.GetDb().SelectAll());
        }
    }
}
