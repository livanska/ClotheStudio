using System.Runtime.Serialization;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StoredMaterials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoredMaterials()
        {
            RequestedMaterialRealization = new HashSet<RequestedMaterialRealization>();
            RequiredMaterialsForOrderedItem = new HashSet<RequiredMaterialsForOrderedItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int storedMaterialID { get; set; }

        public int atelierID { get; set; }

        public int materialID { get; set; }

        public int amountLeft { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        [IgnoreDataMember]
        public virtual Atelie Atelie { get; set; }

        [IgnoreDataMember]
        public virtual Material Material { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestedMaterialRealization> RequestedMaterialRealization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequiredMaterialsForOrderedItem> RequiredMaterialsForOrderedItem { get; set; }
    }
}
