using System;
using System.Collections.Generic;
using System.Linq;
using VyModels;

namespace VyDAL
{
    public class DepartureRepositoryStub : IDepartureRepository
    {
        public bool AddDeparture(int RouteID, Departure NewDeparture)
        {
            var AllDepartures = GetAllDepartures();
            var DeparturesCount = AllDepartures.Count();
            if (NewDeparture.DepartureTime == null || NewDeparture.DepartureID == 0)
            {
                return false;
            }
            AllDepartures.Add(NewDeparture);
            if (AllDepartures.Count() > DeparturesCount)
            {
                return true;
            }
            else return false;
        }

        public bool DeleteDeparture(int DepartureID)
        {
            var AllDepartures = GetAllDepartures();
            var OneDeparture = AllDepartures.FirstOrDefault(d => d.DepartureID == DepartureID);
            if (OneDeparture != null)
            {
                AllDepartures.Remove(OneDeparture);
                return true;
            }
            else return false;
        }

        public bool EditDeparture(Departure Departure)
        {
            var AllDepartures = GetAllDepartures();
            if (Departure.DepartureTime == null || Departure.DepartureID == 0)
            {
                return false;
            }
            var OneDeparture = AllDepartures.FirstOrDefault(d => d.DepartureID == Departure.DepartureID);

            if (OneDeparture != null)
            {
                OneDeparture.RouteID = Departure.RouteID;
                OneDeparture.StationID = Departure.StationID;
                OneDeparture.StationName = Departure.StationName;
                OneDeparture.DepartureTime = Departure.DepartureTime;
                return true;
            }
            else return false;
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

        public List<Departure> GetRouteDepartures(int RouteID)
        {
            var AllDepartures = GetAllDepartures();
            var RouteDepartres = AllDepartures.Where(d => d.RouteID == RouteID).ToList();
            return RouteDepartres;
        }

        public Departure GetDepartureByID(int DepartureID)
        {
            var AllDepartures = GetAllDepartures();
            return AllDepartures.FirstOrDefault(d => d.DepartureID == DepartureID);
        }
    }
}