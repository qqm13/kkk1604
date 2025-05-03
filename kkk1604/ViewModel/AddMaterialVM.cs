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
    class AddMaterialVM : BaseVM
    {
        private ObservableCollection<Material> materials = new ObservableCollection<Material>();
        private Material materialHere;
        private string materialHereTitle;
        private int materialHerePrice;

        public ObservableCollection<Material> Materials
        {
            get => materials;
            set
            {
                materials = value;
                Signal();
            }
        }
        public Material MaterialHere { get => materialHere; set
            {
                materialHere = value;
                if (materialHere != null)
                {
                    MaterialHereTitle = materialHere.Title;
                    MaterialHerePrice = materialHere.Price;
                }
                Signal();
            } 
        }

        public CommandVM UpdateMaterial { get; set; }
        public CommandVM RemoveMaterial { get; set; }
        public CommandVM AddMaterial { get; set; }

        public string MaterialHereTitle { get => materialHereTitle; set { materialHereTitle = value; Signal(); } }
        public int MaterialHerePrice { get => materialHerePrice; set { materialHerePrice = value; Signal(); } }


        public AddMaterialVM()
        {
            SelectAll();

            UpdateMaterial = new CommandVM(() =>
            {
                MaterialHere.Title = MaterialHereTitle;
                MaterialHere.Price = MaterialHerePrice;
                MaterialsDB.GetDb().Update(MaterialHere);
                SelectAll();
            }, () => MaterialHere != null && MaterialHereTitle != null && MaterialHerePrice != 0);

            RemoveMaterial = new CommandVM(() =>
            {
                MaterialsDB.GetDb().Remove(MaterialHere);
                SelectAll();
            }, () => MaterialHere != null);

            AddMaterial = new CommandVM(() =>
            {
                Material addMaterial = new Material();
                addMaterial.Title = MaterialHereTitle;
                addMaterial.Price = MaterialHerePrice;
                MaterialsDB.GetDb().Insert(addMaterial);
                SelectAll();
            }, () => MaterialHereTitle != null && MaterialHerePrice != 0);


        }


        private void SelectAll()
        {
            Materials = new ObservableCollection<Material>(MaterialsDB.GetDb().SelectAll());
        }
    }
}
