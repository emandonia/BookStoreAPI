using BookStoreAPI.Dtos;
using BookStoreAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Editor,Viewer")]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Editor,Viewer")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _service.GetByIdAsync(id);
            return book is null ? NotFound() : Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Created("", result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Update(int id, UpdateBookDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
