using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class RouteRepositoryStub : IRouteRepository
    {
        public bool AddRouteStation(int RouteID, Station NewStation)
        {
            var OneRouteStations = GetOneRouteStations(RouteID);
            var StationsCount = OneRouteStations.Count();
            if (NewStation.StationName == null || NewStation.StationID == 0)
            {
                return false;
            }
            OneRouteStations.Add(NewStation);
            if (OneRouteStations.Count() > StationsCount)
            {
                return true;
            }
            else return false;
        }

        public bool DeleteRouteStation(int RouteID, int StationID)
        {
            var OneRouteStations = GetOneRouteStations(RouteID);
            var OneStation = OneRouteStations.FirstOrDefault(s => s.StationID == StationID);
            if (OneStation == null)
            {
                return false;
            }

            OneRouteStations.Remove(OneStation);
            return true;
        }

        public bool EditRouteStation(int RouteID, Station OneStation)
        {
            var OneRouteStations = GetOneRouteStations(RouteID);
            var Station = OneRouteStations.FirstOrDefault(s => s.StationID == OneStation.StationID);
            if (Station != null)
            {
                Station.StationName = OneStation.StationName;
                Station.StationNumber = OneStation.StationNumber;
                return true;
            }
            else return false;
        }

        public List<Departure> GetAllRouteDepartures(int RouteID)
        {
            var AllDepartures = GetAllDepartures();
            var RouteDepartres = AllDepartures.Where(d => d.RouteID == RouteID).ToList();
            return RouteDepartres;
        }

        public List<Route> GetAllRoutes()
        {
            var RouteStations1 = new List<Station>
            {
                new Station {StationID = 1, StationName = "Oslo S" , StationNumber = 1 },
                new Station { StationID = 2, StationName = "Nationaltheatret", StationNumber = 2},
                new Station { StationID = 3, StationName = "Lysaker", StationNumber = 3  }
            };

            var RouteStations2 = new List<Station>
            {
                new Station { StationID = 1, StationName = "Oslo S", StationNumber = 1},
                new Station { StationID = 4, StationName = "Myrdal", StationNumber = 2 },
                new Station { StationID = 5, StationName = "Bergen", StationNumber = 3 }
            };
            return new List<Route>
            {
                new Route{ RouteID = 1, RouteName = "R1" , RouteStations = RouteStations1},
                new Route{ RouteID = 2, RouteName = "R2", RouteStations = RouteStations2},
            };
        }

        public List<Station> GetOneRouteStations(int RouteID)
        {
            var AllRoutes = GetAllRoutes();
            var OneRouteStations = new List<Station>();
            foreach (var Route in AllRoutes)
            {
                if (Route.RouteID == RouteID)
                {
                    foreach (var Station in Route.RouteStations)
                    {
                        OneRouteStations.Add(Station);
                    }
                }
            }
            return OneRouteStations;
        }

        public Station GetRouteStationByNumber(int RouteID, int StationNumber)
        {
            var OneRouteStations = GetOneRouteStations(RouteID);
            var OneStation = OneRouteStations.FirstOrDefault(s => s.StationNumber == StationNumber);
            return OneStation;
        }

        public Ticket GetTicketWithRoute(Ticket OneTicket)
        {
            OneTicket.TicketRoute = GetRouteByID(1);

            return OneTicket;
        }

        public bool AddRoute(Route NewRoute)
        {
            var AllRoutes = GetAllRoutes();
            var RoutesCount = AllRoutes.Count();
            if (NewRoute.RouteName == "" || NewRoute.RouteID == 0)
            {
                return false;
            }
            AllRoutes.Add(NewRoute);
            if (AllRoutes.Count() > RoutesCount)
            {
                return true;
            }
            else return false;
        }

        public bool DeleteRoute(int RouteID)
        {
            var AllRoutes = GetAllRoutes();
            var OneRoute = AllRoutes.FirstOrDefault(r => r.RouteID == RouteID);
            if (OneRoute != null)
            {
                AllRoutes.Remove(OneRoute);
                return true;
            }
            else return false;
        }

        public bool EditRoute(Route Route)
        {
            var AllRoutes = GetAllRoutes();
            var OneRoute = AllRoutes.FirstOrDefault(r => r.RouteID == Route.RouteID);
            if (Route.RouteName == "" || Route.RouteID == 0)
            {
                return false;
            }
            if (OneRoute != null)
            {
                OneRoute.RouteName = Route.RouteName;

                return true;
            }
            else return false;
        }

        public List<string> GetAllPassengerTypes()
        {
            return new List<string>
            {
                 "Adult",
                 "Student",
                 "Child"
            };
        }

        public Route GetRouteByID(int? RouteID)
        {
            var AllRoutes = GetAllRoutes();
            return AllRoutes.FirstOrDefault(r => r.RouteID == RouteID);
        }

        public List<Departure> GetAllDepartures()
        {
            return new List<Departure>
            {
                new Departure {DepartureID = 1, RouteID = 1, StationID = 1, DepartureTime = new DateTime(2019, 10, 20, 11, 30, 0) },
                new Departure {DepartureID = 2, RouteID = 1, StationID = 1, DepartureTime = new DateTime(2019, 10, 20, 14, 00, 0) },
                new Departure {DepartureID = 3, RouteID = 2, StationID = 1, DepartureTime = new DateTime(2019, 10, 20, 21, 30, 0) },
                new Departure {DepartureID = 4, RouteID = 2, StationID = 1, DepartureTime = new DateTime(2019, 10, 21, 06, 30, 0) },
                new Departure {DepartureID = 5, RouteID = 1, StationID = 2, DepartureTime = new DateTime(2019, 10, 20, 11, 35, 0) },
                new Departure {DepartureID = 6, RouteID = 1, StationID = 2, DepartureTime = new DateTime(2019, 10, 20, 14, 05, 0) },
                new Departure {DepartureID = 7, RouteID = 1, StationID = 3, DepartureTime = new DateTime(2019, 10, 20, 11, 45, 0) },
                new Departure {DepartureID = 8, RouteID = 1, StationID = 3, DepartureTime = new DateTime(2019, 10, 20, 14, 15, 0) },
                new Departure {DepartureID = 9, RouteID = 2, StationID = 4, DepartureTime = new DateTime(2019, 10, 20, 19, 15, 0) },
                new Departure {DepartureID = 10, RouteID = 2, StationID = 4, DepartureTime = new DateTime(2019, 10, 21, 03, 45, 0) },
                new Departure {DepartureID = 11, RouteID = 2, StationID = 5, DepartureTime = new DateTime(2019, 10, 20, 15, 00, 0) },
                new Departure {DepartureID = 12, RouteID = 2, StationID = 5, DepartureTime = new DateTime(2019, 10, 20, 23, 30, 0) }
            };
        }
    }
}