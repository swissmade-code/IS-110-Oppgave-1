
namespace UniversitetSystem.Domain.Exceptions
{
    public class LibraryItemAlreadyReturnedException : DomainException
    {
        public LibraryItemAlreadyReturnedException(string message) : base(message)
        {
        }
    }
}