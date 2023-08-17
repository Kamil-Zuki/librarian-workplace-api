using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace librarian_workplace_DAL.EF
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            Database.Migrate();
        }
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Reader> Readers { get; set; } = null!;
        public virtual DbSet<ReaderBook> ReaderBooks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IDeletableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "IsDeleted");
                    var notDeleted = Expression.Not(property);
                    var lambda = Expression.Lambda(notDeleted, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.ArticleNumber)
                    .HasColumnName("article_number");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("inset_date");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_date");

                entity.Property(e => e.Author)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("author");

                entity.Property(e => e.PublicationDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("publication_date");

                entity.Property(e => e.InstancesNumber)
                    .HasColumnType("int")
                    .HasColumnName("instances_number");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("boolean")
                    .HasColumnName("is_deleted");
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.ToTable("reader");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.FIO)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("fio");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("timestamp")
                    .HasColumnName("date_added");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("timestamp")
                    .HasColumnName("date_updated");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("birth_date");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("boolean")
                    .HasColumnName("is_deleted");
            });

            modelBuilder.Entity<ReaderBook>(entity =>
            {
                entity.ToTable("reader_book");

                entity.Property(e => e.Id) 
                    .HasColumnName("id");

                entity.Property(e => e.ReaderId)
                    .HasColumnType("UUID")
                    .HasColumnName("reader_id");

                entity.Property(e => e.ArticuleNumber)
                    .HasColumnType("UUID")
                    .HasColumnName("articule_number");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("boolean")
                    .HasColumnName("is_deleted");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.ReaderBooks)
                    .HasForeignKey(d => d.ReaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reader_and_reader_book_id_fkey");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ReaderBooks)
                    .HasForeignKey(d => d.ArticuleNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("book_and_reader_book_id_fkey");
            });
        }


    }
}
