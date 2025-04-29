using kkk1604.Model;
using kkk1604.Model.Db;
using kkk1604.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.ViewModel
{
    class MainVM : BaseVM
    {
        private string dateCheckResult;
        private DateTime checkDate;

        public CommandVM OpenPriceTest { get; set; }
        public CommandVM OpenEventMenu { get; set; }
        public CommandVM OpenRepertoire { get; set; }
        public CommandVM OpenDeathPlace { get; set; }
        public CommandVM DateCheck { get; set; }

        public string DateCheckResult { get => dateCheckResult; set { dateCheckResult = value; Signal(); } }
        public DateTime CheckDate { get => checkDate; set { checkDate = value; Signal();} }


        public MainVM()
        {

        DateCheck = new CommandVM(() =>
        {
            DateCheckResult = " ";
             ObservableCollection<Organization> list = new ObservableCollection<Organization>(OrganizationsDB.GetDb().SelectAll());
            foreach (Organization o in list)
            {
                if(o.Date.Year == CheckDate.Year && o.Date.Month == CheckDate.Month && o.Date.Day == CheckDate.Day)
                {
                    DateCheckResult = "Занято";
                }
            }
            if (DateCheckResult != "Занято")
                DateCheckResult = "Свободно";

        }, () => true);

            OpenPriceTest = new CommandVM(() =>
        {
            PriceTest priceTest = new PriceTest();
            priceTest.ShowDialog();

        }, () => true);

        OpenEventMenu = new CommandVM(() =>
        {
            EventMenu eventMenu = new EventMenu();
            eventMenu.ShowDialog();

        }, () => true);

        OpenRepertoire = new CommandVM(() =>
        {
              Repertoire repertoire = new Repertoire();
              repertoire.ShowDialog();

        }, () => true);

        OpenDeathPlace = new CommandVM(() =>
        {
             AddDeathplace addDeathplace = new AddDeathplace();
            addDeathplace.ShowDialog();

         }, () => true);

        }

    }
}
