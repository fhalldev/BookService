using Microsoft.AspNetCore.Mvc;
using BookService.Data;

namespace BookService.BookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository = new BookRepository();

        // GET /api/book/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_bookRepository.GetAllBooks());
        }

        // POST /api/book/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateBook(Book book)
        {
            book.BookId = 0;
            _bookRepository.UpdateBook(book);

            return Ok(book);
        }

        // PUT /api/book/{id}/update
        [HttpPut("{id}/update")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var existingBook = _bookRepository.GetBookById(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            _bookRepository.UpdateBook(book);

            return Ok(book);
        }
    }
}