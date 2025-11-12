using LabWork9.Contexts;
using LabWork9.Services;
using Microsoft.EntityFrameworkCore;
using LabWork9.Models;

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
using var context = new AppDbContext(optionsBuilder.Options);

const int id = 8;

var ticketService = new TicketService(context);
var tickets = await ticketService.GetTicketsAsync();

var visitorService = new VisitorService(context);
var visitors = await visitorService.GetVisitorAsync();


var visitor = new Visitor()
{
    Name = "John Doe",
    Phone = "79006004333",
    Birthday = DateTime.Now,
};
await visitorService.AddVisitorAsync(visitor);

visitor.Name = "fgdfhgffd435";
await visitorService.UpdateVisitorsAsync(visitor);

await ticketService.AddTicketAsync(new Ticket()
{
    VisitorId = 4,
    SessionId = 1,
    Row = 6,
    Seat = 3
});

await ticketService.UpdateTicketAsync(new Ticket()
{
    TicketId = 1,
    Row = 3,
    Seat = 3
});

await visitorService.DeleteVisitorAsync(id);
await ticketService.DeleteTicketAsync(id);