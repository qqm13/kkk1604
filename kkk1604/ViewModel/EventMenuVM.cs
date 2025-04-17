using kkk1604.Model;
using kkk1604.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class EventMenuVM : BaseVM
    {
        public CommandVM OpenCreateEvent { get; set; }
        public CommandVM OpenEditEvent { get; set; }
        Funeral SelectedItem { get; set; }

        public EventMenuVM() 
        {

        OpenCreateEvent = new CommandVM(() =>
        {
              CreateEvent createEvent = new CreateEvent();
                createEvent.ShowDialog();

        }, () => true);

        OpenEditEvent = new CommandVM(() =>
        {
                EditEvent editEvent = new EditEvent();
                editEvent.ShowDialog();

        }, () => SelectedItem != null);


        }   
    }
}
