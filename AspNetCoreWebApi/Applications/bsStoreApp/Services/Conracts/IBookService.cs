using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Conracts;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges);
    Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
    Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto);
    Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDtoForUpdate, bool trackChanges);
    Task DeleteOneBookAsync(int id, bool trackChanges);
    Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);

    Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book);
}

