using FinalProjectXuong.Models;
using FinalProjectXuong.Repositories;

namespace FinalProjectXuong.Services
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<CartItem> _cartItemRepository;
        private readonly IGenericRepository<Book> _bookRepository;
        public CartService(IGenericRepository<Cart> cartRepository,
            IGenericRepository<CartItem> cartItemRepository,
            IGenericRepository<Book> bookRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
        }

        public void AddToCart(Guid userId, Guid bookId, int quantity)
        {
            var cart = GetCartFromUserId(userId);
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (cartItem != null)
            {
                if (cartItem.Quantity + quantity > cartItem.Book.Quantity)
                {
                    throw new Exception($"Cannot add more than {cartItem.Book.Quantity} copies of {cartItem.Book.Title}.");
                }
                cartItem.Quantity += quantity;
                _cartItemRepository.Update(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    BookId = bookId,
                    Quantity = quantity,
                    Book = _bookRepository.GetById(bookId),
                    Cart = _cartRepository.GetById(cart.Id)
                };
                if (quantity > cartItem.Book.Quantity)
                {
                    throw new Exception($"Cannot add more than {cartItem.Book.Quantity} copies of {cartItem.Book.Title}.");
                }
                _cartItemRepository.Insert(cartItem);
            }
            _cartItemRepository.Save();
        }

        public void ClearCart(Guid cartId)
        {
            var cart = _cartRepository.GetById(cartId);
            if (cart != null)
            {
                foreach (var cartItem in cart.CartItems)
                {
                    _cartItemRepository.Delete(cartItem);
                }
                _cartItemRepository.Save();
            }
        }

        public Cart GetCartFromUserId(Guid userId)
        {
            return _cartRepository.GetAll().FirstOrDefault(c => c.UserId == userId);
        }

        public List<CartItem> GetCartItems(Guid cartId)
        {
            var cartItems = _cartItemRepository.GetAll()
                .Where(ci => ci.CartId == cartId).ToList();
            if (cartItems != null)
            {
                return cartItems;
            }
            else
            {
                return new List<CartItem>();
            }
        }

        public void RemoveCartItem(Guid cartItemId)
        {
            var cartItem = _cartItemRepository.GetById(cartItemId);
            if (cartItem != null)
            {
                _cartItemRepository.Delete(cartItem);
                _cartItemRepository.Save();
            }
        }

        public void UpdateCartItem(Guid cartItemId, int quantity)
        {
            var cartItem = _cartItemRepository.GetById(cartItemId);
            if (quantity > cartItem.Book.Quantity)
            {
                throw new Exception($"Cannot add more than {cartItem.Book.Quantity} copies of {cartItem.Book.Title}.");
            }
            cartItem.Quantity = quantity;
            _cartItemRepository.Update(cartItem);
            _cartItemRepository.Save();
        }
    }
}
