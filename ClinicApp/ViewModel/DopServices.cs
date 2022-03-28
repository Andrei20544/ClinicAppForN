using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.ViewModel
{
    public class DopServices
    {
        public int ID { get; set; }
        public string NameServices { get; set; }
        public string NamePills { get; set; }
        public int? LimitAge { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int? QtyPills { get; set; }

        public DopServices(int iD, string nameServices, string namePills, int? limitAge, string value, string description, int? qtyPills)
        {
            ID = iD;
            NameServices = nameServices;
            NamePills = namePills;
            LimitAge = limitAge;
            Value = value;
            Description = description;
            QtyPills = qtyPills;
        }
    }
}
