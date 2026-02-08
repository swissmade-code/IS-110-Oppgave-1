

namespace UniversitetSystem.Models.Users
{
    public class ExchangeStudent : Student
    {
        public string HomeUniversity { get; set; }
        public string Country { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public ExchangeStudent(
            int id, string name, string email,
            string homeUniversity, string country,
            DateTime from, DateTime to
        ) : base(id, name, email)
        {
            HomeUniversity = homeUniversity;
            Country = country;
            From = from;
            To = to;
        }
    }
}