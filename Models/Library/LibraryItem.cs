using UniversitetSystem.Enums;

namespace UniversitetSystem.Models.Library
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
                throw new InvalidOperationException("No copies available.");
            }

            AvailableCopies--;
        }

        public void Return()
        {
            if (AvailableCopies >= TotalCopies)
            {
                Console.WriteLine("Warning: all copies are already in the library.");
                return;
            }

            AvailableCopies++;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Year: {Year}");
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Available: {AvailableCopies}/{TotalCopies}");
            Console.WriteLine(IsAvailable ? "Status: Available" : "Status: Not available");
        }
    }
}