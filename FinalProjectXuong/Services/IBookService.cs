using FinalProjectXuong.Models;

namespace FinalProjectXuong.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBookById(Guid id);
    }
}
