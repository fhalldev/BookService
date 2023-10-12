CREATE PROCEDURE [dbo].[pr_get_book_by_id]
    @BookID int = 0
AS
    SELECT * from book where book_id = @BookID
