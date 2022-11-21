using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    /// <summary>
    /// bu alanda artık hem genericRepositoryden gelen metodlara ulaşılacak hemde IPtoductRepositoryden gelen methodlara ulaşılacak.
    /// </summary>
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        // db context ile bağlanbtı kurduk
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWitCategory()
        {
            // datalar daha çekilmeden ürünlere bağlı kategorileri ve ürünleri hazır hale getirdik
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
