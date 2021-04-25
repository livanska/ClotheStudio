using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RequestedMaterials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestedMaterials()
        {
            RequestedMaterialRealization = new HashSet<RequestedMaterialRealization>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int requestedMaterialID { get; set; }

       
        public int requestID { get; set; }

        public int materialID { get; set; }

        public int amount { get; set; }

        public int? cost { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        [JsonIgnore]
        public virtual Material Material { get; set; }

        [JsonIgnore]
        public virtual Request Request { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestedMaterialRealization> RequestedMaterialRealization { get; set; }
    }
}
