using System.Runtime.Serialization;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestedMaterialRealization")]
    public partial class RequestedMaterialRealization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int materialRealizationID { get; set; }

        public int requestedMaterialID { get; set; }

        public int storedMaterialID { get; set; }

        public int employeeID { get; set; }

        public int materialAmount { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        [IgnoreDataMember]
        public virtual Employee Employee { get; set; }

        [IgnoreDataMember]
        public virtual RequestedMaterials RequestedMaterials { get; set; }

        [IgnoreDataMember]
        public virtual StoredMaterials StoredMaterials { get; set; }
    }
}
