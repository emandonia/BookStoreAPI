using AutoMapper;
using BookStoreAPI.Dtos;
using BookStoreAPI.Interfaces;
using BookStoreAPI.Models;

namespace BookStoreAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _repo.AddAsync(book);
            await _repo.SaveAsync();
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateAsync(int id, UpdateBookDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            await _repo.SaveAsync();

            return _mapper.Map<BookDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _repo.Delete(existing);
            await _repo.SaveAsync();
            return true;
        }
    }
}
