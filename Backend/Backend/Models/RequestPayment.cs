using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestPayment")]
    public partial class RequestPayment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestPayment()
        {
            Request = new HashSet<Request>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int requestPaymentID { get; set; }


        public int paymentID { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        [JsonIgnore]
        public virtual Payment Payment { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Request { get; set; }
    }
}
