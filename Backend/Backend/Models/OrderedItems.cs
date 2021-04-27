using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderedItems
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderedItems()
        {
            RequiredMaterialsForOrderedItem = new HashSet<RequiredMaterialsForOrderedItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orderedItemID { get; set; }

        public int orderID { get; set; }

        public int employeeID { get; set; }

        public int serviceID { get; set; }

        public DateTime? doneTime { get; set; }

        [Required]
        public string description { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

     
        public virtual Services Services { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequiredMaterialsForOrderedItem> RequiredMaterialsForOrderedItem { get; set; }
    }
}
