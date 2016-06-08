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

        public WCFCzytelniaSerwis()
        {
            Book book1 = new Book(1, "author1", "title1");
            Book book2 = new Book(2, "author2", "title2");
            Book book3 = new Book(3, "author3", "title3");
            Book book4 = new Book(4, "author4", "title4");

            Console.WriteLine(book2.borrowBook(123, "someday"));

            booklist.Add(book1);
            booklist.Add(book2);
            booklist.Add(book3);
            booklist.Add(book4);
        }

        public String getInfoAboutBook(int userId, int bookId)
        {
            Console.WriteLine("Sending user " + userId + " info about book: " + bookId);
            //Console.WriteLine(booklist.ElementAt<Book>(bookId).getBookInfo().parseBookInfoToString());
            String info = "";
            /*BookInfo bookInfo = new BookInfo("author", "title", 1);
            Console.WriteLine(bookInfo.parseBookInfoToString());
            Book newBook = new Book(1, bookInfo);
            Console.WriteLine("stworzono booka");
            Console.WriteLine(newBook.returnDate);
            Console.WriteLine(newBook.getStringBookInfo());
            booklist.Add(newBook);
            Book bookFound = booklist.ElementAt<Book>(bookId);
            if (bookFound.Equals(newBook)) Console.WriteLine("Found!");
            Console.WriteLine(bookFound.getStringBookInfo());
            */
            //Console.WriteLine(booklist.ElementAt<Book>(0).getBookInfo().parseBookInfoToString());
            try
            {
                info = booklist.ElementAt(bookId-1).getStringBookInfo();
            }
            catch 
            {
                throw new FaultException("There was a problem");
            }
            return info;
        }

        public List<Book> listOfBorrowedItems(int userId)
        {
            Console.WriteLine("Sending user " + userId + "info about his borrowed books");
            List<Book> listOfBooks = new List<Book>();
            try
            {
                foreach (Book book in booklist)
                {
                    if (book.isBorrowed && book.idOfBorrower == userId) listOfBooks.Add(book);
                }
            }
            catch
            {
                throw new FaultException("There was a problem");
            }
            return listOfBooks;
        }
        
        List<Book> getBorrowedBooks(int userId, int wantedUserId)
        {
            Console.WriteLine("Sending user " + userId + "info about books borrowed by user " + wantedUserId);
            List<Book> listOfBooks = new List<Book>();
            try
            {
                foreach (Book book in booklist)
                {
                    if (book.isBorrowed && book.idOfBorrower == wantedUserId) listOfBooks.Add(book);
                }
            }
            catch
            {
                throw new FaultException("There was a problem");
            }
            return listOfBooks;
        }
        
        String borrowBook(int userId, String returnDate, int bookId)
        {
            String state = "";
            try
            {
               state = booklist.ElementAt(bookId - 1).borrowBook(userId, returnDate);
            }
            catch
            {
                throw new FaultException("There was a problem");
            }
            return state;
        }

        /*public void initialize() 
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
        }*/

        List<Book> IWCFCzytelniaSerwis.getBorrowedBooks(int userId, int wantedUserId)
        {
            throw new NotImplementedException();
        }

        string IWCFCzytelniaSerwis.borrowBook(int userId, string returnDate, int bookId)
        {
            throw new NotImplementedException();
        }

        public void initialize()
        {
            throw new NotImplementedException();
        }
    }

}
       
    

    

