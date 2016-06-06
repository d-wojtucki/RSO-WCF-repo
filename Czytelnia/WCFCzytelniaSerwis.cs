using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Czytelnia
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFCzytelniaSerwis" in both code and config file together.
    public class WCFCzytelniaSerwis : IWCFCzytelniaSerwis
    {
        public static List<Book> booklist = new List<Book>();


        public String getInfoAboutBook(int userId, int bookId)
        {
            Console.WriteLine("Sending user " + userId + " info about book: " + bookId);
            return booklist.ElementAt(bookId - 1).getBookInfo().parseBookInfoToString();
        }

        public List<Book> listOfBorrowedItems(int userId)
        {
            Console.WriteLine("Sending user " + userId + "info about his borrowed books");
            List<Book> listOfBooks = new List<Book>();
            foreach(Book book in booklist) 
            {
                if (book.getBookInfo().getBorrowedStatus() && book.getBookInfo().getBorrowerId() == userId) listOfBooks.Add(book);
            }
            return listOfBooks;
        }
        
        List<Book> getBorrowedBooks(int userId, int wantedUserId)
        {
            Console.WriteLine("Sending user " + userId + "info about books borrowed by user " + wantedUserId);
            List<Book> listOfBooks = new List<Book>();
            foreach (Book book in booklist)
            {
                if (book.getBookInfo().getBorrowedStatus() && book.getBookInfo().getBorrowerId() == wantedUserId) listOfBooks.Add(book);
            }
            return listOfBooks;
        }
        
        String borrowBook(int userId, String returnDate, int bookId)
        {
            return booklist.ElementAt(bookId - 1).borrowBook(userId, returnDate);
        }

        public void initialize() 
        {
            BookInfo bi1 = new BookInfo("autor1", "tytul1", 1);
            BookInfo bi2 = new BookInfo("autor2", "tytul2", 2);
            BookInfo bi3 = new BookInfo("autor3", "tytul3", 3);
            BookInfo bi4 = new BookInfo("autor4", "tytul4", 3);

            Book book1 = new Book(1, bi1);
            Book book2 = new Book(2, bi2);
            Book book3 = new Book(3, bi3);
            Book book4 = new Book(4, bi4);

            booklist.Add(book1);
            booklist.Add(book2);
            booklist.Add(book3);
            booklist.Add(book4);
        }

        List<Book> IWCFCzytelniaSerwis.getBorrowedBooks(int userId, int wantedUserId)
        {
            throw new NotImplementedException();
        }

        string IWCFCzytelniaSerwis.borrowBook(int userId, string returnDate, int bookId)
        {
            throw new NotImplementedException();
        }
    }

}
       
    

    

