using UniversitetSystem.Domain.Library;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Tests.Repositories
{
    public class InMemoryLibraryRepository : ILibraryRepository
    {
        private readonly List<LibraryItem> _items = new();

        public void Add(LibraryItem item) => _items.Add(item);

        public bool Exists(int itemId) => _items.Any(i => i.Id == itemId);

        public List<LibraryItem> GetAll() => _items;

        public LibraryItem? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);

        public int GetNextId() => _items.Count == 0 ? 1 : _items.Max(i => i.Id) + 1;

        public List<LibraryItem> SearchByTitleOrAuthor(string query) =>
            _items.Where(i => i.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                              i.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                  .ToList();
    }
}