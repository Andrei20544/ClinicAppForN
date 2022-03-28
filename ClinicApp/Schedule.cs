namespace ClinicApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class Schedule
    {
        [Key]
        public int id_schedule { get; set; }

        [StringLength(20)]
        public string momday { get; set; }

        [StringLength(20)]
        public string tuesday { get; set; }

        [StringLength(20)]
        public string wednesday { get; set; }

        [StringLength(20)]
        public string thursday { get; set; }

        [StringLength(20)]
        public string friday { get; set; }

        [StringLength(20)]
        public string saturday { get; set; }

        [StringLength(20)]
        public string sunday { get; set; }

        public int id_doctor { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
