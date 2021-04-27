using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            OrderPayment = new HashSet<OrderPayment>();
            RequestPayment = new HashSet<RequestPayment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int paymentID { get; set; }

        public int? employeeID { get; set; }

        public int billNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal? totalCost { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayment> OrderPayment { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestPayment> RequestPayment { get; set; }
    }
}
