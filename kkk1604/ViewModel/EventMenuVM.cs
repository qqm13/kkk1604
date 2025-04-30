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
        private ObservableCollection<Organization> organizations = new ObservableCollection<Organization>();

        public CommandVM OpenCreateEvent { get; set; }
        public CommandVM OpenEditEvent { get; set; }
        public CommandVM RemoveEvent { get; set; }
        public Organization SelectedEvent { get; set; }
        public ObservableCollection<Organization> Organizations { get => organizations; set { organizations = value;Signal(); } }

        public EventMenuVM() 
        {
            SelectAll();

        OpenCreateEvent = new CommandVM(() =>
        {
             Organization organization = new Organization();
              CreateEvent createEvent = new CreateEvent(organization);
                createEvent.ShowDialog();
            SelectAll();

        }, () => true);

        OpenEditEvent = new CommandVM(() =>
        {
                EditEvent editEvent = new EditEvent(SelectedEvent);
                editEvent.ShowDialog();
            SelectAll();

        }, () => SelectedEvent != null);

            RemoveEvent = new CommandVM(() =>
            {
                OrganizationsDB.GetDb().Remove(SelectedEvent);
                SelectAll();
            }, () => SelectedEvent != null);
        }

        private void SelectAll()
        {
            Organizations = new ObservableCollection<Organization>(OrganizationsDB.GetDb().SelectAll());
        }

    }
}
