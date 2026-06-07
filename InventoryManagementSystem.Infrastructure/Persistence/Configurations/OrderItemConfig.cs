using InventoryManagementSystem.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.OrderItemId);

            builder.Property(x => x.BaseUnitPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.FinalUnitPrice)
                .HasPrecision(18, 2);
        }
    }
}
