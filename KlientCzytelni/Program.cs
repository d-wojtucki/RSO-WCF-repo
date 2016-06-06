using Czytelnia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace KlientCzytelni
{
    class Program
    {
        static IWCFCzytelniaSerwis iwcfCzytelniaSerwis;
        static void Main(string[] args)
        {
            ChannelFactory<IWCFCzytelniaSerwis> channelFactory = new ChannelFactory<IWCFCzytelniaSerwis>("EndpointCzytelnia");
            iwcfCzytelniaSerwis = channelFactory.CreateChannel();
            iwcfCzytelniaSerwis.initialize();
            Console.WriteLine("joł ja jestem klijentem");
            Console.WriteLine("Podaj swoje ID: ");
            int userId = Int32.Parse(Console.ReadLine());
            while(true)
            {
                Console.WriteLine("Menu!");
                Console.WriteLine("1: Info o pozyczonych ksiazkach");
                Console.WriteLine("2: Info o ksiazkach pozyczonych przez kogos innego");
                Console.WriteLine("3: Info o ksiazce");
                Console.WriteLine("4: Wypozycz ksiazke");
                int switcher = Int32.Parse(Console.ReadLine());
                switch(switcher)
                {
                    case 1:
                        Console.WriteLine("Wybrales 1. \nWysylanie zapytania...");
                        List<Book> listaMoichKsiazek = iwcfCzytelniaSerwis.listOfBorrowedItems(userId);
                        foreach (Book book in listaMoichKsiazek) Console.WriteLine(book.getBookInfo().parseBookInfoToString());
                        break;

                    case 2:
                        Console.WriteLine("Wybrales 2. \nWysylanie zapytania...");
                        Console.WriteLine("Podaj id: ");
                        int hisId = Int32.Parse(Console.ReadLine());
                        List<Book> listaCzyichsKsiazek = iwcfCzytelniaSerwis.getBorrowedBooks(userId, hisId);
                        foreach (Book book in listaCzyichsKsiazek) Console.WriteLine(book.getBookInfo().parseBookInfoToString());
                        break;

                    case 3:
                        Console.WriteLine("Wybrales 3. \nWysylanie zapytania");
                        Console.WriteLine("Podaj id ksiazki: ");
                        int bookId = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(iwcfCzytelniaSerwis.getInfoAboutBook(userId, bookId));
                        break;

                    case 4:
                        Console.WriteLine("Wybrales 4. \nWysylanie zapytania");
                        Console.WriteLine("Podaj id ksiazki: ");
                        int bookIdYouWantToBorrow = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Podaj czas oddania ksiazki: ");
                        String returnDate = Console.ReadLine();
                        Console.WriteLine(iwcfCzytelniaSerwis.borrowBook(userId, returnDate, bookIdYouWantToBorrow));
                        break;

                    default:
                        Console.WriteLine("oops, you chose wrong number! Try again!");
                        break;
                }   
            }
        }

        private String parseListToStringForm (List<Book> booklist)
        {
            String result = "";
            foreach(Book book in booklist)
            {
                result += book.getBookInfo().parseBookInfoToString() + "\n\n\n";
            }

            return result;
        }
    }
}
