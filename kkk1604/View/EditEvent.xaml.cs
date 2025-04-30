using kkk1604.Model;
using kkk1604.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kkk1604.View
{
    /// <summary>
    /// Логика взаимодействия для EditEvent.xaml
    /// </summary>
    public partial class EditEvent : Window
    {
        public EditEvent(Organization organization)
        {
            InitializeComponent();
            ((EditEventVM)this.DataContext).SetOrganization(organization);
        }
    }
}
