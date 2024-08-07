using FinalProjectXuong.Models;
using FinalProjectXuong.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectXuong.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Cart()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(userId));
            var cartItems = _cartService.GetCartItems(cart.Id);
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid bookId, int quantity)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "User");
            }
            try
            {
                _cartService.AddToCart(Guid.Parse(userId), bookId, quantity);
                TempData["Message"] = $"{quantity} books have been added to your cart.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Details", "Book", new { id = bookId });
        }

        [HttpPost]
        public IActionResult UpdateCartItem(Guid cartItemId, int quantity)
        {
            try
            {
                _cartService.UpdateCartItem(cartItemId, quantity);
                TempData["Message"] = "Cart item updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveCartItem(Guid cartItemId)
        {
            _cartService.RemoveCartItem(cartItemId);
            TempData["Message"] = "Cart item removed successfully.";
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(userId));
            if (cart != null)
            {
                _cartService.ClearCart(cart.Id);
                TempData["Message"] = "Cart cleared successfully.";
            }
            return RedirectToAction("Cart");
        }
    }
}
