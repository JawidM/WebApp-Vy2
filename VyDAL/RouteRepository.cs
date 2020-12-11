using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class RouteRepository : IRouteRepository
    {
        public Route MapRouteDbToRoute(RouteDb OneRouteDb)
        {
            using (var db = new DB())
            {
                var OneRoute = new Route
                {
                    RouteID = OneRouteDb.RouteID,
                    RouteName = OneRouteDb.RouteName,
                    RouteStations = GetOneRouteStations(OneRouteDb.RouteID),
                    RouteDepartures = GetAllRouteDepartures(OneRouteDb.RouteID)
                };

                return OneRoute;
            }
        }

        public RouteDb MapRouteToRouteDb(Route OneRoute)
        {
            var OneRouteDb = new RouteDb
            {
                RouteID = OneRoute.RouteID,
                RouteName = OneRoute.RouteName,
                //RouteTrains = AllRouteTrainDb
            };

            return OneRouteDb;
        }

        public List<Route> GetAllRoutes()
        {
            using (var db = new DB())
            {
                var AllRoutes = new List<Route>();
                var AllRoutesDb = db.Routes.ToList();
                foreach (var OneRouteDb in AllRoutesDb)
                {
                    AllRoutes.Add(MapRouteDbToRoute(OneRouteDb));
                }

                return AllRoutes;
            };
        }

        public Route GetRouteByID(int? id)
        {
            using (var db = new DB())
            {
                if (id == null)
                {
                    return null;
                }
                Route route = MapRouteDbToRoute(db.Routes.Find(id));
                if (route == null)
                {
                    return null;
                }
                return route;
            }
        }

        public bool AddRoute(Route NewRoute)
        {
            using (var db = new DB())
            {
                var Routesxist = db.Routes.SingleOrDefault(r => r.RouteName == NewRoute.RouteName);
                if (Routesxist == null)
                {
                    db.Routes.Add(MapRouteToRouteDb(NewRoute));
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                };
            }
        }

        public bool EditRoute(Route route)
        {
            using (var db = new DB())
            {
                var OneRouteDb = MapRouteToRouteDb(route);
                db.Entry(OneRouteDb).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteRoute(int id)
        {
            try
            {
                using (var db = new DB())
                {
                    RouteDb routeDb = db.Routes.Find(id);
                    db.Routes.Remove(routeDb);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Station GetRouteStationByNumber(int RouteID, int StationNumber)
        {
            using (var db = new DB())
            {
                var RouteStationDb = db.RouteStations.Where(rs => rs.RouteID == RouteID).SingleOrDefault((s => s.StationNumber == StationNumber));
                Station RouteStation = new Station
                {
                    StationID = RouteStationDb.StationID,
                    StationName = db.Stations.Find(RouteStationDb.StationID).StationName,
                    StationNumber = RouteStationDb.StationNumber
                };
                return RouteStation;
            }
        }

        public List<Station> GetOneRouteStations(int RouteID)
        {
            using (var db = new DB())
            {
                var AllRouteStationDb = db.RouteStations.Where(rs => rs.RouteID == RouteID).ToList();
                List<Station> AllRouteStation = new List<Station> { };
                foreach (var RouteStationDb in AllRouteStationDb)
                {
                    AllRouteStation.Add(new Station
                    {
                        StationID = RouteStationDb.StationID,
                        StationName = db.Stations.Find(RouteStationDb.StationID).StationName,
                        StationNumber = RouteStationDb.StationNumber
                    });
                }
                return AllRouteStation;
            }
        }

        public List<Departure> GetAllRouteDepartures(int RouteID)
        {
            using (var db = new DB())
            {
                var AllDeparturesDb = db.Departures.Where(d => d.RouteID == RouteID).ToList();
                var AllOneDepartures = new List<Departure> { };
                foreach (var OneDepartureDb in AllDeparturesDb)
                {
                    AllOneDepartures.Add(new Departure
                    {
                        DepartureID = OneDepartureDb.DepartureID,
                        StationID = OneDepartureDb.StationID,
                        StationName = db.Stations.Find(OneDepartureDb.StationID).StationName,
                        DepartureTime = OneDepartureDb.DepartureTime,
                        RouteID = OneDepartureDb.RouteID
                    });
                }
                return AllOneDepartures;
            }
        }

        public bool AddRouteStation(int RouteID, Station NewStation)
        {
            using (var db = new DB())
            {
                System.Diagnostics.Debug.WriteLine(RouteID);
                System.Diagnostics.Debug.WriteLine(NewStation.StationID);

                try
                {
                    db.RouteStations.Add(new RouteStationDb
                    {
                        RouteID = RouteID,
                        StationID = NewStation.StationID,
                        StationNumber = NewStation.StationNumber
                    });
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool EditRouteStation(int RouteID, Station OneStation)
        {
            using (var db = new DB())
            {
                var RouteStationDb = db.RouteStations.Where(rs => rs.RouteID == RouteID).SingleOrDefault((s => s.StationID == OneStation.StationID));
                RouteStationDb.StationNumber = OneStation.StationNumber;
                db.Entry(RouteStationDb).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteRouteStation(int RouteID, int StationID)
        {
            using (var db = new DB())
            {
                var RouteStationDb = db.RouteStations.Where(rs => rs.RouteID == RouteID).SingleOrDefault((s => s.StationID == StationID));
                db.RouteStations.Remove(RouteStationDb);
                db.SaveChanges();

                return true;
            }
        }

        // Get the route between start and end stations from ticket object.
        public Ticket GetTicketWithRoute(Ticket OneTicket)
        {
            using (var db = new DB())
            {
                var s1 = db.Stations.SingleOrDefault(s => s.StationName == OneTicket.StartStation);
                var s2 = db.Stations.SingleOrDefault(s => s.StationName == OneTicket.EndStation);
                var RouteStations1 = db.RouteStations.Where(s => s.StationID == s1.StationID).ToList();
                var RouteStations2 = db.RouteStations.Where(s => s.StationID == s2.StationID).ToList();

                if (s1 != null && s2 != null)
                {
                    foreach (var rs1 in RouteStations1)
                    {
                        foreach (var rs2 in RouteStations2)
                        {
                            if (rs1.RouteID == rs2.RouteID)
                            {
                                //var OneRoute = GetRouteByID(rs1.RouteID);
                                var OneRoute = GetTicketRoute(rs1.RouteID, s1.StationID, s2.StationID);

                                OneTicket.TicketPrice = 0;
                                switch (OneRoute.RouteID)
                                {
                                    case 1:
                                        OneTicket.TicketDuration = 60;

                                        foreach (var passenger in OneTicket.TicketPassengers)
                                        {
                                            switch (passenger.PassengerType)
                                            {
                                                case "Adult":
                                                    OneTicket.TicketPrice += 50;
                                                    break;

                                                case "Student":
                                                    OneTicket.TicketPrice += 30;
                                                    break;

                                                case "Child":
                                                    OneTicket.TicketPrice += 10;
                                                    break;
                                            }
                                        }
                                        break;

                                    case 2:
                                        OneTicket.TicketDuration = 120;
                                        foreach (var passenger in OneTicket.TicketPassengers)
                                        {
                                            switch (passenger.PassengerType)
                                            {
                                                case "Adult":
                                                    OneTicket.TicketPrice += 300;
                                                    break;

                                                case "Student":
                                                    OneTicket.TicketPrice += 150;
                                                    break;

                                                case "Child":
                                                    OneTicket.TicketPrice += 75;
                                                    break;
                                            }
                                        }
                                        break;
                                }
                                OneTicket.TicketRoute = OneRoute;
                            }
                        }
                    }

                    return OneTicket;
                }

                return OneTicket;
            }
        }

        private Route GetTicketRoute(int RouteID, int StartStationID, int EndStationID)
        {
            using (var db = new DB())
            {
                Route OneRoute = MapRouteDbToRoute(db.Routes.Find(RouteID));
                var RouteStations = new List<Station>();
                if (StartStationID < EndStationID)
                    RouteStations = OneRoute.RouteStations.Where(s1 => s1.StationID >= StartStationID).Where(s2 => s2.StationID <= EndStationID).ToList();
                if (StartStationID > EndStationID)
                {
                    RouteStations = OneRoute.RouteStations.Where(s1 => s1.StationID <= StartStationID).Where(s2 => s2.StationID >= EndStationID).ToList();
                    RouteStations.Reverse();
                }
                OneRoute.RouteStations = RouteStations;
                if (OneRoute == null)
                {
                    return null;
                }
                return OneRoute;
            }
        }
    }
}