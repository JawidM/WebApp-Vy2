using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;
using VyDAL;

namespace VyBLL
{
    public class StationLogic : IStationLogic
    {
        private IStationRepository _repository;
        public StationLogic() 
        {
            _repository = new StationRepository();
        }
        public StationLogic(IStationRepository Stub)
        {
            _repository = Stub;
        }

        public List<Station> GetAllStations()
        {
            var AllStations = _repository.GetAllStations();
            return AllStations;
        }

        public List<string> GetAllStationNames()
        {
            var AllStationsNames = _repository.GetAllStationNames();
            return AllStationsNames;
        }

        public Station GetStationByID(int? StationID)
        {
            var OneStation = _repository.GetStationByID(StationID);
            return OneStation;
        }

        public bool AddStation(Station NewStation)
        {
            var StationAdded = _repository.AddStation(NewStation);
            return StationAdded;
        }

        public bool EditStation(Station Station)
        {
            var StationEdited = _repository.EditStation(Station);
            return StationEdited;
        }

        public bool DeleteStation(int id)
        {
            var StationDeleted = _repository.DeleteStation(id);
            return StationDeleted;
        }
    }
}
