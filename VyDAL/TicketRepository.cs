using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class TicketRepository : ITicketRepository
    {
        // Map Ticket object to TicketDb object
        public TicketDb MapTicketToTicketDb(Ticket OneTicket)
        {
            using (var db = new DB())
            {
                List<PassengerDb> DBTicketPassengers = new List<PassengerDb>();
                foreach (var Passenger in OneTicket.TicketPassengers)
                {
                    var DBPassenger = new PassengerDb()
                    {
                        PassengerTypeID = db.PassengerTypes.SingleOrDefault(t => t.PassengerType == Passenger.PassengerType).PassengerTypeID
                    };
                    DBTicketPassengers.Add(DBPassenger);
                };
                var OneRoute = db.Routes.Find(OneTicket.TicketRoute.RouteID);
                var DBTicket = new TicketDb()
                {
                    TicketDuration = OneTicket.TicketDuration,
                    TicketPrice = OneTicket.TicketPrice,
                    TicketPurchaseDate = OneTicket.TicketPurchaseDate,
                    TicketPurchaseTime = OneTicket.TicketPurchaseTime,
                    OrderedBy = OneTicket.OrderedBy,
                    StartStation = OneTicket.StartStation,
                    EndStation = OneTicket.EndStation,
                    TicketPassengers = DBTicketPassengers,
                    TicketRouteID = OneRoute.RouteID

                };


                return DBTicket;
            }
        }

        // Map TicketDb object to Ticket object
        public Ticket MapTicketDbToTicket(TicketDb TicketDB)
        {
            var RouteDAL = new RouteRepository();
            using (var db = new DB())
            {
                List<Passenger> TicketPassengers = new List<Passenger>();
                foreach (var Passenger in TicketDB.TicketPassengers)
                {
                    var OnePassenger = new Passenger
                    {
                        PassengerID = Passenger.PassengerID,
                        PassengerType = db.PassengerTypes.SingleOrDefault(t => t.PassengerTypeID == Passenger.PassengerTypeID).PassengerType
                    };
                    TicketPassengers.Add(OnePassenger);
                }
                var OneTicket = new Ticket
                {
                    TicketID = TicketDB.TicketID,
                    TicketDuration = TicketDB.TicketDuration,
                    TicketPrice = TicketDB.TicketPrice,
                    TicketPurchaseDate = TicketDB.TicketPurchaseDate,
                    TicketPurchaseTime = TicketDB.TicketPurchaseTime,
                    OrderedBy = TicketDB.OrderedBy,
                    StartStation = TicketDB.StartStation,
                    EndStation = TicketDB.EndStation,
                    TicketRoute = RouteDAL.GetRouteByID(TicketDB.TicketRouteID),
                    TicketPassengers = TicketPassengers
                };
                return OneTicket;
            }
        }

        // Return a list of all tickets
        public List<Ticket> GetAllTickets()
        {
            using (var db = new DB())
            {
                List<Ticket> AllTickets = new List<Ticket>();
                List<TicketDb> AllTicketsFromDB = db.Tickets.ToList();
                foreach (var TicketDb in AllTicketsFromDB)
                {
                    var OneTicket = MapTicketDbToTicket(TicketDb);
                    AllTickets.Add(OneTicket);

                }
                return AllTickets;
            }
        }

        // Return one ticket by ID
        public Ticket GetTicketByID(int TicketID)
        {
            using (var db = new DB())
            {
                var OneTicketFromDB = db.Tickets.Where(t => t.TicketID == TicketID).
                    SingleOrDefault();

                var OneTicket = MapTicketDbToTicket(OneTicketFromDB);
                return OneTicket;

            }
        }

        // Save ticket to database
        public int SaveTicket(Ticket t)
        {
            using (var db = new DB())
            {
                try
                {
                    var TicketDB = MapTicketToTicketDb(t);
                    // db.Routes.Attach(TicketDB.TicketRoute);
                    db.Tickets.Add(TicketDB);

                    db.SaveChanges();
                    db.Entry(TicketDB).GetDatabaseValues();
                    int id = TicketDB.TicketID;
                    return id;

                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
    }
}
