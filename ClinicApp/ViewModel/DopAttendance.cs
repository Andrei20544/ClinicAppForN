using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.ViewModel
{
    public class DopAttendance
    {
        public int IDClient { get; set; }
        public string NameClient { get; set; }
        public string NameService { get; set; }
        public double Itogo { get; set; }

        public DopAttendance(int iDClient, string nameClient, string nameService, double itogo)
        {
            IDClient = iDClient;
            NameClient = nameClient;
            NameService = nameService;
            Itogo = itogo;
        }
    }
}
