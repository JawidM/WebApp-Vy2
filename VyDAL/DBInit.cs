using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    public class DBInit : DropCreateDatabaseAlways<DB>
    {
        private static byte[] fixHash(string innPassord, byte[] innSalt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(innPassord, innSalt, 1000); // 1000 angir hvor mange ganger hash funskjonen skal utføres for økt sikkerhet
            return pbkdf2.GetBytes(keyLength);
        }

        private static byte[] fixSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csprng.GetBytes(salt);
            return salt;
        }

        protected override void Seed(DB context)
        {
            // Initiate the datebase with data
            byte[] salt = fixSalt();
            byte[] hash = fixHash("vyadmin", salt);
            var admin1 = new AdminDb()
            {
                Email = "admin@vy.no",
                Salt = salt,
                Password = hash
            };

            var AllAdmins = new List<AdminDb>
            {
                admin1
            };

            foreach (var OneAdminDb in AllAdmins)
            {
                context.Admins.Add(OneAdminDb);
            }

            var Station1 = new StationDb
            {
                StationName = "Oslo S"
            };

            var Station2 = new StationDb
            {
                StationName = "Nationaltheatret",
            };

            var Station3 = new StationDb
            {
                StationName = "Lysaker",
            };

            var Station4 = new StationDb
            {
                StationName = "Myrdal",
            };

            var Station5 = new StationDb
            {
                StationName = "Bergen",
            };

            var AllStations = new List<StationDb> {
                Station1,
                Station2,
                Station3,
                Station4,
                Station5
            };

            foreach (var OneStationDb in AllStations)
            {
                context.Stations.Add(OneStationDb);
            }

            var DeparturTime1Station1 = new DepartureDb
            {
                RouteID = 1,
                StationID = 1,
                DepartureTime = new DateTime(2019, 10, 20, 11, 30, 0)
            };

            var DeparturTime2Station1 = new DepartureDb
            {
                RouteID = 1,
                StationID = 1,
                DepartureTime = new DateTime(2019, 10, 20, 14, 00, 0)
            };

            var DeparturTime3Station1 = new DepartureDb
            {
                RouteID = 2,
                StationID = 1,
                DepartureTime = new DateTime(2019, 10, 20, 21, 30, 0)
            };

            var DeparturTime4Station1 = new DepartureDb
            {
                RouteID = 2,
                StationID = 1,
                DepartureTime = new DateTime(2019, 10, 21, 06, 30, 0)
            };

            var DeparturTime1Station2 = new DepartureDb
            {
                RouteID = 1,
                StationID = 2,
                DepartureTime = new DateTime(2019, 10, 20, 11, 35, 0)
            };

            var DeparturTime2Station2 = new DepartureDb
            {
                RouteID = 1,
                StationID = 2,
                DepartureTime = new DateTime(2019, 10, 20, 14, 05, 0)
            };

            var DeparturTime1Station3 = new DepartureDb
            {
                RouteID = 1,
                StationID = 3,
                DepartureTime = new DateTime(2019, 10, 20, 11, 45, 0)
            };

            var DeparturTime2Station3 = new DepartureDb
            {
                RouteID = 1,
                StationID = 3,
                DepartureTime = new DateTime(2019, 10, 20, 14, 15, 0)
            };

            var DeparturTime1Station4 = new DepartureDb
            {
                RouteID = 2,
                StationID = 4,
                DepartureTime = new DateTime(2019, 10, 20, 19, 15, 0)
            };

            var DeparturTime2Station4 = new DepartureDb
            {
                RouteID = 2,
                StationID = 4,
                DepartureTime = new DateTime(2019, 10, 21, 03, 45, 0)
            };

            var DeparturTime1Station5 = new DepartureDb
            {
                RouteID = 2,
                StationID = 5,
                DepartureTime = new DateTime(2019, 10, 20, 15, 00, 0)
            };

            var DeparturTime2Station5 = new DepartureDb
            {
                RouteID = 2,
                StationID = 5,
                DepartureTime = new DateTime(2019, 10, 20, 23, 30, 0)
            };

            var AllDeparturs = new List<DepartureDb> {
            DeparturTime1Station1,
            DeparturTime1Station2,
            DeparturTime1Station3,
            DeparturTime3Station1,
            DeparturTime4Station1,
            DeparturTime1Station4,
            DeparturTime1Station5,
            DeparturTime2Station1,
            DeparturTime2Station2,
            DeparturTime2Station3,
            DeparturTime2Station4,
            DeparturTime2Station5,
            };

            foreach (var Departure in AllDeparturs)
            {
                context.Departures.Add(Departure);
            }

            var Route1 = new RouteDb
            {
                RouteName = "R1",
            };

            var Route2 = new RouteDb
            {
                RouteName = "R2",
            };

            var RoutesDb = new List<RouteDb> {
                Route1,
                Route2
                };

            foreach (var OneRouteDb in RoutesDb)
            {
                context.Routes.Add(OneRouteDb);
            }

            var RouteStation1 = new RouteStationDb
            {
                RouteID = 1,
                StationID = 1,
                StationNumber = 1
            };

            var RouteStation2 = new RouteStationDb
            {
                RouteID = 1,
                StationID = 2,
                StationNumber = 2
            };

            var RouteStation3 = new RouteStationDb
            {
                RouteID = 1,
                StationID = 3,
                StationNumber = 3
            };

            var RouteStation4 = new RouteStationDb
            {
                RouteID = 2,
                StationID = 1,
                StationNumber = 1
            };

            var RouteStation5 = new RouteStationDb
            {
                RouteID = 2,
                StationID = 4,
                StationNumber = 2
            };

            var RouteStation6 = new RouteStationDb
            {
                RouteID = 2,
                StationID = 5,
                StationNumber = 3
            };

            var RouteStations = new List<RouteStationDb>
            {
                RouteStation1,
                RouteStation2,
                RouteStation3,
                RouteStation4,
                RouteStation5,
                RouteStation6
            };

            foreach (var RouteStation in RouteStations)
            {
                context.RouteStations.Add(RouteStation);
            }

            var PassengerType1 = new PassengerTypeDb
            {
                PassengerType = "Adult"
            };

            var PassengerType2 = new PassengerTypeDb
            {
                PassengerType = "Student"
            };

            var PassengerType3 = new PassengerTypeDb
            {
                PassengerType = "Child"
            };

            var AllPassengerTypes = new List<PassengerTypeDb> {
                PassengerType1,
                PassengerType2,
                PassengerType3
            };

            foreach (var pt in AllPassengerTypes)
                context.PassengerTypes.Add(pt);

            var Price1 = new PriceDb
            {
                RouteID = 1,
                PassengerTypeID = 1,
                TicketPrice = 50
            };

            var Price2 = new PriceDb
            {
                RouteID = 1,
                PassengerTypeID = 2,
                TicketPrice = 30
            };

            var Price3 = new PriceDb
            {
                RouteID = 1,
                PassengerTypeID = 3,
                TicketPrice = 15
            };

            var Price4 = new PriceDb
            {
                RouteID = 2,
                PassengerTypeID = 1,
                TicketPrice = 300
            };

            var Price5 = new PriceDb
            {
                RouteID = 2,
                PassengerTypeID = 2,
                TicketPrice = 150
            };

            var Price6 = new PriceDb
            {
                RouteID = 2,
                PassengerTypeID = 3,
                TicketPrice = 75
            };

            var AllPrices = new List<PriceDb> {
                Price1,
                Price2,
                Price3,
                Price4,
                Price5,
                Price6
            };

            foreach (var p in AllPrices)
                context.Prices.Add(p);

            var Passenger1 = new PassengerDb
            {
                PassengerTypeID = 1
            };

            var Passenger2 = new PassengerDb
            {
                PassengerTypeID = 2
            };

            var Passenger3 = new PassengerDb
            {
                PassengerTypeID = 3
            };

            var PassengerList1 = new List<PassengerDb>
            {
                Passenger1
            };

            var PassengerList2 = new List<PassengerDb>
            {
                Passenger2
            };
            var PassengerList3 = new List<PassengerDb>
            {
                Passenger1,
                Passenger3
            };

            var Ticket1 = new TicketDb
            {
                TicketDuration = 60,
                TicketPassengers = PassengerList1,
                TicketPrice = 50,
                TicketRouteID = 1,
                StartStation = "Oslo S",
                EndStation = "Lysaker",
                OrderedBy = "ole@nordmann.no",
                TicketPurchaseDate = new DateTime(2019, 9, 29),
                TicketPurchaseTime = new DateTime(2019, 9, 29, 11, 30, 0)
            };

            var Ticket2 = new TicketDb
            {
                TicketDuration = 60,
                TicketPassengers = PassengerList2,
                TicketPrice = 50,
                TicketRouteID = 1,
                StartStation = "Lysaker",
                EndStation = "Oslo S",
                OrderedBy = "kari@nordmann.no",
                TicketPurchaseDate = new DateTime(2019, 9, 29),
                TicketPurchaseTime = new DateTime(2019, 9, 29, 15, 05, 0)
            };

            var Ticket3 = new TicketDb
            {
                TicketDuration = 290,
                TicketPassengers = PassengerList3,
                TicketPrice = 250,
                TicketRouteID = 2,
                StartStation = "Bergen",
                EndStation = "Oslo S",
                OrderedBy = "kari@nordmann.no",
                TicketPurchaseDate = new DateTime(2019, 9, 29),
                TicketPurchaseTime = new DateTime(2019, 9, 29, 15, 05, 0)
            };

            List<TicketDb> AllTickets = new List<TicketDb>
            {
                Ticket1,
                Ticket2,
                Ticket3
            };

            foreach (TicketDb t in AllTickets)
                context.Tickets.Add(t);

            base.Seed(context);
        }
    }
}