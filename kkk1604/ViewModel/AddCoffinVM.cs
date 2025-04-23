using kkk1604.Model.Db;
using kkk1604.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class AddCoffinVM : BaseVM
    {
        private ObservableCollection<Coffin> coffins = new ObservableCollection<Coffin>();
        private Coffin coffinHere = new Coffin();

        private ObservableCollection<Material> materials = new ObservableCollection<Material>();
        public ObservableCollection<Material> Materials
        {
            get => materials;
            set
            {
                materials = value;
                Signal();
            }
        }
        private ObservableCollection<Size> sizes = new ObservableCollection<Size>();
        private Material selectedMaterial;
        private int priceHere;
        private Size selectedSize;

        public ObservableCollection<Size> Sizes
        {
            get => sizes;
            set
            {
                sizes = value;
                Signal();
            }
        }

        public ObservableCollection<Coffin> Coffins
        {
            get => coffins;
            set
            {
                coffins = value;
                Signal();
            }
        }
        public Coffin CoffinHere
        { 
            get => coffinHere; 
            set 
            { 
                coffinHere = value;
               
                Signal();
            }
        }
        public int PriceHere
        {
            get => priceHere;
            set
            {
                priceHere = value;
                Signal();
            }
        }

        public Material SelectedMaterial 
        { 
            get => selectedMaterial;
            set
            {
                selectedMaterial = value;
               if(SelectedSize != null && SelectedMaterial != null)
                {
                    PriceHere = SelectedMaterial.Price * SelectedSize.PriceModiffier;
                }
                Signal();
            }
        }

        public Size SelectedSize 
        {
            get => selectedSize;
            set
            {
                selectedSize = value;
                if (SelectedMaterial != null && SelectedSize != null)
                {
                    PriceHere = SelectedMaterial.Price * SelectedSize.PriceModiffier;
                }
                Signal();
            }
        }


        public CommandVM UpdateCoffin { get; set; }
        public CommandVM RemoveCoffin { get; set; }
        public CommandVM AddCoffin { get; set; }



        public AddCoffinVM()
        {
            SelectAll();

            UpdateCoffin = new CommandVM(() =>
            {
                CoffinHere.Material = SelectedMaterial;
                CoffinHere.Size = SelectedSize;

                CoffinHere.Price = PriceHere;

                CoffinsDB.GetDb().Update(CoffinHere);
                SelectAll();
            }, () => CoffinHere != null);

            RemoveCoffin = new CommandVM(() =>
            {
                CoffinsDB.GetDb().Remove(CoffinHere);
                SelectAll();
            }, () => CoffinHere != null);

            AddCoffin = new CommandVM(() =>
            {
                CoffinHere.Material = SelectedMaterial;
                CoffinHere.Size = SelectedSize;

                CoffinHere.Price =PriceHere;
                CoffinsDB.GetDb().Insert(CoffinHere);
                SelectAll();
            }, () => true);


        }


        private void SelectAll()
        {
            Coffins = new ObservableCollection<Coffin>(CoffinsDB.GetDb().SelectAll());
            Materials = new ObservableCollection<Material>(MaterialsDB.GetDb().SelectAll());
            Sizes = new ObservableCollection<Size>(SizesDB.GetDb().SelectAll());
        }

    }
}
