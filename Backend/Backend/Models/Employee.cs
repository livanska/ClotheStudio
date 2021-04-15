using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Order = new HashSet<Order>();
            OrderedItems = new HashSet<OrderedItems>();
            Payment = new HashSet<Payment>();
            Request = new HashSet<Request>();
            RequestedMaterialRealization = new HashSet<RequestedMaterialRealization>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int employeeID { get; set; }

        public int atelieID { get; set; }

        [Required]
        [StringLength(50)]
        public string firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string lastname { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string phoneNumber { get; set; }

        public int positionID { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        
        [StringLength(50)]
        public string password { get; set; }

        [JsonIgnore]
        public virtual Atelie Atelie { get; set; }

        public virtual Position Position { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedItems> OrderedItems { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payment { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Request { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestedMaterialRealization> RequestedMaterialRealization { get; set; }
    }
}
