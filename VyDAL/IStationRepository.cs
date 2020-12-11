using System.Collections.Generic;
using VyModels;

namespace VyDAL
{
    public interface IStationRepository
    {
        bool AddStation(Station NewStation);
        bool DeleteStation(int id);
        bool EditStation(Station Station);
        List<string> GetAllStationNames();
        List<Station> GetAllStations();
        Station GetStationByID(int? id);
    }
}