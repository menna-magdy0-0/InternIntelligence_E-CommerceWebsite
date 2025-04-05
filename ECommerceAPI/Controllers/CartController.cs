using ECommerceAPI.Models;
using ECommerceAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartController(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartRepository.GetUserCartAsync(userId);

            if (cart == null)
            {
                cart = new Cart { ApplicationUserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }

            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<ActionResult> AddItemToCart([FromBody] CartItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartRepository.GetUserCartAsync(userId) ??
                      new Cart { ApplicationUserId = userId };

            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            if (product == null) return BadRequest("Invalid product ID");

            if (cart.Id == 0)
            {
                await _cartRepository.CreateCartAsync(cart);
            }

            await _cartRepository.AddItemToCartAsync(cart.Id, item);
            return Ok();
        }

        [HttpPut("items/{productId}")]
        public async Task<ActionResult> UpdateCartItem(int productId, [FromBody] int quantity)
        {
            if (quantity <= 0) return BadRequest("Quantity must be greater than 0");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartRepository.GetUserCartAsync(userId);
            if (cart == null) return NotFound("Cart not found");

            await _cartRepository.UpdateItemQuantityAsync(cart.Id, productId, quantity);
            return Ok();
        }

        [HttpDelete("items/{productId}")]
        public async Task<ActionResult> RemoveItemFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartRepository.GetUserCartAsync(userId);
            if (cart == null) return NotFound("Cart not found");

            await _cartRepository.RemoveItemFromCartAsync(cart.Id, productId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartRepository.GetUserCartAsync(userId);
            if (cart == null) return NotFound("Cart not found");

            await _cartRepository.ClearCartAsync(cart.Id);
            return NoContent();
        }
    }
}
