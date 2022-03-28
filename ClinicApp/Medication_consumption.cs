namespace ClinicApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Medication_consumption
    {
        public int id { get; set; }

        public int qty { get; set; }

        public int cost { get; set; }

        public int id_pills { get; set; }

        public int id_services { get; set; }

        public DateTime date { get; set; }

        public virtual Pills Pills { get; set; }

        public virtual Services Services { get; set; }
    }
}
