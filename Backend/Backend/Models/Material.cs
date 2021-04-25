using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Material")]
    public partial class Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Material()
        {
            RequestedMaterials = new HashSet<RequestedMaterials>();
            RequiredMaterialsForOrderedItem = new HashSet<RequiredMaterialsForOrderedItem>();
            StoredMaterials = new HashSet<StoredMaterials>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int materialID { get; set; }

        public int materialTypeID { get; set; }

        public int colorID { get; set; }

        [Column(TypeName = "money")]
        public decimal costPerUnit { get; set; }

        public string description { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

       
        public virtual Color Color { get; set; }

        public virtual MaterialType MaterialType { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestedMaterials> RequestedMaterials { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequiredMaterialsForOrderedItem> RequiredMaterialsForOrderedItem { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoredMaterials> StoredMaterials { get; set; }
    }
}
