using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class StationRepositoryStub : IStationRepository
    {
        public bool AddStation(Station NewStation)
        {
            var AllStations = GetAllStations();
            var StationsCount = AllStations.Count();
            if (NewStation.StationName == null || NewStation.StationID == 0)
            {
                return false;
            }
            AllStations.Add(NewStation);
            if (AllStations.Count() > StationsCount)
            {
                return true;
            }
            else return false;
        }

        public bool DeleteStation(int StationID)
        {
            var AllStations = GetAllStations();
            var OneStation = AllStations.FirstOrDefault(s => s.StationID == StationID);
            if (OneStation != null)
            {
                AllStations.Remove(OneStation);
                return true;
            }
            else return false;
        }

        public bool EditStation(Station Station)
        {
            var AllStations = GetAllStations();
            var OneStation = AllStations.FirstOrDefault(s => s.StationID == Station.StationID);
            if (Station.StationName == null || Station.StationID == 0)
            {
                return false;
            }
            if (OneStation != null)
            {
                OneStation.StationName = Station.StationName;
                OneStation.StationNumber = Station.StationNumber;
                return true;
            }
            else return false;
        }

        public List<string> GetAllStationNames()
        {
            return new List<string>
            {
               "Oslo S",
               "Nationaltheatret",
               "Lysaker"
             };
        }

        public List<Station> GetAllStations()
        {
            return new List<Station>
            {
                new Station {StationID = 1, StationName = "Oslo S"},
                new Station {StationID = 2, StationName = "Nationaltheatret"},
                new Station{StationID = 3, StationName = "Lysaker"}
            };
        }

        public Station GetStationByID(int? StationID)
        {
            var AllStations = GetAllStations();
            return AllStations.FirstOrDefault(s => s.StationID == StationID);
        }
    }
}