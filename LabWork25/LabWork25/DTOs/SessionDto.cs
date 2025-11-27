namespace LabWork25.DTOs
{
    public class SessionDto
    {
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public byte HallNumber { get; set; }
        public decimal Price { get; set; }
    }
}
