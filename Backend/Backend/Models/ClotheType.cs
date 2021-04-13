namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClotheType")]
    public partial class ClotheType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClotheType()
        {
            Services = new HashSet<Services>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clotheTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Services> Services { get; set; }
    }
}
