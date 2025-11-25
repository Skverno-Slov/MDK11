namespace LabWork24.DTOs
{
    public class TicketDto
    {
        public int TicketId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public string Cinema { get; set; } = null!;
        public byte HallNumber { get; set; }
        public byte Row { get; set; }
        public byte Seat { get; set; }
    }
}
