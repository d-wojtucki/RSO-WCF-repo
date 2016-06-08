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
            //iwcfCzytelniaSerwis.initialize();
            Console.WriteLine("joł ja jestem klijentem");
            Console.WriteLine("Podaj swoje ID: ");
            int userId = Int32.Parse(Console.ReadLine());
            Boolean exitFlag = false;
            do
            {
                Console.WriteLine("Menu!");
                Console.WriteLine("1: Info o pozyczonych ksiazkach");
                Console.WriteLine("2: Info o ksiazkach pozyczonych przez kogos innego");
                Console.WriteLine("3: Info o ksiazce");
                Console.WriteLine("4: Wypozycz ksiazke");
                Console.WriteLine("0: Wyjdz");
                int switcher = Int32.Parse(Console.ReadLine());
                switch (switcher)
                {
                    case 1:
                        Console.WriteLine("Wybrales 1. \nWysylanie zapytania...");
                        List<Book> listaMoichKsiazek = new List<Book>();
                        try
                        {
                            listaMoichKsiazek = iwcfCzytelniaSerwis.listOfBorrowedItems(userId);
                        }
                        catch (FaultException faultException)
                        {
                            Console.WriteLine(faultException.Message);
                        }
                        foreach (Book book in listaMoichKsiazek) Console.WriteLine(book.getStringBookInfo());
                        break;

                    case 2:
                        Console.WriteLine("Wybrales 2. \nWysylanie zapytania...");
                        Console.WriteLine("Podaj id: ");
                        int hisId = Int32.Parse(Console.ReadLine());
                        List<Book> listaCzyichsKsiazek = new List<Book>();
                        try
                        {
                            listaCzyichsKsiazek = iwcfCzytelniaSerwis.getBorrowedBooks(userId, hisId);
                        }
                        catch (FaultException faultException)
                        {
                            Console.WriteLine(faultException.Message);
                        }
                        foreach (Book book in listaCzyichsKsiazek) Console.WriteLine(book.getStringBookInfo());
                        break;

                    case 3:
                        Console.WriteLine("Wybrales 3. \nWysylanie zapytania");
                        Console.WriteLine("Podaj id ksiazki: ");
                        int bookId = Int32.Parse(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(iwcfCzytelniaSerwis.getInfoAboutBook(userId, bookId));
                        }
                        catch (FaultException faultException)
                        {
                            Console.WriteLine(faultException.Message);
                        }
                        break;

                    case 4:
                        Console.WriteLine("Wybrales 4. \nWysylanie zapytania");
                        Console.WriteLine("Podaj id ksiazki: ");
                        String sth = Console.ReadLine();
                        int bookIdYouWantToBorrow = Int32.Parse(sth);
                        Console.WriteLine("Podaj czas oddania ksiazki: ");
                        String returnDate = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(iwcfCzytelniaSerwis.borrowBook(userId, returnDate, bookIdYouWantToBorrow));
                        }
                        catch (FaultException faultException)
                        {
                            Console.WriteLine(faultException.Message);
                        }
                        break;

                    case 0:
                        Console.WriteLine("Nastapi wyjscie, wcisnij <Enter> aby potwierdzić");
                        Console.ReadLine();
                        exitFlag = true;
                        break;

                    default:
                        Console.WriteLine("oops, you chose wrong number! Try again!");
                        break;
                }
            } while (!exitFlag);
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
