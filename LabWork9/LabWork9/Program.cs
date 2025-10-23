using LabWork9.Contexts;
using LabWork9.Services;
using Microsoft.EntityFrameworkCore;
using LabWork9.Models;

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
using var context = new AppDbContext(optionsBuilder.Options);

var ticketService = new TicketService(context);
var tickets = await ticketService.GetTicketsAsync();

var visitorService = new VisitorService(context);
var visitors = await visitorService.GetVisitorAsync();

await visitorService.AddVisitorAsync(new Visitor()
{
    Name = "John Doe",
    Phone = "89006004321",
    Birthday = DateTime.Now,
    Email = "johndoe@mail.com"
});

await ticketService.AddTicketAsync(new Ticket()
{
    VisitorId = 4,
    SessionId = 1,
    Row = 4,
    Seat = 5
});

await visitorService.UpdateVisitorsAsync(9);
await ticketService.UpdateTicketAsync(9);

await visitorService.DeleteVisitorAsync(9);
await ticketService.DeleteTicketAsync(9);