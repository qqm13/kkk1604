using kkk1604.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class AddSizeVM : BaseVM
    {
        private ObservableCollection<Size> sizes = new ObservableCollection<Size>();

        public ObservableCollection<Size> Sizes 
        {
            get => sizes;
            set
            {
                sizes = value;
                Signal();
            }
        }
        public Size SelectedSize { get; set; }

        public CommandVM UpdateSize { get; set; }
        public CommandVM RemoveSize { get; set; }
        public CommandVM AddSize { get; set; }


        public AddSizeVM()
        {
            SelectAll();


        }


        private void SelectAll()
        {
            Sizes = new ObservableCollection<Size>(SizesDB.GetDb().SelectAll());
        }
    }
}
