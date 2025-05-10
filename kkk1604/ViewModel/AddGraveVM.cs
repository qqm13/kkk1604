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
        public int MasterPrice { get; set; } = 10000;

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
        private string graveHereTitle;

        public string GraveHereTitle { get => graveHereTitle; set { graveHereTitle = value; Signal(); } }

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
                if (GraveHere != null)
                {
                    GraveHereTitle = graveHere.Title;
                    SelectedMaterial = graveHere.Material;
                    selectedForm = graveHere.Form;
                    PriceHere = graveHere.Price;
                }
                else
                {
                    GraveHereTitle = null;
                    SelectedMaterial = null;
                    selectedForm = null;
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

        public Material SelectedMaterial
        {
            get => selectedMaterial;
            set
            {
                selectedMaterial = value;
                
               
                Signal();
            }
        }

        public Form SelectedForm
        {
            get => selectedForm;
            set
            {
                selectedForm = value;
               
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
                GraveHere.Title = SelectedForm.Title + " " + SelectedMaterial.Title;

                GravesDB.GetDb().Update(GraveHere);
                SelectAll();
            }, () => GraveHere != null && SelectedMaterial != null && SelectedForm != null );

            RemoveGrave = new CommandVM(() =>
            {
                GravesDB.GetDb().Remove(GraveHere);
                SelectAll();
            }, () => GraveHere != null);

            AddGrave = new CommandVM(() =>
            {
                Grave graveAdd = new Grave();
                graveAdd.Material = SelectedMaterial;
                graveAdd.Form = SelectedForm;
                graveAdd.Price = PriceHere;
                graveAdd.Title = SelectedForm.Title + " " + SelectedMaterial.Title;

                GravesDB.GetDb().Insert(graveAdd);
                SelectAll();
            }, () =>  SelectedMaterial != null && SelectedForm != null);


        }


        private void SelectAll()
        {
            Graves = new ObservableCollection<Grave>(GravesDB.GetDb().SelectAll());
            Materials = new ObservableCollection<Material>(MaterialsDB.GetDb().SelectAll());
            Forms = new ObservableCollection<Form>(FormsDB.GetDb().SelectAll());
        }

    }
}
