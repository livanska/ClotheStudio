using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Services
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Services()
        {
            OrderedItems = new HashSet<OrderedItems>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int serviceID { get; set; }

        public int clotheTypeID { get; set; }

        public int serviceTypeID { get; set; }

        public int workCost { get; set; }

        public TimeSpan duration { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        public virtual ClotheType ClotheType { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedItems> OrderedItems { get; set; }

        public virtual ServiceType ServiceType { get; set; }
    }
}
