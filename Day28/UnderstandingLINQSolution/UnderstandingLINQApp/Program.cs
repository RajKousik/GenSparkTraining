using UnderstandingLINQApp.Model;

namespace UnderstandingLINQApp
{
    internal class Program
    {
        void PrintTheBooksPublisherwise_2()
        {
            using pubsContext context = new pubsContext();

            var books = context.Titles
                .GroupBy(t => t.PubId)
                .Select(g => new
                {
                    PublisherId = g.Key,
                    Titles = g.Select(t => t.Title1).ToList()
                });

            foreach (var bookGroup in books)
            {
                Console.WriteLine($"PublisherId: {bookGroup.PublisherId}");

                foreach (var title in bookGroup.Titles)
                {
                    Console.WriteLine($"\tTitle: {title}");
                }
            }
        }

        void PrintTheBooksPublisherwise_1()
        {
            pubsContext context = new pubsContext();
            var books = context.Titles
                        .GroupBy(t => t.PubId, t => t.Pub, (pid, title) => new { Key = pid, TitleCount = title.Count() });

            foreach (var book in books)
            {
                Console.Write(book.Key);
                Console.WriteLine(" - " + book.TitleCount);
            }
        }

        void PrintTheBooksPublisherwise_3()
        {
            pubsContext context = new pubsContext();
            var books = context.Titles
                        .GroupBy(t => t.PubId, t => t.Title1, (pid, title) => new { Key = pid, TitleCount = title.Count(), Titles = title.ToList() });

            foreach (var book in books)
            {
                Console.Write(book.Key);
                Console.WriteLine(" - " + book.TitleCount);
                foreach (var title in book.Titles)
                    Console.WriteLine(" - " + title);
            }
        }

        void PrintNumberOfBooksFromType(string type)
        {
            pubsContext context = new pubsContext();
            var bookCount = context.Titles.Where(t => t.Type == type);
            Console.WriteLine($"There are {bookCount.Count()} in the type \"{type}\"");
        }
        void PrintAuthorNames()
        {
            pubsContext context = new pubsContext();
            var authors = context.Authors;
            foreach (var author in authors)
            {
                Console.WriteLine(author.AuFname + " " + author.AuLname);
            }
        }

        void PrintTheBooksPulisherwise_traditional_method()
        {
            pubsContext context = new pubsContext();
            var books = context.Titles
                        .GroupBy(t => t.PubId)
                        .Select(t => new
                        {
                            PublisherId = t.Key,
                            TitleCount = t.Count(),
                            Titles = t.Select(t => new
                            {
                                BookName = t.Title1,
                                BookPrice = t.Price
                            })
                        });

            foreach (var book in books)
            {
                Console.Write(book.PublisherId);
                Console.WriteLine(" - " + book.TitleCount);
                foreach (var title in book.Titles)
                {
                    Console.WriteLine("\t" + title.BookName + " " + title.BookPrice);
                }
            }
        }


        void PrintTitleAndSaleOrderQauntity()
        {
            pubsContext context = new pubsContext();
            var sales = context.Sales
                        .GroupBy(s => s.TitleId, s => s, (tid, sale) => new { Id = tid, OrderDetials = sale.ToList() });

            foreach (var sale in sales)
            {
                Console.WriteLine($"Title Id is {sale.Id}");
                foreach (var saleDetail in sale.OrderDetials)
                {
                    Console.WriteLine("    Quantity - " + saleDetail.Qty);
                    Console.WriteLine("    Order No - " + saleDetail.OrdNum);
                }
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.PrintAuthorNames();
            //program.PrintNumberOfBooksFromType("mod_cook");
            //program.PrintTheBooksPublisherwise_1();
            //program.PrintTheBooksPublisherwise_2();
            //program.PrintTheBooksPublisherwise_3();

            //program.PrintTheBooksPulisherwise_traditional_method();
            program.PrintTitleAndSaleOrderQauntity();
        }
    }
}
