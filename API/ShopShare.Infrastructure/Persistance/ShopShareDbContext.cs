﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopShare.Domain.ShoppingListAggregate;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Persistance
{
    public class ShopShareDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ShopShareDbContext(DbContextOptions<ShopShareDbContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingList> ShoppingLists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ShopShareDbContext).Assembly);
        }
    }
}
