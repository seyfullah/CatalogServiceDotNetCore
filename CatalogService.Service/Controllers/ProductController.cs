using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowardApi.Model;
using FlowardApi.Service;

namespace FlowardApi.Controllers
{
    #region snippet_Route
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    #endregion
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _service.GetProducts();
        }

        // GET: api/Products/5
        #region snippet_GetByID
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(long id)
        {
            return await _service.GetProduct(id) ?? NotFound();
        }
        #endregion

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, Products product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateProduct(id, product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Create
        [HttpPost]
        public async Task<ActionResult<Products>> PostProduct(Products product)
        {
            await _service.CreateProduct(product);

            //return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }
        #endregion

        // DELETE: api/Products/5
        #region snippet_Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _service.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            await _service.DeleteProduct(id);
            return NoContent();
        }
        #endregion

    }
}
