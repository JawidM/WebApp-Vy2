using System.Collections.Generic;
using VyModels;

namespace VyBLL
{
    public interface ITicketLogic
    {
        List<Ticket> GetAllTickets();
        Ticket GetTicketByID(int TicketID);
        int SaveTicket(Ticket t);
    }
}