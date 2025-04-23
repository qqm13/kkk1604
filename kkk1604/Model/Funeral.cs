using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kkk1604.Model
{
    public class Funeral
    {
        public int Id {  get; set; }
        public int OrganizationId { get; set; }
        public bool Status { get; set; }
        public Organization Organization { get; set; }
    }
}
