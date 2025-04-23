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
        private Form formhere = new Form();

        public ObservableCollection<Form> Forms
        {
            get => forms;
            set
            {
                forms = value;
                Signal();
            }
        }
        public Form Formhere { get => formhere; set { formhere = value; Signal(); } }

        public CommandVM UpdateForm { get; set; }
        public CommandVM RemoveForm { get; set; }
        public CommandVM AddForm { get; set; }


        public AddFormVM()
        {
            SelectAll();

            UpdateForm = new CommandVM(() =>
            {
                FormsDB.GetDb().Update(Formhere);
                SelectAll();
            }, () => Formhere != null);

            RemoveForm = new CommandVM(() =>
            {
                FormsDB.GetDb().Remove(Formhere);
                SelectAll();
            }, () => Formhere != null);

            AddForm = new CommandVM(() =>
            {
                FormsDB.GetDb().Insert(Formhere);
                SelectAll();
            }, () => true);


        }


        private void SelectAll()
        {
            Forms = new ObservableCollection<Form>(FormsDB.GetDb().SelectAll());
        }
    }
}
