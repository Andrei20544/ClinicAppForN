namespace ClinicApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Register = new HashSet<Register>();
        }

        [Key]
        public int id_client { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        public string surname { get; set; }

        [Required]
        [StringLength(20)]
        public string middle_name { get; set; }

        public DateTime birthday { get; set; }

        [StringLength(20)]
        public string gender { get; set; }

        [Required]
        [StringLength(11)]
        public string phone { get; set; }

        [StringLength(40)]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        public string type_policy { get; set; }

        [Required]
        [StringLength(20)]
        public string policy_number { get; set; }

        [Required]
        [StringLength(20)]
        public string passport_data { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Register> Register { get; set; }
    }
}
