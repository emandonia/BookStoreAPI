using BookStoreAPI.Data;
using BookStoreAPI.Interfaces;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repositories
{
    public class BookRepository :
     IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task AddAsync(Book entity) => await _context.Books.AddAsync(entity);

        public void Update(Book entity) => _context.Books.Update(entity);

        public void Delete(Book entity) => _context.Books.Remove(entity);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author) =>
            await _context.Books.Where(b => b.Author == author).ToListAsync();
    }

}
