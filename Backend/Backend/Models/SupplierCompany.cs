using System.Runtime.Serialization;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplierCompany")]
    public partial class SupplierCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierCompany()
        {
            Request = new HashSet<Request>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int companyID { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int cityID { get; set; }

        [Required]
        [StringLength(50)]
        public string address { get; set; }

        public int rating { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        [IgnoreDataMember]
        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Request { get; set; }
    }
}
