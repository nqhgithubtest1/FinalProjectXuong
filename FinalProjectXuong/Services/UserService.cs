using FinalProjectXuong.Models;
using FinalProjectXuong.Repositories;

namespace FinalProjectXuong.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Cart> _cartRepository;

        public UserService(IGenericRepository<User> userRepository,
            IGenericRepository<Cart> cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public User Login(string username, string password)
        {
            return _userRepository.GetAll()
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public User Register(User user)
        {
            if (_userRepository.FindBy(u => u.Username == user.Username) != null)
            {
                throw new Exception("Username already exists.");
            }

            _userRepository.Insert(user);
            _userRepository.Save();

            var cart = new Cart()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id
            };

            _cartRepository.Insert(cart);
            _cartRepository.Save();

            return user;
        }
    }
}
