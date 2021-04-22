using System.Runtime.Serialization;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual RequestedMaterials RequestedMaterials { get; set; }

        [JsonIgnore]
        public virtual StoredMaterials StoredMaterials { get; set; }
    }
}
