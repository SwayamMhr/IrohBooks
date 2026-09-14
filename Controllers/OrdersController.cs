using IrohBooks.Data;
using IrohBooks.Models;
using IrohBooks.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IrohBooks.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly Repository<Order> _orders;
        private readonly Repository<Product> _products;

        public OrdersController(ApplicationDbContext context)
        {
            _orders = new Repository<Order>(context);
            _products = new Repository<Product>(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orders.GetAllAsync(new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });

            return Ok(orders.Select(ToDto));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orders.GetByIdAsync(id, new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });

            if (order == null)
            {
                return NotFound();
            }

            return Ok(ToDto(order));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(OrderWriteDto dto)
        {
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                UserId = dto.UserId
            };

            foreach (var item in dto.Items)
            {
                var product = await _products.GetByIdAsync(item.ProductId, new QueryOptions<Product>());
                if (product == null)
                {
                    return BadRequest($"Product {item.ProductId} was not found.");
                }

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            order.TotalAmount = order.OrderItems.Sum(i => i.Price * i.Quantity);
            await _orders.AddAsync(order);

            var created = await _orders.GetByIdAsync(order.OrderId, new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });

            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, ToDto(created!));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderDto>> Update(int id, OrderWriteDto dto)
        {
            var existing = await _orders.GetByIdAsync(id, new QueryOptions<Order>
            {
                Includes = "OrderItems"
            });

            if (existing == null)
            {
                return NotFound();
            }

            existing.UserId = dto.UserId;
            existing.OrderItems.Clear();

            foreach (var item in dto.Items)
            {
                var product = await _products.GetByIdAsync(item.ProductId, new QueryOptions<Product>());
                if (product == null)
                {
                    return BadRequest($"Product {item.ProductId} was not found.");
                }

                existing.OrderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            existing.TotalAmount = existing.OrderItems.Sum(i => i.Price * i.Quantity);
            await _orders.UpdateAsync(existing);

            var updated = await _orders.GetByIdAsync(id, new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });

            return Ok(ToDto(updated!));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _orders.GetByIdAsync(id, new QueryOptions<Order>());
            if (existing == null)
            {
                return NotFound();
            }

            await _orders.DeleteAsync(id);
            return NoContent();
        }

        private static OrderDto ToDto(Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Items = (order.OrderItems ?? Array.Empty<OrderItem>()).Select(i => new OrderItemDto
                {
                    OrderItemId = i.OrderItemId,
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
