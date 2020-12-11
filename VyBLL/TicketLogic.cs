using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyDAL;
using VyModels;

namespace VyBLL
{
    public class TicketLogic : ITicketLogic
    {
        // Return a list of all tickets
        public List<Ticket> GetAllTickets()
        {
            var TicketDAL = new TicketRepository();
            var AllTickets = TicketDAL.GetAllTickets();
            return AllTickets;

        }

        // Return one ticket by ID
        public Ticket GetTicketByID(int TicketID)
        {
            var TicketDAL = new TicketRepository();
            var OneTicket = TicketDAL.GetTicketByID(TicketID);
            return OneTicket;
        }

        // Save ticket to database
        public int SaveTicket(Ticket t)
        {
            var TicketDAL = new TicketRepository();
            var NewTicketID = TicketDAL.SaveTicket(t);
            return NewTicketID;
        }
    }
}
