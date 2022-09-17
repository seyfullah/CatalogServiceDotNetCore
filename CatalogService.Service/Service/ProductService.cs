using FlowardApi.Helpers;
using FlowardApi.Model;
using FlowardApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowardApi.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductDb _context;
        public ProductService(ProductDb context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ActionResult<Products>?> GetProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<ActionResult<string>> UpdateProduct(long id, Products product)
        {
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return new ActionResult<string>("NotFound");
                }
                else
                {
                    throw;
                }
            }

            return new ActionResult<string>("NoContent");
        }

        public async Task<ActionResult<Products>> CreateProduct(Products product)
        {
            ProducerHelper.NotifyForNewProduct(product);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ActionResult<Products>(product);
        }

        public async Task<ActionResult<string>> DeleteProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ActionResult<string>("NotFound");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ActionResult<string>("NoContent");
        }

        public bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
