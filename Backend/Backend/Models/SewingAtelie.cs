namespace Backend
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SewingAtelie : DbContext
    {
        public SewingAtelie()
            : base("name=SewingAtelie")
        {
        }

        public virtual DbSet<Atelie> Atelie { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<ClotheType> ClotheType { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<MaterialType> MaterialType { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderedItems> OrderedItems { get; set; }
        public virtual DbSet<OrderPayment> OrderPayment { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestedMaterialRealization> RequestedMaterialRealization { get; set; }
        public virtual DbSet<RequestedMaterials> RequestedMaterials { get; set; }
        public virtual DbSet<RequestPayment> RequestPayment { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<RequiredMaterialsForOrderedItem> RequiredMaterialsForOrderedItem { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<StoredMaterials> StoredMaterials { get; set; }
        public virtual DbSet<SupplierCompany> SupplierCompany { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .Property(c => c.customerID)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Atelie>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Atelie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Atelie>()
                .HasMany(e => e.StoredMaterials)
                .WithRequired(e => e.Atelie)
                .HasForeignKey(e => e.atelierID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Atelie)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.SupplierCompany)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClotheType>()
                .HasMany(e => e.Services)
                .WithRequired(e => e.ClotheType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Material)
                .WithRequired(e => e.Color)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.City)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OrderedItems)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Payment)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Request)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.RequestedMaterialRealization)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Material>()
                .Property(e => e.costPerUnit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.RequestedMaterials)
                .WithRequired(e => e.Material)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.StoredMaterials)
                .WithRequired(e => e.Material)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialType>()
                .HasMany(e => e.Material)
                .WithRequired(e => e.MaterialType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderedItems)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<OrderStatus>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.OrderStatus)
                .HasForeignKey(e => e.statusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderPayment>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.OrderPayment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.totalCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.OrderPayment)
                .WithRequired(e => e.Payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.RequestPayment)
                .WithRequired(e => e.Payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.RequestedMaterials)
                .WithRequired(e => e.Request)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.RequestPayment)
                .WithRequired(e => e.Request)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestedMaterials>()
                .HasMany(e => e.RequestedMaterialRealization)
                .WithRequired(e => e.RequestedMaterials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestStatus>()
                .HasMany(e => e.Request)
                .WithRequired(e => e.RequestStatus)
                .HasForeignKey(e => e.statusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.OrderedItems)
                .WithRequired(e => e.Services)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceType>()
                .HasMany(e => e.Services)
                .WithRequired(e => e.ServiceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoredMaterials>()
                .HasMany(e => e.RequestedMaterialRealization)
                .WithRequired(e => e.StoredMaterials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoredMaterials>()
                .HasMany(e => e.RequiredMaterialsForOrderedItem)
                .WithRequired(e => e.StoredMaterials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierCompany>()
                .HasMany(e => e.Request)
                .WithRequired(e => e.SupplierCompany)
                .WillCascadeOnDelete(false);
        }
    }
}
