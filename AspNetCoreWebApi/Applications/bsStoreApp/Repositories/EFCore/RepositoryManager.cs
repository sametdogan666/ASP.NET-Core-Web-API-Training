using Repositories.Contracts;

namespace Repositories.EFCore;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IBookRepository> _bookRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_repositoryContext));
    }

    public IBookRepository Book => _bookRepository.Value;
    public async Task SaveAsync()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}

