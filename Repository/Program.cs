using Models;

namespace Repository
{
    public class LibraryService
    {

        private readonly IRepository _bookRepo;

        public LibraryService(IRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public bool BorrowBook(int bookId)
        {
            var book = _bookRepo.GetById(bookId);
            if (book == null || !book.IsAvailable)
            {
                return false;
            }

            book.IsAvailable = false;
            _bookRepo.Update(book);
            return true;
        }
    }
}
