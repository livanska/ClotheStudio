using System.Runtime.Serialization;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequiredMaterialsForOrderedItem")]
    public partial class RequiredMaterialsForOrderedItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int requiredMaterialID { get; set; }

        public int amount { get; set; }

        public int storedMaterialID { get; set; }

        public int? orderedItemID { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        public int? materialID { get; set; }

        [IgnoreDataMember]
        public virtual Material Material { get; set; }

        [IgnoreDataMember]
        public virtual OrderedItems OrderedItems { get; set; }

        [IgnoreDataMember]
        public virtual StoredMaterials StoredMaterials { get; set; }
    }
}
