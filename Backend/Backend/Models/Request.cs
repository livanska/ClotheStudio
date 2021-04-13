using System.Runtime.Serialization;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Request")]
    public partial class Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request()
        {
            RequestedMaterials = new HashSet<RequestedMaterials>();
            RequestPayment = new HashSet<RequestPayment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int requestID { get; set; }

        public int companyID { get; set; }

        public int statusID { get; set; }

        public int employeeID { get; set; }

        public DateTime expectedDeadlineTime { get; set; }

        public DateTime? realReceivingTime { get; set; }

        public DateTime updateDate { get; set; }

        public DateTime createDate { get; set; }

        [IgnoreDataMember]
        public virtual Employee Employee { get; set; }

        [IgnoreDataMember]
        public virtual SupplierCompany SupplierCompany { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestedMaterials> RequestedMaterials { get; set; }

        [IgnoreDataMember]
        public virtual RequestStatus RequestStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestPayment> RequestPayment { get; set; }
    }
}
