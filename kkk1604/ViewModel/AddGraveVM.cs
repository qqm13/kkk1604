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
    class AddGraveVM : BaseVM
    {
        private ObservableCollection<Grave> graves = new ObservableCollection<Grave>();
        private Grave graveHere { get; set; } = new Grave();

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
        private ObservableCollection<Form> forms = new ObservableCollection<Form>();
        private Material selectedMaterial;
        private int priceHere;
        private Form selectedForm;

        public ObservableCollection<Form> Forms
        {
            get => forms;
            set
            {
                forms = value;
                Signal();
            }
        }

        public ObservableCollection<Grave> Graves
        {
            get => graves;
            set
            {
                graves = value;
                Signal();
            }
        }
        public Grave GraveHere
        {
            get => graveHere;
            set
            {
                graveHere = value;

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
                if (SelectedForm != null && SelectedMaterial != null)
                {
                    PriceHere = SelectedMaterial.Price * SelectedForm.PriceModiffier;
                }
                Signal();
            }
        }

        public Form SelectedForm
        {
            get => selectedForm;
            set
            {
                selectedForm = value;
                if (SelectedMaterial != null && SelectedForm != null)
                {
                    PriceHere = SelectedMaterial.Price * SelectedForm.PriceModiffier;
                }
                Signal();
            }
        }


        public CommandVM UpdateGrave { get; set; }
        public CommandVM RemoveGrave { get; set; }
        public CommandVM AddGrave { get; set; }



        public AddGraveVM()
        {
            SelectAll();

            UpdateGrave = new CommandVM(() =>
            {
                GraveHere.Material = SelectedMaterial;
                GraveHere.Form = SelectedForm;

                GraveHere.Price = PriceHere;

                GravesDB.GetDb().Update(GraveHere);
                SelectAll();
            }, () => GraveHere != null);

            RemoveGrave = new CommandVM(() =>
            {
                GravesDB.GetDb().Remove(GraveHere);
                SelectAll();
            }, () => GraveHere != null);

            AddGrave = new CommandVM(() =>
            {
                GraveHere.Material = SelectedMaterial;
                GraveHere.Form = SelectedForm;

                GraveHere.Price = PriceHere;
                GravesDB.GetDb().Insert(GraveHere);
                SelectAll();
            }, () => true);


        }


        private void SelectAll()
        {
            Graves = new ObservableCollection<Grave>(GravesDB.GetDb().SelectAll());
            Materials = new ObservableCollection<Material>(MaterialsDB.GetDb().SelectAll());
            Forms = new ObservableCollection<Form>(FormsDB.GetDb().SelectAll());
        }

    }
}
