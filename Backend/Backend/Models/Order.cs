using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderedItems = new HashSet<OrderedItems>();
            OrderPayment = new HashSet<OrderPayment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orderID { get; set; }

        public int customerID { get; set; }

        public int statusID { get; set; }

        public int employeeID { get; set; }

        public DateTime expectedDeadlineTime { get; set; }

        public DateTime? realReceivingTime { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        public int? orderPaymentID { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [IgnoreDataMember]
        public virtual Employee Employee { get; set; }

        [IgnoreDataMember]
        public virtual OrderStatus OrderStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedItems> OrderedItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayment> OrderPayment { get; set; }
    }
}
