﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Conracts;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
    {
        var books = await _repositoryManager.Book.GetAllBooksAsync(trackChanges);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
    {
        var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
    {
        var entity = _mapper.Map<Book>(bookDto);
        _repositoryManager.Book.CreateOneBook(entity);
        await _repositoryManager.SaveAsync();
        return _mapper.Map<BookDto>(entity);
    }

    public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDtoForUpdate, bool trackChanges)
    {
        //check entity
        var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

        entity = _mapper.Map<Book>(bookDtoForUpdate);

        _repositoryManager.Book.UpdateOneBook(entity);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteOneBookAsync(int id, bool trackChanges)
    {
        var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

        _repositoryManager.Book.DeleteOneBook(entity);
        await _repositoryManager.SaveAsync();
    }

    public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
    {
        var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

        var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);

        return (bookDtoForUpdate, book);

    }

    public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
    {
        _mapper.Map(bookDtoForUpdate, book);
        await _repositoryManager.SaveAsync();
    }

    private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
    {
        //check entity
        var entity = await _repositoryManager.Book.GetOneBookByIdAsync(id, trackChanges);
        if (entity is null)
        {
            throw new BookNotFoundException(id);
        }

        return entity;
    }
}

