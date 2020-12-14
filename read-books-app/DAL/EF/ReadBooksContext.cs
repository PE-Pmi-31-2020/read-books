// <copyright file="ReadBooksContext.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadBooksContext"/> class.
    /// </summary>
    public partial class ReadBooksContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadBooksContext"/> class.
        /// </summary>
        public ReadBooksContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadBooksContext"/> class.
        /// </summary>
        /// <param name="options">options.</param>
        public ReadBooksContext(DbContextOptions<ReadBooksContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets books.
        /// </summary>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets statistic.
        /// </summary>
        public virtual DbSet<Statistic> Statistic { get; set; }

        /// <summary>
        /// Gets or sets users.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// OnConfiguring.
        /// </summary>
        /// <param name="optionsBuilder">optionsBuilder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=read-books-db;Username=postgres;Password=Erik1212");
            }
        }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder">modelBuilder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author).HasColumnName("author");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Pages).HasColumnName("pages");
            });

            modelBuilder.Entity<Statistic>(entity =>
            {
                entity.ToTable("statistic");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.ReadedPages).HasColumnName("readed_pages");

                entity.Property(e => e.Review).HasColumnName("review");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Statistic)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("statistic_book_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Statistic)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("statistic_user_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("users_email_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
            });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
