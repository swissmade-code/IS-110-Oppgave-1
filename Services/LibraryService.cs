using UniversitetSystem.Common;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibraryService(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public Result AddLibraryItem(LibraryItem item)
        {
            if (_libraryRepository.Exists(item.Id))
                return Result.Fail("Item with this ID already exists.");

            _libraryRepository.Add(item);
            return Result.Ok();
        }

        public Result<List<LibraryItem>> GetAll()
        {
            var items = _libraryRepository.GetAll();

            if (items == null || !items.Any())
                return Result<List<LibraryItem>>.Fail("No library items found.");

            return Result<List<LibraryItem>>.Ok(items);
        }

        public Result<List<LibraryItem>> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Result<List<LibraryItem>>.Fail("Search query cannot be empty.");

            var results = _libraryRepository.SearchByTitleOrAuthor(query);

            if (results == null || !results.Any())
                return Result<List<LibraryItem>>.Fail("No matching library items found.");

            return Result<List<LibraryItem>>.Ok(results);
        }

        public int GetNextLibraryItemId()
        {
            return _libraryRepository.GetNextId();
        }
    }
}