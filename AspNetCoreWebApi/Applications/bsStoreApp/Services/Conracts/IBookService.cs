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
        BookDto GetOneBookById(int id, bool trackChanges);
        BookDto CreateOneBook(BookDtoForInsertion bookDto);
        void UpdateOneBook(int id, BookDtoForUpdate bookDtoForUpdate, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
        (BookDtoForUpdate bookDtoForUpdate, Book book) GetOneBookForPatch(int id, bool trackChanges);

        void SaveChangesForPatch(BookDtoForUpdate bookDtoForUpdate, Book book);
    }
}
