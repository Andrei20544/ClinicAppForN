using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.ViewModel
{
    public class DopMedConsumption
    {

        public int ID { get; set; }
        public int? QTY { get; set; }
        public int? Cost { get; set; }
        public string Pill { get; set; }
        public string Service { get; set; }
        public int IDService { get; set; }
        public int IDPill { get; set; }
        public DateTime Date { get; set; }

        public DopMedConsumption(int iD, int? qTY, int? cost, string pill, string service, int iDService, int iDPill, DateTime date)
        {
            ID = iD;
            QTY = qTY;
            Cost = cost;
            Pill = pill;
            Service = service;
            IDService = iDService;
            IDPill = iDPill;
            Date = date;
        }
    }
}
