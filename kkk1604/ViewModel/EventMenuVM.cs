using kkk1604.Model;
using kkk1604.Model.Db;
using kkk1604.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.ViewModel
{
    class EventMenuVM : BaseVM
    {
        public CommandVM OpenCreateEvent { get; set; }
        public CommandVM OpenEditEvent { get; set; }
        public Organization SelectedItem { get; set; }
        public ObservableCollection<Organization> Organizations { get; set; } = new ObservableCollection<Organization>();

        public EventMenuVM() 
        {
            SelectAll();

        OpenCreateEvent = new CommandVM(() =>
        {
              CreateEvent createEvent = new CreateEvent(new Organization());
                createEvent.ShowDialog();

        }, () => true);

        OpenEditEvent = new CommandVM(() =>
        {
                EditEvent editEvent = new EditEvent();
                editEvent.ShowDialog();

        }, () => SelectedItem != null);
        }

        private void SelectAll()
        {
            Organizations = new ObservableCollection<Organization>(OrganizationsDB.GetDb().SelectAll());
        }

    }
}
