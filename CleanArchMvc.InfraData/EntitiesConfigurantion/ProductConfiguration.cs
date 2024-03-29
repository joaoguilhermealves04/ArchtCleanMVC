﻿using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.EntitiesConfigurantion
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.HasKey(x => x.Id);
           
            builder.Property(N=> N.Nome)
                .HasMaxLength(100).IsRequired();

            builder.Property(d=>d.Description)
                .HasMaxLength(200).IsRequired();

            builder.Property(p => p.Price)
                .HasPrecision(10, 2);

            builder.HasOne(e=> e.Category).WithMany(e=>e.Products).HasForeignKey(e=>e.CategoryId);
        }
    }
}
