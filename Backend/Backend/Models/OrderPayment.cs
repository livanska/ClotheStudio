using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderPayment")]
    public partial class OrderPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orderPaymentID { get; set; }

        public int paymentID { get; set; }

        public int orderID { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

       
        public virtual Payment Payment { get; set; }
    }
}
