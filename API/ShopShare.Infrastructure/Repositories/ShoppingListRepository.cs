using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.ShoppingListAggregate;
using ShopShare.Infrastructure.Persistance;

namespace ShopShare.Infrastructure.Repositories
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly ShopShareDbContext _context;

        public ShoppingListRepository(ShopShareDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShoppingList model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ShoppingList model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public ShoppingList? Get(Func<ShoppingList, bool> predicate)
        {
            return _context.ShoppingLists.FirstOrDefault(predicate);
        }

        public IEnumerable<ShoppingList> GetAll(Func<ShoppingList, bool> predicate)
        {
            return _context.ShoppingLists
                .Where(predicate)
                .ToList();
        }

        public async Task UpdateAsync(ShoppingList model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
