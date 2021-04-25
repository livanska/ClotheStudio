using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Atelie")]
    public partial class Atelie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Atelie()
        {
            Employee = new HashSet<Employee>();
            StoredMaterials = new HashSet<StoredMaterials>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int atelieID { get; set; }

        public int cityID { get; set; }

        [Required]
        [StringLength(50)]
        public string address { get; set; }

        public DateTime createDate { get; set; }


        public DateTime updateDate { get; set; }

        [JsonIgnore]
        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoredMaterials> StoredMaterials { get; set; }
    }
}
