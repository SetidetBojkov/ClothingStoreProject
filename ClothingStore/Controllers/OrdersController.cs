using ClothingStore.Data;
using ClothingStore.Helpers;
using ClothingStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string CartSessionKey = "Cart";

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: /Orders/Create
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey);
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var user = await _userManager.GetUserAsync(User);

            var order = new Order
            {
                UserId = user.Id,
                Items = cart.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList(),
                Status = OrderStatus.Pending
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove(CartSessionKey); // изчистване на количката
            return RedirectToAction("Mine");
        }

        // GET: /Orders/Mine
        public async Task<IActionResult> Mine()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.UserId == user.Id)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: /Orders/All – само за Admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> All(string status = null, DateTime? from = null, DateTime? to = null)
        {
            var ordersQuery = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<OrderStatus>(status, out var parsedStatus))
            {
                ordersQuery = ordersQuery.Where(o => o.Status == parsedStatus);
            }

            if (from.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.OrderDate >= from);
            }

            if (to.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.OrderDate <= to);
            }

            var orders = await ordersQuery.OrderByDescending(o => o.OrderDate).ToListAsync();
            ViewBag.Statuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList();
            return View("All", orders);
        }

        // POST: /Orders/ChangeStatus – само за Admin
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction("All");
        }
    }
}
