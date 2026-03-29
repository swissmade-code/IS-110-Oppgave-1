using UniversitetSystem.Domain.Exceptions;
using UniversitetSystem.Enums;

namespace UniversitetSystem.Domain.Library
{
    public class LibraryItem
    {
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public int Year { get; }
        public int TotalCopies { get; }
        public int AvailableCopies { get; private set; }
        public MediaType Type { get; }

        public bool IsAvailable => AvailableCopies > 0;

        public LibraryItem(int id, string title, string author, int year, int totalCopies, MediaType type)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
            Type = type;
        }

        public void Borrow()
        {
            if (!IsAvailable)
            {
                throw new LibraryItemUnavailableException($"No copies of '{Title}' are available to borrow.");
            }

            AvailableCopies--;
        }

        public void Return()
        {
            if (AvailableCopies >= TotalCopies)
            {
                throw new LibraryItemAlreadyReturnedException(
                                   $"Cannot return '{Title}': all copies are already in the library.");
            }

            AvailableCopies++;
        }
    }
}