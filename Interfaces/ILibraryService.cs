using UniversitetSystem.Common;
using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Interfaces
{
    public interface ILibraryService
    {
        Result AddLibraryItem(LibraryItem item);
        Result<List<LibraryItem>> Search(string query);
        Result<List<LibraryItem>> GetAll();
        public int GetNextLibraryItemId();
    }
}