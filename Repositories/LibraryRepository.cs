using UniversitetSystem.Data;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Domain.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        public void Add(LibraryItem item)
        {
            Database.LibraryItems.Add(item);
        }

        public int GetNextId()
        {
            return Database.NextLibraryItemId();
        }

        public List<LibraryItem> GetAll()
        {
            return Database.LibraryItems.ToList();
        }

        public List<LibraryItem> Search(string query)
        {
            return Database.LibraryItems
                .Where(l => l.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            l.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public LibraryItem? GetById(int id)
        {
            return Database.LibraryItems.FirstOrDefault(i => i.Id == id);
        }

        public bool Exists(int itemId)
        {
            return Database.LibraryItems.Any(i => i.Id == itemId);
        }

        public List<LibraryItem> SearchByTitleOrAuthor(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<LibraryItem>();

            return Database.LibraryItems
                .Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || item.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}