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
        private Material materialHere = new Material();

        public ObservableCollection<Material> Materials
        {
            get => materials;
            set
            {
                materials = value;
                Signal();
            }
        }
        public Material MaterialHere { get => materialHere; set { materialHere = value; Signal(); } }

        public CommandVM UpdateMaterial { get; set; }
        public CommandVM RemoveMaterial { get; set; }
        public CommandVM AddMaterial { get; set; }


        public AddMaterialVM()
        {
            SelectAll();

            UpdateMaterial = new CommandVM(() =>
            {
                MaterialsDB.GetDb().Update(MaterialHere);
                SelectAll();
            }, () => MaterialHere != null);

            RemoveMaterial = new CommandVM(() =>
            {
                MaterialsDB.GetDb().Remove(MaterialHere);
                SelectAll();
            }, () => MaterialHere != null);

            AddMaterial = new CommandVM(() =>
            {
                MaterialsDB.GetDb().Insert(MaterialHere);
                SelectAll();
            }, () => true);


        }


        private void SelectAll()
        {
            Materials = new ObservableCollection<Material>(MaterialsDB.GetDb().SelectAll());
        }
    }
}
