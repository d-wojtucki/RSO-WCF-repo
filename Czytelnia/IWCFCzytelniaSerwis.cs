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
        [FaultContract(typeof(MyFault))]
        String getInfoAboutBook(int userId, int bookId);

        [OperationContract]
        [FaultContract(typeof(MyFault))]
        List<Book> listOfBorrowedItems(int userId);

        [OperationContract]
        [FaultContract(typeof(MyFault))]
        List<Book> getBorrowedBooks(int userId, int wantedUserId);

        [OperationContract]
        [FaultContract(typeof(MyFault))]
        String borrowBook(int userId, String returnDate, int bookId);

    }

    [DataContract]
    public class Book
    {
        [DataMember]
        public int bookId;

        [DataMember]
        public String author;

        [DataMember]
        public String title;

        [DataMember]
        public Boolean isBorrowed { get; set; }

        [DataMember]
        public String returnDate { get; set;}

        [DataMember]
        public int idOfBorrower { get; set; }

        public Book(int id, String author, String title)
        {
            this.returnDate = "";
            this.bookId = id;
            this.author = author;
            this.title = title;
            this.isBorrowed = false;
            this.idOfBorrower = 0;
        }

        public BookInfo getBookInfo()
        {
            BookInfo bookInfo = new BookInfo(author, title, bookId, isBorrowed, returnDate, idOfBorrower);
            return bookInfo;
        }

        [OperationContract]
        public String getStringBookInfo()
        {
            BookInfo bookInfo = getBookInfo();

            return bookInfo.parseBookInfoToString();
        }
    
        [OperationContract]
        public String borrowBook(int userId, String returnDate)
        {
            //Console.WriteLine("Some data");
            if (isBorrowed) return "Sorry, that book is not available for borrowing.";
            else
            {
                this.returnDate = returnDate;
                this.idOfBorrower = userId;
                this.isBorrowed = true;
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

        public BookInfo(String author, String title, int bookId, Boolean isBorrowed, String returnDate, int idOfBorrower)
        {
            this.bookId = bookId;
            this.author = author;
            this.title = title;
            this.isBorrowed = isBorrowed;
            this.returnDate = returnDate;
            this.idOfBorrower = idOfBorrower;
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
    }

    [DataContract]
    public class MyFault
    {
        [DataMember]
        public String Issue { get; set; }

        [DataMember]
        public String Details { get; set;}

        [DataMember]
        public String Message { get; set;}
    }
}
