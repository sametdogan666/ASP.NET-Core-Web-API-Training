using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Conracts
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        Book GetOneBookById(int id, bool trackChanges);
        Book CreateOneBook(Book  book);
        void UpdateOneBook(int id, BookDtoForUpdate bookDtoForUpdate, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
    }
}
