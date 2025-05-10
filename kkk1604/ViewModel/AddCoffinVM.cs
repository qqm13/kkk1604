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
        private Coffin coffinHere;

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
        private string coffinHereTitle;

        public string CoffinHereTitle { get => coffinHereTitle; set { coffinHereTitle = value; Signal(); } }

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
                if(CoffinHere != null)
                {
                    CoffinHereTitle = coffinHere.Title;
                    SelectedMaterial = coffinHere.Material;
                    SelectedSize = coffinHere.Size;
                    PriceHere = coffinHere.Price;
                }
                else
                {
                    CoffinHereTitle = "";
                    SelectedMaterial = null;
                    SelectedSize = null;
                    PriceHere = 0;
                }
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

        public int MasterPrice { get; set; } = 10000;

        public Material SelectedMaterial 
        { 
            get => selectedMaterial;
            set
            {
                selectedMaterial = value;
               if(SelectedSize != null && SelectedMaterial != null)
                {
                    int Sdna = selectedSize.Length * selectedSize.Width;
                    int Ssides =  (selectedSize.Length * selectedSize.Height) + (selectedSize.Width * selectedSize.Height);
                    int Skrishka = selectedSize.Length * selectedSize.Width;
                    int SAll = (Sdna + Skrishka + Ssides);
                    PriceHere =( SAll * selectedMaterial.Price / 10000) + MasterPrice;
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
                    int Sdna = selectedSize.Length * selectedSize.Width;
                    int Ssides = (selectedSize.Length * selectedSize.Height) + (selectedSize.Width * selectedSize.Height);
                    int Skrishka = selectedSize.Length * selectedSize.Width;
                    int SAll = (Sdna + Skrishka + Ssides);
                    PriceHere = (SAll * selectedMaterial.Price / 10000) + MasterPrice;
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
                CoffinHere.Title = SelectedMaterial.Title + " " + SelectedSize.Title;
                CoffinHere.Price = PriceHere;

                CoffinsDB.GetDb().Update(CoffinHere);
                SelectAll();
            }, () => CoffinHere != null && SelectedMaterial != null && SelectedSize != null );

            RemoveCoffin = new CommandVM(() =>
            {
                CoffinsDB.GetDb().Remove(CoffinHere);
                SelectAll();
            }, () => CoffinHere != null);

            AddCoffin = new CommandVM(() =>
            {
                Coffin coffinAdd = new Coffin();
                coffinAdd.Title = SelectedMaterial.Title + " " + SelectedSize.Title;
                coffinAdd.Material = SelectedMaterial;
                coffinAdd.Size = SelectedSize;
                coffinAdd.Price =PriceHere;

                CoffinsDB.GetDb().Insert(coffinAdd);
                SelectAll();
            }, () =>   SelectedMaterial != null && SelectedSize != null);


        }


        private void SelectAll()
        {
            Coffins = new ObservableCollection<Coffin>(CoffinsDB.GetDb().SelectAll());
            Materials = new ObservableCollection<Material>(MaterialsDB.GetDb().SelectAll());
            Sizes = new ObservableCollection<Size>(SizesDB.GetDb().SelectAll());
        }

    }
}
