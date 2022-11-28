
namespace ContactApi.ApiModels
{
    public class CreateContactModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public string CategoryId { get; set; }
        public string CategoryText { get; set; }
    }
}
