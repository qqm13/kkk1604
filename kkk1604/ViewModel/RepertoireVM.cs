using kkk1604.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class RepertoireVM : BaseVM
    {
        public CommandVM OpenAddCoffin { get; set; }
        public CommandVM OpenAddFlowers { get; set; }
        public CommandVM OpenAddForm { get; set; }
        public CommandVM OpenAddGrave { get; set; }
        public CommandVM OpenAddMaterial { get; set; }
        public CommandVM OpenAddPlace { get; set; }
        public CommandVM OpenAddSize { get; set; }



        public RepertoireVM()
        {
            OpenAddCoffin = new CommandVM(() =>
            {
                AddCoffin addCoffin = new AddCoffin();  
                addCoffin.ShowDialog();

            }, () => true);

            OpenAddFlowers = new CommandVM(() =>
            {
                AddFlowers addFlowers = new AddFlowers();
                addFlowers.ShowDialog();

            }, () => true);

            OpenAddForm = new CommandVM(() =>
            {
               AddForm addForm = new AddForm();
                addForm.ShowDialog();

            }, () => true);

            OpenAddGrave = new CommandVM(() =>
            {
                AddGrave addGrave = new AddGrave();
                addGrave.ShowDialog();

            }, () => true);

            OpenAddMaterial = new CommandVM(() =>
            {
                AddMaterial addMaterial = new AddMaterial();
                addMaterial.ShowDialog();

            }, () => true);

            OpenAddPlace = new CommandVM(() =>
            {
                Addplace addPlace = new Addplace();
                addPlace.ShowDialog();

            }, () => true);

            OpenAddSize = new CommandVM(() =>
            {
                AddSize addSize = new AddSize();
                addSize.ShowDialog();

            }, () => true);

        }
    }
}
