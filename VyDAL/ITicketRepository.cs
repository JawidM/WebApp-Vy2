using System.Collections.Generic;
using VyModels;

namespace VyDAL
{
    public interface ITicketRepository
    {
        List<Ticket> GetAllTickets();
        Ticket GetTicketByID(int TicketID);
        Ticket MapTicketDbToTicket(TicketDb TicketDB);
        int SaveTicket(Ticket t);
    }
}