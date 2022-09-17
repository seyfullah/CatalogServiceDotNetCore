using FlowardApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlowardApi.Service
{
    public interface IProductService
    {
        public Task<ActionResult<IEnumerable<Products>>> GetProducts();
        public Task<ActionResult<Products>?> GetProduct(long id);
        public Task<ActionResult<string>> UpdateProduct(long id, Products product);

        public Task<ActionResult<Products>> CreateProduct(Products product);
        public Task<ActionResult<string>> DeleteProduct(long id);

        public bool ProductExists(long id);

    }
}