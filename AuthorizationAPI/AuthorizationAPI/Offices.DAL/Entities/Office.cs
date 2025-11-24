namespace Offices.DAL.Entities
{
    public class Office
    {
        public required Guid Id { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public required Guid PhotoId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
