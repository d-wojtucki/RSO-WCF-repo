using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Czytelnia
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFCzytelniaSerwis" in both code and config file together.
    [ServiceContract]
    public interface IWCFCzytelniaSerwis
    { 

        [OperationContract]
        String getInfoAboutBook(int userId, int bookId);

        [OperationContract]
        List<Book> listOfBorrowedItems(int userId);

        [OperationContract]
        List<Book> getBorrowedBooks(int userId, int wantedUserId);

        [OperationContract]
        String borrowBook(int userId, String returnDate, int bookId);

        [OperationContract]
        void initialize();
    }

    [DataContract]
    public class Book
    {
        [DataMember]
        public int bookId;

        [DataMember]
        public BookInfo bookInfo;

        [DataMember]
        public String returnDate;
        
        public Book(int id, BookInfo bookInfo)
        {
            
        }

        [OperationContract]
        public BookInfo getBookInfo()
        {
            return this.bookInfo;
        }
    
        [OperationContract]
        public String borrowBook(int userId, String returnDate)
        {
            if (getBookInfo().isBorrowed) return "Sorry, that book is not available for borrowing.";
            else
            {
                getBookInfo().setBorrowedStatus(userId, returnDate);
                return "Borrowed!";
            }
        }

    }

    [DataContract]
    public class BookInfo
    {
        [DataMember]
        public int bookId;

        [DataMember]
        public String author;

        [DataMember]
        public String title;

        [DataMember]
        public Boolean isBorrowed;

        [DataMember]
        public String returnDate;

        [DataMember]
        public int idOfBorrower;

        public BookInfo(String author, String title, int bookId)
        {
            this.bookId = bookId;
            this.author = author;
            this.title = title;
            this.isBorrowed = false;
            this.returnDate = "";
            this.idOfBorrower = 0;
        }

        [OperationContract]
        public String parseBookInfoToString()
        {
            String body = "Book id: " + bookId + "\nAuthor: " + author + "\nTitle: " + title + "\n\n";
            String state;
            if (isBorrowed)
            {
                state = "Book borrowed by user with id = " + idOfBorrower + " until " + returnDate + ".";
            }
            else
            {
                state = "Book is available for borrowing.";
            }
            return body + state;
        }
        [OperationContract]
        public void setBorrowedStatus(int userId, String date)
        {
            this.isBorrowed = true;
            this.returnDate = date;
            this.idOfBorrower = userId;
        }
        [OperationContract]
        public Boolean getBorrowedStatus()
        {
            return isBorrowed;
        }
        [OperationContract]
        public int getBorrowerId()
        {
            if(isBorrowed)
            {
                return idOfBorrower;
            }
            else
            {
                return 0;
            }
        }
    }
}
