using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Interfaces
{
    public interface ILibraryRepository
    {
        void Add(LibraryItem item);
        bool Exists(int itemId);
        LibraryItem? GetById(int id);
        List<LibraryItem> GetAll();
        List<LibraryItem> SearchByTitleOrAuthor(string query);
        public int GetNextId();
    }
}