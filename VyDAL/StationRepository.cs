using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class StationRepository : IStationRepository
    {
        //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Station MapStationDbToStation(StationDb StationDb)
        {
            Station OneStation = new Station
            {
                StationID = StationDb.StationID,
                StationName = StationDb.StationName
            };

            return OneStation;
        }

        public StationDb MapStationToStationDb(Station OneStation)
        {
            var OneStationDb = new StationDb
            {
                StationID = OneStation.StationID,
                StationName = OneStation.StationName,
            };

            return OneStationDb;
        }

        public List<Station> GetAllStations()
        {
            using (var db = new DB())
            {
                var AllStationsDB = db.Stations.ToList();
                var AllStations = new List<Station>();
                foreach (var s in AllStationsDB)
                {
                    AllStations.Add(MapStationDbToStation(s));
                }

                return AllStations;
            };
        }

        public List<string> GetAllStationNames()
        {
            using (var db = new DB())
            {
                var AllStationsDB = db.Stations.ToList();
                var AllStationsNames = new List<string>();
                foreach (var StationDb in AllStationsDB)
                {
                    var StationAddToRoute = db.RouteStations.FirstOrDefault(rs => rs.StationID == StationDb.StationID);

                    if (StationAddToRoute != null)
                    {
                        AllStationsNames.Add(StationDb.StationName);
                    }
                }
                return AllStationsNames;
            };
        }

        public Station GetStationByID(int? StationID)
        {
            using (var db = new DB())
            {
                if (StationID == null)
                {
                    return null;
                }
                Station OneStation = MapStationDbToStation(db.Stations.Find(StationID));

                if (OneStation == null)
                {
                    return null;
                }
                return OneStation;
            }
        }

        public bool AddStation(Station NewStation)
        {
            using (var db = new DB())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                var StationExist = db.Stations.SingleOrDefault(s => s.StationName == NewStation.StationName);
                if (StationExist == null)
                {
                    db.Stations.Add(MapStationToStationDb(NewStation));
                    var entries = db.ChangeTracker.Entries();
                    System.Diagnostics.Debug.WriteLine("Station Name : " + NewStation.StationName);
                    //Log.Error("Station Name : " + NewStation.StationName);

                    foreach (var entry in entries)
                    {
                        System.Diagnostics.Debug.WriteLine("Table Name: " + entry.Entity.GetType().Name);
                        System.Diagnostics.Debug.WriteLine("Status: " + entry.State.ToString());
                        //Log.Error("StStatus: " + entry.State.ToString());
                    }

                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                };
            }
        }

        public bool EditStation(Station Station)
        {
            using (var db = new DB())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                //var OneStationDb = MapStationToStationDb(Station);
                var OneStationDb = db.Stations.Find(Station.StationID);
                System.Diagnostics.Debug.WriteLine("Old Station Name: " + OneStationDb.StationName);

                OneStationDb.StationName = Station.StationName;
                var entries = db.ChangeTracker.Entries();

                //db.Entry(OneStationDb).State = EntityState.Modified;

                foreach (var entry in entries)
                {
                    //foreach (var propName in entry.OriginalValues.PropertyNames)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("Property {0} changed from {1} to {2}",
                    //         propName,
                    //         entry.OriginalValues[propName],
                    //         entry.CurrentValues[propName]);
                    //}
                    System.Diagnostics.Debug.WriteLine("Table Name: " + entry.Entity.GetType().Name);
                    System.Diagnostics.Debug.WriteLine("New Station Name: " + OneStationDb.StationName);
                    System.Diagnostics.Debug.WriteLine("Status: " + entry.State.ToString());
                }

                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteStation(int StationID)
        {
            try
            {
                using (var db = new DB())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    StationDb StationDb = db.Stations.Find(StationID);
                    System.Diagnostics.Debug.WriteLine("Station Name :" + StationDb.StationName);

                    db.Stations.Remove(StationDb);
                    var entries = db.ChangeTracker.Entries();

                    foreach (var entry in entries)
                    {
                        System.Diagnostics.Debug.WriteLine("Table Name: " + entry.Entity.GetType().Name);
                        System.Diagnostics.Debug.WriteLine("Status: " + entry.State.ToString());
                    }
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