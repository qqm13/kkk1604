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
        private int forhHereMaterialUsage;

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
                    ForhHereMaterialUsage = formhere.MaterialUsage;
                }
                else
                {
                    FormhereTitle = "";
                    FormhereHeight = 0;
                    FormhereWidth = 0;
                    ForhHereMaterialUsage = 0;
                }
                Signal();
            } 
        }

        public CommandVM UpdateForm { get; set; }
        public CommandVM RemoveForm { get; set; }
        public CommandVM AddForm { get; set; }



        public string FormhereTitle { get => formhereTitle; set { formhereTitle = value; Signal(); } }
        public int FormhereHeight { get => formhereHeight; set
            {
                formhereHeight = value;
                if(FormhereHeight != null && FormhereWidth != null)
                {
                    double height = FormhereHeight;
                    double width = FormhereWidth;
                    height = height / 100;
                    width = width / 100;
                    if (height > Math.Round(height))
                    {
                        height = height + 1;
                    }
                    height = Math.Round(height);

                    int iheight = Convert.ToInt32(height);

                    if (width > Math.Round(width))
                    {
                        width = width + 1;
                    }
                    width = Math.Round(width);

                    int iwidth = Convert.ToInt32(width);

                    ForhHereMaterialUsage = iheight * iwidth;
                }
                Signal(); 
            } 
        }
        public int FormhereWidth 
        { 
            get => formhereWidth;
            set
            {
                formhereWidth = value;
                if (FormhereHeight != null && FormhereWidth != null)
                {
                    double height = FormhereHeight;
                    double width = FormhereWidth;
                    height = height / 100;
                    width = width / 100;
                    if (height > Math.Round(height))
                    {
                        height = height + 1;
                    }
                    height = Math.Round(height);

                    int iheight = Convert.ToInt32(height);

                    if (width > Math.Round(width))
                    {
                        width = width + 1;
                    }
                    width = Math.Round(width);

                    int iwidth = Convert.ToInt32(width);

                    ForhHereMaterialUsage = iheight * iwidth;
                }
                Signal();
            } 
        }
        public int ForhHereMaterialUsage { get => forhHereMaterialUsage; set { forhHereMaterialUsage = value; Signal(); } }



        public AddFormVM()
        {
            SelectAll();

            UpdateForm = new CommandVM(() =>
            {
                Formhere.Title = FormhereTitle;
                Formhere.Height = FormhereHeight;
                Formhere.Width = FormhereWidth;
                Formhere.MaterialUsage = ForhHereMaterialUsage;
                FormsDB.GetDb().Update(Formhere);
                SelectAll();
            }, () => Formhere != null && string.IsNullOrWhiteSpace(FormhereTitle) == false && FormhereHeight != 0 && ForhHereMaterialUsage != 0 && formhereWidth != 0);

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
                addform.MaterialUsage = ForhHereMaterialUsage;
                FormsDB.GetDb().Insert(addform);
                SelectAll();
            }, () => string.IsNullOrWhiteSpace(FormhereTitle) == false && FormhereHeight != 0 && ForhHereMaterialUsage != 0 && formhereWidth != 0 );


        }


        private void SelectAll()
        {
            Forms = new ObservableCollection<Form>(FormsDB.GetDb().SelectAll());
        }
    }
}
