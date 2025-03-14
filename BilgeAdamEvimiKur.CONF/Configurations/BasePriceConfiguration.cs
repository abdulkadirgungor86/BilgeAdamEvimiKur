using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.CONF.Configurations
{
    public abstract class BasePriceConfiguration<T> : BaseConfiguration<T> where T : BasePriceSpec
    {
        public override void Configure (EntityTypeBuilder<T> builder)
        {
            base.Configure (builder);
            builder.Property(x => x.Price).HasColumnType("money");
        }
    }
}
