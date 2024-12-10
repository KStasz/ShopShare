using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopShare.Domain.ShoppingListAggregate;
using ShopShare.Domain.ShoppingListAggregate.Entities;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Infrastructure.Configurations
{
    internal class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            ConfigureShoppingListsTable(builder);
            ConfigureListItemsTable(builder);
        }

        private void ConfigureListItemsTable(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.OwnsMany(m => m.Items, ib =>
            {
                ib.ToTable("ListItems");
                ib.WithOwner().HasForeignKey("ShoppingListId");

                ib.HasKey(nameof(ListItem.Id), "ShoppingListId");

                ib.Property(x => x.Id)
                    .HasColumnName("ListItemId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => ListItemId.Create(value));

                ib.Property(x => x.ItemName)
                    .IsRequired()
                    .HasMaxLength(32);

                ib.Property(x => x.CreationDate);
            });

            builder.Metadata.FindNavigation(nameof(ShoppingList.Items))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureShoppingListsTable(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.ToTable("ShoppingLists");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ShoppingListId.Create(value));

            builder.Property(x => x.ListName)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(x => x.Description);
            builder.Property(x => x.CreationDate);
            builder.Property(x => x.UpdatedDate);
        }
    }
}
