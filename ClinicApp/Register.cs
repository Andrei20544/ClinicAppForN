namespace ClinicApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Register")]
    public partial class Register
    {
        [Key]
        public int id_register { get; set; }

        public int id_client { get; set; }

        public int id_service { get; set; }

        public int id_doctor { get; set; }

        public DateTime date_register { get; set; }

        public virtual Client Client { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Services Services { get; set; }
    }
}
