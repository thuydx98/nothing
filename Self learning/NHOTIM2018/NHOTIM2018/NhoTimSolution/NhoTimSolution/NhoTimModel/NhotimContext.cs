using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NhoTimSolution.NhoTimModel
{
    public partial class NhotimContext : DbContext
    {

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<CategoryNTP> CategoryNTP { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Emp_Dep> Emp_Dep { get; set; }
        public virtual DbSet<Employee_Group> Employee_Group { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Folders> Folders { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<MetaTag> MetaTag { get; set; }
        public virtual DbSet<Pages_Meta_NTP> Pages_Meta_NTP { get; set; }
        public virtual DbSet<PagesNTP> PagesNTP { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Post_TagNTP> Post_TagNTP { get; set; }
        public virtual DbSet<PostCatNTP> PostCatNTP { get; set; }
        public virtual DbSet<PostPinNTP> PostPinNTP { get; set; }
        public virtual DbSet<PostsNTP> PostsNTP { get; set; }
        public virtual DbSet<ProductsNTP> ProductsNTP { get; set; }
        public virtual DbSet<Rule_Group> Rule_Group { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<SubCategoryNTP> SubCategoryNTP { get; set; }
        public virtual DbSet<TagsNTP> TagsNTP { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=Server-Data.nhotim.vn;Database=NhoTim;User ID=test;Password=123456789");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CategoryNTP>(entity =>
            {
                entity.HasKey(e => e.CatID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DepID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Emp_Dep>(entity =>
            {
                entity.HasKey(e => new { e.DepID, e.EmpID });

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Emp_Dep)
                    .HasForeignKey(d => d.DepID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Emp_Dep_Departments");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Emp_Dep)
                    .HasForeignKey(d => d.EmpID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Emp_Dep_Employees");

                entity.HasOne(d => d.Pos)
                    .WithMany(p => p.Emp_Dep)
                    .HasForeignKey(d => d.PosID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Emp_Dep_Position");
            });

            modelBuilder.Entity<Employee_Group>(entity =>
            {
                entity.HasKey(e => new { e.EmpID, e.GroupID });

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Employee_Group)
                    .HasForeignKey(d => d.EmpID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Group_Employees");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Employee_Group)
                    .HasForeignKey(d => d.GroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Group_Group");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmpID);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Distric).HasMaxLength(10);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Province).HasMaxLength(25);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Ward).HasMaxLength(10);

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.FolderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Folders");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerID)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.FileID);

                entity.Property(e => e.CSS).HasColumnType("text");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Tag).HasColumnType("ntext");

                entity.Property(e => e.TypeFile)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.FolderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Files_Folders");
            });

            modelBuilder.Entity<Folders>(entity =>
            {
                entity.HasKey(e => e.FolderID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.SubFolder)
                    .WithMany(p => p.InverseSubFolder)
                    .HasForeignKey(d => d.SubFolderID)
                    .HasConstraintName("FK_Folders_Folders");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MetaTag>(entity =>
            {
                entity.HasKey(e => e.MetaID);

                entity.Property(e => e.MetaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MetaTag1)
                    .HasColumnName("MetaTag")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pages_Meta_NTP>(entity =>
            {
                entity.HasKey(e => new { e.PageID, e.MetaID });

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Meta)
                    .WithMany(p => p.Pages_Meta_NTP)
                    .HasForeignKey(d => d.MetaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pages_Meta_MetaTag");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Pages_Meta_NTP)
                    .HasForeignKey(d => d.PageID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pages_Meta_Pages");
            });

            modelBuilder.Entity<PagesNTP>(entity =>
            {
                entity.HasKey(e => e.PageID);

                entity.Property(e => e.PageID).ValueGeneratedNever();

                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PageTitle).HasMaxLength(20);

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.PagesNTP)
                    .HasForeignKey(d => d.FolderID)
                    .HasConstraintName("FK_Pages_Folders");

                entity.HasOne(d => d.PageChildNavigation)
                    .WithMany(p => p.InversePageChildNavigation)
                    .HasForeignKey(d => d.PageChild)
                    .HasConstraintName("FK_PagesNTP_PagesNTP");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PosID);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Post_TagNTP>(entity =>
            {
                entity.HasKey(e => new { e.PostID, e.TagID });

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Post_TagNTP)
                    .HasForeignKey(d => d.PostID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Tag_Posts");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Post_TagNTP)
                    .HasForeignKey(d => d.TagID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Tag_Tags");
            });

            modelBuilder.Entity<PostCatNTP>(entity =>
            {
                entity.HasKey(e => e.CatID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<PostsNTP>(entity =>
            {
                entity.HasKey(e => e.PostID);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LinkFB)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.PostsNTP)
                    .HasForeignKey(d => d.CatID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_PostCat");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.PostsNTP)
                    .HasForeignKey(d => d.EmpID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_Employees");

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.PostsNTP)
                    .HasForeignKey(d => d.FolderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_Folders");
            });

            modelBuilder.Entity<ProductsNTP>(entity =>
            {
                entity.HasKey(e => e.ProductID);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.LinkFB)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SEO)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ShortContent)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductsNTP)
                    .HasForeignKey(d => d.BrandID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Brand");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.ProductsNTP)
                    .HasForeignKey(d => d.EmpID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Employees");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.ProductsNTP)
                    .HasForeignKey(d => d.SubID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_SubCategory");
            });

            modelBuilder.Entity<Rule_Group>(entity =>
            {
                entity.HasKey(e => new { e.RuleID, e.GroupID });

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Rule_Group)
                    .HasForeignKey(d => d.GroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rule_Group_Group");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.Rule_Group)
                    .HasForeignKey(d => d.RuleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rule_Group_Rules");
            });

            modelBuilder.Entity<Rules>(entity =>
            {
                entity.HasKey(e => e.RuleID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SubCategoryNTP>(entity =>
            {
                entity.HasKey(e => e.SubID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.SubCategoryNTP)
                    .HasForeignKey(d => d.CatID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<TagsNTP>(entity =>
            {
                entity.HasKey(e => e.TagID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
