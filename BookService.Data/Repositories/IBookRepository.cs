namespace BookService.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        void UpdateBook(Book task);
        Book? GetBookById(int id);
    }
}
