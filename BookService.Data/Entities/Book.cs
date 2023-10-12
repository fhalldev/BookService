namespace BookService.Data
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public Category Category { get; set; }
        public DateTime RegistrationTimestamp { get; set; }
        public string Description { get; set; }

        public Book(string name, string author, Category category, DateTime registrationTimestamp, string description)
        {
            this.BookId = 0;
            this.Name = name;
            this.Author = author;
            this.Category = category;
            this.RegistrationTimestamp = registrationTimestamp;
            this.Description = description;
        }
    }

    public enum Category
    {
        Thriller,
        History,
        Drama,
        Biography
    }
}