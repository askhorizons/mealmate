﻿using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Table", "Mealmate");
            builder.HasKey(p => p.Id)
                   .HasName("PK_Table");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.LocationId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.IsActive)
                    .HasColumnType("BIT")
                    .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Location)
                .WithMany(p => p.Tables)
                .HasForeignKey(p => p.LocationId)
                .HasConstraintName("FK_Table_Location")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
