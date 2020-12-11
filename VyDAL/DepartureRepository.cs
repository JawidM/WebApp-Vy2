using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class DepartureRepository : IDepartureRepository
    {
        public Departure MapDepartureDbToDeparture(DepartureDb OneDepartureDb)
        {
            using (var db = new DB())
            {
                var OneDeparture = new Departure
                {
                    DepartureID = OneDepartureDb.DepartureID,
                    RouteID = OneDepartureDb.RouteID,
                    StationID = OneDepartureDb.StationID,
                    StationName = db.Stations.Find(OneDepartureDb.StationID).StationName,
                    DepartureTime = OneDepartureDb.DepartureTime,
                };
                return OneDeparture;
            }
        }

        public DepartureDb MapDepartureToDepartureDb(Departure OneDeparture)
        {
            var OneDepartureDb = new DepartureDb
            {
                RouteID = OneDeparture.RouteID,
                StationID = OneDeparture.StationID,
                DepartureTime = OneDeparture.DepartureTime,
            };
            return OneDepartureDb;
        }
        public List<Departure> GetRouteDepartures(int RouteID)
        {
            using (var db = new DB())
            {
                var AllDeparturesDB = db.Departures.Where(r => r.RouteID == RouteID).ToList();
                var AllDepartures = new List<Departure>();
                foreach (var Departure in AllDeparturesDB)
                {
                    AllDepartures.Add(MapDepartureDbToDeparture(Departure));
                }

                return AllDepartures;
            };
        }

        public Departure GetDepartureByID(int DepartureID)
        {
            using (var db = new DB())
            {

                Departure OneDeparture = MapDepartureDbToDeparture(db.Departures.Find(DepartureID));

                if (OneDeparture == null)
                {
                    return null;
                }
                return OneDeparture;
            }
        }


        public bool AddDeparture(int RouteID, Departure NewDeparture)
        {
            using (var db = new DB())
            {
                NewDeparture.RouteID = RouteID;
                db.Departures.Add(MapDepartureToDepartureDb(NewDeparture));
                db.SaveChanges();
                return true;
            }
        }

        public bool EditDeparture(Departure OneDeparture)
        {
            using (var db = new DB())
            {
                //var OneDepartureDb = db.Departures.Where(d => d.RouteID == OneDeparture.RouteID).SingleOrDefault((s => s.StationID == OneDeparture.StationID));

                var OneDepartureDb = db.Departures.Find(OneDeparture.DepartureID);
                //var OneDepartureDb = MapDepartureToDepartureDb(DepartureDb);
                OneDepartureDb.RouteID = OneDeparture.RouteID;
                OneDepartureDb.StationID = OneDeparture.StationID;
                OneDepartureDb.DepartureTime = OneDeparture.DepartureTime;


                db.Entry(OneDepartureDb).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteDeparture(int DepartureID)
        {
            try
            {
                using (var db = new DB())
                {
                    DepartureDb DepartureDb = db.Departures.Find(DepartureID);
                    db.Departures.Remove(DepartureDb);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
