using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// bu alanda tabloların veri alanındaki sütunların özelliklerini belirledik
       
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

            /// burada bir ürünün bir kategorisi olur dedik ama bir kategorinin birden fazla ürünü olabilir dedik.
            builder.HasOne(x=>x.Category).WithMany(x=> x.Products).HasForeignKey(x=>x.CategoryId);
            /// </summary>
        }
    }
}
