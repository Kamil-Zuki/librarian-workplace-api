using librarian_workplace_DAL.EF;
using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces.TypeInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore;
namespace librarian_workplace_DAL.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context) { }

        public IEnumerable<Book> ExtendedGet(Func<Book, bool> predicate)
        {
            return context.Books.Include(rb => rb.ReaderBooks).Where(predicate)
                .Select(readerBook => new Book()
                {
                    ArticleNumber = readerBook.ArticleNumber,
                    InsertDate = readerBook.InsertDate,
                    UpdateDate = readerBook.UpdateDate,
                    Author = readerBook.Author,
                    PublicationDate = readerBook.PublicationDate,
                    InstancesNumber = readerBook.InstancesNumber
                });
        }
    }
}