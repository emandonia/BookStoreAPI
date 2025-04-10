using BookStoreAPI.Models;

namespace BookStoreAPI.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
    }
}
