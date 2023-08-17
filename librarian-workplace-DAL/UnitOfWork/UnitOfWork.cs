using librarian_workplace_DAL.EF;
using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces;
using librarian_workplace_DAL.Interfaces.TypeInterfaces;
using librarian_workplace_DAL.Repositories;

namespace librarian_workplace_DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryContext context;
        public UnitOfWork(LibraryContext context)
        {
            this.context = context;
            Reader = new ReaderRepository(this.context);
            Book = new BookRepository(this.context);
            ReaderBook = new ReaderBookRepository(this.context);
        }
        public IReaderRepository Reader
        {
            get;
            private set;
        }
        public IBookRepository Book
        {
            get;
            private set;
        }

        public IReaderBookRepository ReaderBook 
        { 
            get; 
            private set; 
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
