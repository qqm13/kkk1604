using kkk1604.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.ViewModel
{
    class MainVM : BaseVM
    {
        public CommandVM OpenPriceTest { get; set; }
        public CommandVM OpenEventMenu { get; set; }
        public CommandVM OpenRepertoire { get; set; }

        public MainVM()
        {

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

        }

    }
}
