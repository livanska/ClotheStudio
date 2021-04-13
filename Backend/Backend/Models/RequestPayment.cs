using System.Runtime.Serialization;

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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int requestPaymentID { get; set; }

        public int requestID { get; set; }

        public int paymentID { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        [IgnoreDataMember]
        public virtual Payment Payment { get; set; }

        [IgnoreDataMember]
        public virtual Request Request { get; set; }
    }
}
