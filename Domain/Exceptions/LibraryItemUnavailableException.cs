namespace UniversitetSystem.Domain.Exceptions
{
    public class LibraryItemUnavailableException : DomainException
    {
        public LibraryItemUnavailableException(string message)
                : base(message)
        {
        }
    }
}