﻿using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Identity");

            builder.Property(p => p.UserName)
                .HasColumnType("NVARCHAR(350)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(350)")
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasColumnType("VARCHAR(25)");

            builder.Property(p => p.FirstName)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnType("NVARCHAR(250)");

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}
