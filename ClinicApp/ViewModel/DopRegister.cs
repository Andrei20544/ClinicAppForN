using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.ViewModel
{
    public class DopRegister
    {
        public string NameClient { get; set; }
        public string NameServices { get; set; }
        public string NameDoctor { get; set; }
        public DateTime? Date { get; set; }
        public string role { get; set; }
        public string polNum { get; set; }
        public int IdServices { get; set; }
        public int IdDoctor { get; set; }
        public int IdRegister { get; set; }

        public DopRegister(string nameClient, string nameServices, string nameDoctor, DateTime? date, string Role = "", string PolNum = "", int idServices = 0, int idDoctor = 0, int idRegister = 0)
        {
            NameClient = nameClient;
            NameServices = nameServices;
            NameDoctor = nameDoctor;
            Date = date;
            role = Role;
            polNum = PolNum;
            IdServices = idServices;
            IdDoctor = idDoctor;
            IdRegister = idRegister;
        }
    }
}
