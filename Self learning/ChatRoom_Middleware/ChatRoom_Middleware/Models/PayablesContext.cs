using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatRoom_Middleware.Models
{
    public partial class PayablesContext : DbContext
    {
        public PayablesContext()
        {
        }

        public PayablesContext(DbContextOptions<PayablesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArchivedInvoiceLineItems> ArchivedInvoiceLineItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Payables;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArchivedInvoiceLineItems>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceID, e.InvoiceSequence });

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
