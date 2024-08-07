using FinalProjectXuong.Models;

namespace FinalProjectXuong.Services
{
    public interface IUserService
    {
        User Register(User user);
        User Login(string username, string password);
    }
}
