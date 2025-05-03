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
    class AddFormVM : BaseVM
    {
        private ObservableCollection<Form> forms = new ObservableCollection<Form>();
        private Form formhere;
        private string formhereTitle;
        private int formherePriceModiffier;

        public ObservableCollection<Form> Forms
        {
            get => forms;
            set
            {
                forms = value;
                Signal();
            }
        }
        public Form Formhere { get => formhere;
            set 
            {
                formhere = value;
                if (formhere != null)
                {
                    FormhereTitle = formhere.Title;
                    FormherePriceModiffier = formhere.PriceModiffier;
                }
                Signal();
            } 
        }

        public CommandVM UpdateForm { get; set; }
        public CommandVM RemoveForm { get; set; }
        public CommandVM AddForm { get; set; }



        public string FormhereTitle { get => formhereTitle; set { formhereTitle = value; Signal(); } }
        public int FormherePriceModiffier { get => formherePriceModiffier; set { formherePriceModiffier = value; Signal(); }
}


public AddFormVM()
        {
            SelectAll();

            UpdateForm = new CommandVM(() =>
            {
                Form updateform = new Form();
                Formhere.Title = FormhereTitle;
                Formhere.PriceModiffier = FormherePriceModiffier;
                updateform = Formhere;
                FormsDB.GetDb().Update(updateform);
                SelectAll();
            }, () => Formhere != null && Formhere.Title!= null && Formhere.PriceModiffier != 0);

            RemoveForm = new CommandVM(() =>
            {
                FormsDB.GetDb().Remove(Formhere);
                SelectAll();
            }, () => Formhere != null && Formhere.Title != null && Formhere.PriceModiffier != 0);

            AddForm = new CommandVM(() =>
            {
                Form addform = new Form();
                addform.Title = FormhereTitle;
                addform.PriceModiffier = FormherePriceModiffier;
                FormsDB.GetDb().Insert(addform);
                SelectAll();
            }, () => FormhereTitle != null && FormherePriceModiffier != 0);


        }


        private void SelectAll()
        {
            Forms = new ObservableCollection<Form>(FormsDB.GetDb().SelectAll());
        }
    }
}
