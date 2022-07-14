﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdventureWorksNS.Data
{
    public partial class AdventureWorksDB : DbContext
    {
        public AdventureWorksDB()
        {
        }

        public AdventureWorksDB(DbContextOptions<AdventureWorksDB> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<BuildVersion> BuildVersions { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductDescription> ProductDescriptions { get; set; } = null!;
        public virtual DbSet<ProductModel> ProductModels { get; set; } = null!;
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; } = null!;
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; } = null!;
        public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; } = null!;
        public virtual DbSet<VGetAllCategory> VGetAllCategories { get; set; } = null!;
        public virtual DbSet<VProductAndDescription> VProductAndDescriptions { get; set; } = null!;
        public virtual DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3RM6B1F;Initial Catalog=AdventureWorksLT2019;Integrated Security=false;User=sa;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasComment("Street address information for customers.");

                entity.Property(e => e.AddressId).HasComment("Primary key for Address records.");

                entity.Property(e => e.AddressLine1).HasComment("First street address line.");

                entity.Property(e => e.AddressLine2).HasComment("Second street address line.");

                entity.Property(e => e.City).HasComment("Name of the city.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.PostalCode).HasComment("Postal code for the street address.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.StateProvince).HasComment("Name of state or province.");
            });

            modelBuilder.Entity<BuildVersion>(entity =>
            {
                entity.HasComment("Current version number of the AdventureWorksLT 2012 sample database. ");

                entity.Property(e => e.DatabaseVersion).HasComment("Version number of the database in 9.yy.mm.dd.00 format.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.SystemInformationId)
                    .ValueGeneratedOnAdd()
                    .HasComment("Primary key for BuildVersion records.");

                entity.Property(e => e.VersionDate).HasComment("Date and time the record was last updated.");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasComment("Customer information.");

                entity.Property(e => e.CustomerId).HasComment("Primary key for Customer records.");

                entity.Property(e => e.CompanyName).HasComment("The customer's organization.");

                entity.Property(e => e.EmailAddress).HasComment("E-mail address for the person.");

                entity.Property(e => e.FirstName).HasComment("First name of the person.");

                entity.Property(e => e.LastName).HasComment("Last name of the person.");

                entity.Property(e => e.MiddleName).HasComment("Middle name or middle initial of the person.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.NameStyle).HasComment("0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.");

                entity.Property(e => e.PasswordHash).HasComment("Password for the e-mail account.");

                entity.Property(e => e.PasswordSalt).HasComment("Random value concatenated with the password string before the password is hashed.");

                entity.Property(e => e.Phone).HasComment("Phone number associated with the person.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.SalesPerson).HasComment("The customer's sales person, an employee of AdventureWorks Cycles.");

                entity.Property(e => e.Suffix).HasComment("Surname suffix. For example, Sr. or Jr.");

                entity.Property(e => e.Title).HasComment("A courtesy title. For example, Mr. or Ms.");
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.AddressId })
                    .HasName("PK_CustomerAddress_CustomerID_AddressID");

                entity.HasComment("Cross-reference table mapping customers to their address(es).");

                entity.Property(e => e.CustomerId).HasComment("Primary key. Foreign key to Customer.CustomerID.");

                entity.Property(e => e.AddressId).HasComment("Primary key. Foreign key to Address.AddressID.");

                entity.Property(e => e.AddressType).HasComment("The kind of Address. One of: Archive, Billing, Home, Main Office, Primary, Shipping");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasComment("Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct. Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct.");

                entity.Property(e => e.ErrorLogId).HasComment("Primary key for ErrorLog records.");

                entity.Property(e => e.ErrorLine).HasComment("The line number at which the error occurred.");

                entity.Property(e => e.ErrorMessage).HasComment("The message text of the error that occurred.");

                entity.Property(e => e.ErrorNumber).HasComment("The error number of the error that occurred.");

                entity.Property(e => e.ErrorProcedure).HasComment("The name of the stored procedure or trigger where the error occurred.");

                entity.Property(e => e.ErrorSeverity).HasComment("The severity of the error that occurred.");

                entity.Property(e => e.ErrorState).HasComment("The state number of the error that occurred.");

                entity.Property(e => e.ErrorTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The date and time at which the error occurred.");

                entity.Property(e => e.UserName).HasComment("The user who executed the batch in which the error occurred.");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasComment("Products sold or used in the manfacturing of sold products.");

                entity.Property(e => e.ProductId).HasComment("Primary key for Product records.");

                entity.Property(e => e.Color).HasComment("Product color.");

                entity.Property(e => e.DiscontinuedDate).HasComment("Date the product was discontinued.");

                entity.Property(e => e.ListPrice).HasComment("Selling price.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Name).HasComment("Name of the product.");

                entity.Property(e => e.ProductCategoryId).HasComment("Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. ");

                entity.Property(e => e.ProductModelId).HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.");

                entity.Property(e => e.ProductNumber).HasComment("Unique product identification number.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.SellEndDate).HasComment("Date the product was no longer available for sale.");

                entity.Property(e => e.SellStartDate).HasComment("Date the product was available for sale.");

                entity.Property(e => e.Size).HasComment("Product size.");

                entity.Property(e => e.StandardCost).HasComment("Standard cost of the product.");

                entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");

                entity.Property(e => e.ThumbnailPhotoFileName).HasComment("Small image file name.");

                entity.Property(e => e.Weight).HasComment("Product weight.");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasComment("High-level product categorization.");

                entity.Property(e => e.ProductCategoryId).HasComment("Primary key for ProductCategory records.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Name).HasComment("Category description.");

                entity.Property(e => e.ParentProductCategoryId).HasComment("Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.HasOne(d => d.ParentProductCategory)
                    .WithMany(p => p.InverseParentProductCategory)
                    .HasForeignKey(d => d.ParentProductCategoryId)
                    .HasConstraintName("FK_ProductCategory_ProductCategory_ParentProductCategoryID_ProductCategoryID");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.HasComment("Product descriptions in several languages.");

                entity.Property(e => e.ProductDescriptionId).HasComment("Primary key for ProductDescription records.");

                entity.Property(e => e.Description).HasComment("Description of the product.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ProductModelProductDescription>(entity =>
            {
                entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture })
                    .HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

                entity.HasComment("Cross-reference table mapping product descriptions and the language the description is written in.");

                entity.Property(e => e.ProductModelId).HasComment("Primary key. Foreign key to ProductModel.ProductModelID.");

                entity.Property(e => e.ProductDescriptionId).HasComment("Primary key. Foreign key to ProductDescription.ProductDescriptionID.");

                entity.Property(e => e.Culture)
                    .IsFixedLength()
                    .HasComment("The culture for which the description is written");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ProductDescription)
                    .WithMany(p => p.ProductModelProductDescriptions)
                    .HasForeignKey(d => d.ProductDescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.ProductModelProductDescriptions)
                    .HasForeignKey(d => d.ProductModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId })
                    .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

                entity.HasComment("Individual products associated with a specific sales order. See SalesOrderHeader.");

                entity.Property(e => e.SalesOrderId).HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

                entity.Property(e => e.SalesOrderDetailId)
                    .ValueGeneratedOnAdd()
                    .HasComment("Primary key. One incremental unique number per product sold.");

                entity.Property(e => e.LineTotal)
                    .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", false)
                    .HasComment("Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.OrderQty).HasComment("Quantity ordered per product.");

                entity.Property(e => e.ProductId).HasComment("Product sold to customer. Foreign key to Product.ProductID.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.UnitPrice).HasComment("Selling price of a single product.");

                entity.Property(e => e.UnitPriceDiscount).HasComment("Discount amount.");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {
                entity.HasKey(e => e.SalesOrderId)
                    .HasName("PK_SalesOrderHeader_SalesOrderID");

                entity.HasComment("General sales order information.");

                entity.Property(e => e.SalesOrderId).HasComment("Primary key.");

                entity.Property(e => e.AccountNumber).HasComment("Financial accounting number reference.");

                entity.Property(e => e.BillToAddressId).HasComment("The ID of the location to send invoices.  Foreign key to the Address table.");

                entity.Property(e => e.Comment).HasComment("Sales representative comments.");

                entity.Property(e => e.CreditCardApprovalCode).HasComment("Approval code provided by the credit card company.");

                entity.Property(e => e.CustomerId).HasComment("Customer identification number. Foreign key to Customer.CustomerID.");

                entity.Property(e => e.DueDate).HasComment("Date the order is due to the customer.");

                entity.Property(e => e.Freight)
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Shipping cost.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.OnlineOrderFlag)
                    .HasDefaultValueSql("((1))")
                    .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");

                entity.Property(e => e.OrderDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Dates the sales order was created.");

                entity.Property(e => e.PurchaseOrderNumber).HasComment("Customer purchase order number reference. ");

                entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");

                entity.Property(e => e.Rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.SalesOrderNumber)
                    .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
                    .HasComment("Unique sales order identification number.");

                entity.Property(e => e.ShipDate).HasComment("Date the order was shipped to the customer.");

                entity.Property(e => e.ShipMethod).HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");

                entity.Property(e => e.ShipToAddressId).HasComment("The ID of the location to send goods.  Foreign key to the Address table.");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");

                entity.Property(e => e.SubTotal)
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");

                entity.Property(e => e.TaxAmt)
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Tax amount.");

                entity.Property(e => e.TotalDue)
                    .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false)
                    .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.");

                entity.HasOne(d => d.BillToAddress)
                    .WithMany(p => p.SalesOrderHeaderBillToAddresses)
                    .HasForeignKey(d => d.BillToAddressId)
                    .HasConstraintName("FK_SalesOrderHeader_Address_BillTo_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrderHeaders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ShipToAddress)
                    .WithMany(p => p.SalesOrderHeaderShipToAddresses)
                    .HasForeignKey(d => d.ShipToAddressId)
                    .HasConstraintName("FK_SalesOrderHeader_Address_ShipTo_AddressID");
            });

            modelBuilder.Entity<VGetAllCategory>(entity =>
            {
                entity.ToView("vGetAllCategories", "SalesLT");
            });

            modelBuilder.Entity<VProductAndDescription>(entity =>
            {
                entity.ToView("vProductAndDescription", "SalesLT");

                entity.HasComment("Product names and descriptions. Product descriptions are provided in multiple languages.");

                entity.Property(e => e.Culture).IsFixedLength();
            });

            modelBuilder.Entity<VProductModelCatalogDescription>(entity =>
            {
                entity.ToView("vProductModelCatalogDescription", "SalesLT");

                entity.HasComment("Displays the content from each element in the xml column CatalogDescription for each product in the Sales.ProductModel table that has catalog data.");

                entity.Property(e => e.ProductModelId).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
