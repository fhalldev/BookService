using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BookService.Data
{
    public class BookRepository : IBookRepository
    {
        public IEnumerable<Book> GetAllBooks()
        {
            IEnumerable<Book> books = new List<Book>();
            SqlCommand sqlCommand = new SqlCommand();
            var sqlConnection = new SqlConnection("Server = franlaptop\\sqlexpress; Database = book_task; Trusted_Connection=True; TrustServerCertificate=True");
            DataSet dsDataSet = new DataSet();

            //set up SP
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "pr_get_all_books";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            var sqlAdapter = new SqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dsDataSet);

            //Declare DataTable for each table in DataSet
            DataTable dtBook = dsDataSet.Tables[0];

            foreach(DataRow row in dtBook.Rows)
            {
                var book = new Book((string)row["book_name"], (string)row["book_author"].ToString(), (Category)row["book_category"], (DateTime)row["book_registration_timestamp"], (string)row["book_description"]);
                book.BookId = (int)row["book_id"];
                books.Append(book);
            }

            return books;
        }

        public Book? GetBookById(int id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlParameter sqlParameter;
            var sqlConnection = new SqlConnection("Server = franlaptop\\sqlexpress; Database = book_task; Trusted_Connection=True; TrustServerCertificate=True");
            DataSet dsDataSet = new DataSet();

            //set up SP
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "pr_get_book_by_id";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            // add the parameters
            sqlParameter = sqlCommand.Parameters.Add("@BookID", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = id;

            var sqlAdapter = new SqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dsDataSet);

            //Declare DataTable for each table in DataSet
            DataTable dtBook = dsDataSet.Tables[0];

            if (dtBook.Rows.Count > 0) {
                var row = dtBook.Rows[0];
                var book = new Book((string)row["book_name"], (string)row["book_author"].ToString(), (Category)row["book_category"], (DateTime)row["book_registration_timestamp"], (string)row["book_description"]);
                book.BookId = (int)row["book_id"];

                return book;
            }

            return null;
        }

        public void UpdateBook(Book book) 
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlParameter sqlParameter;
            var sqlAdapter = new SqlDataAdapter();
            var sqlConnection = new SqlConnection("Server = franlaptop\\sqlexpress; Database = book_task; Trusted_Connection=True; TrustServerCertificate=True");

            //set up SP
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "pr_book_update";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            //Add Parameters
            sqlParameter = sqlCommand.Parameters.Add("@BookID", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = book.BookId;
            sqlParameter = sqlCommand.Parameters.Add("@BookName", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = book.Name;
            sqlParameter = sqlCommand.Parameters.Add("@BookAuthor", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = book.Author;
            sqlParameter = sqlCommand.Parameters.Add("@BookCategory", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = book.Category;
            sqlParameter = sqlCommand.Parameters.Add("@BookDescription", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = book.Description;
            sqlParameter = sqlCommand.Parameters.Add("@RegistrationTimestamp", SqlDbType.DateTime);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = book.RegistrationTimestamp;

            //Execute SP
            sqlAdapter.SelectCommand = sqlCommand;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
