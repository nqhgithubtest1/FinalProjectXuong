using FinalProjectXuong.Models;
using FinalProjectXuong.Repositories;

namespace FinalProjectXuong.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookRepository;
        public BookService(IGenericRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetBookById(Guid id)
        {
            return _bookRepository.GetById(id);
        }
    }
}
