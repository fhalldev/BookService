CREATE PROCEDURE dbo.[pr_book_update]
    @BookID int = 0,
    @BookName varchar(MAX) = '',
    @BookAuthor varchar(MAX) = '',
    @BookCategory int = 0,
    @BookDescription varchar(MAX) = '',
    @RegistrationTimestamp datetime = GETDATE
AS
    IF NOT EXISTS (SELECT 1 FROM book WHERE book_id = @BookID)
    BEGIN
        -- Insert a new book
        INSERT INTO book (book_name, book_author, book_category, book_description, book_registration_timestamp)
        VALUES (@BookName, @BookAuthor, @BookCategory, @BookDescription, GETDATE());
    END

    ELSE
    BEGIN
        -- Update an existing book
        UPDATE book
        SET book_name = @BookName,
            book_author = @BookAuthor,
            book_category = @BookCategory,
            book_description = @BookDescription
        WHERE book_id = @BookID;
    END
