using kkk1604.Model;
using kkk1604.Model.Db;
using kkk1604.View;
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
        private int formhereHeight;
        private int formhereWidth;
        private int formhereLength;

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
                    FormhereHeight = formhere.Height;
                    FormhereWidth = formhere.Width;
                    FormhereLength = formhere.Length;
                }
                else
                {
                    FormhereTitle = "";
                    FormhereHeight = 0;
                    FormhereWidth = 0;
                    FormhereLength = 0;
                }
                Signal();
            } 
        }

        public CommandVM UpdateForm { get; set; }
        public CommandVM RemoveForm { get; set; }
        public CommandVM AddForm { get; set; }



        public string FormhereTitle { get => formhereTitle; set { formhereTitle = value; Signal(); } }
        public int FormhereHeight { get => formhereHeight; set { formhereHeight = value; Signal(); } }
        public int FormhereWidth { get => formhereWidth; set { formhereWidth = value; Signal(); } }
        public int FormhereLength { get => formhereLength; set { formhereLength = value; Signal(); } }



        public AddFormVM()
        {
            SelectAll();

            UpdateForm = new CommandVM(() =>
            {
                Formhere.Title = FormhereTitle;
                Formhere.Height = FormhereHeight;
                Formhere.Width = FormhereWidth;
                Formhere.Length = FormhereLength;
                FormsDB.GetDb().Update(Formhere);
                SelectAll();
            }, () => Formhere != null && string.IsNullOrWhiteSpace(FormhereTitle) == false && FormhereHeight != 0 && FormhereLength != 0 && formhereWidth != 0);

            RemoveForm = new CommandVM(() =>
            {
                FormsDB.GetDb().Remove(Formhere);
                SelectAll();
            }, () => Formhere != null);

            AddForm = new CommandVM(() =>
            {
                Form addform = new Form();
                addform.Title = FormhereTitle;
                addform.Height = FormhereHeight;
                addform.Width = FormhereWidth;
                addform.Length = FormhereLength;
                FormsDB.GetDb().Insert(addform);
                SelectAll();
            }, () => string.IsNullOrWhiteSpace(FormhereTitle) == false && FormhereHeight != 0 && FormhereLength != 0 && formhereWidth != 0 );


        }


        private void SelectAll()
        {
            Forms = new ObservableCollection<Form>(FormsDB.GetDb().SelectAll());
        }
    }
}
