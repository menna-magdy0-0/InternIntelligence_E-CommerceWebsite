using ECommerceAPI.Models;
using ECommerceAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetUserOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderRepository.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (order.ApplicationUserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderFromCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartRepository.GetUserCartAsync(userId);

            if (cart == null || !cart.CartItems.Any())
                return BadRequest("Cart is empty");

            var order = new Order
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = cart.CartItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                }).ToList(),
                TotalPrice = cart.CartItems.Sum(i => i.Product.Price * i.Quantity)
            };

            await _orderRepository.AddOrderAsync(order);
            await _cartRepository.ClearCartAsync(cart.Id);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            order.Status = status;
            await _orderRepository.UpdateOrderAsync(order);

            return NoContent();
        }

        [HttpPost("{orderId}/pay")]
        public async Task<IActionResult> ProcessPayment(int orderId, [FromBody] PaymentRequest request)
        {
            // 1. Get the order
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();

            // 2. Verify order belongs to user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (order.ApplicationUserId != userId) return Forbid();

            // 3. Process payment (mock version)
            bool paymentSuccess = request.PaymentToken != "fail"; // Simple mock

            if (!paymentSuccess)
                return BadRequest("Payment failed");

            // 4. Update order
            order.Status = OrderStatus.Paid;
            order.PaymentDate = DateTime.UtcNow;
            await _orderRepository.UpdateOrderAsync(order);

            return Ok(new
            {
                success = true,
                orderId = order.Id,
                status = "paid"
            });
        }

    }
}
