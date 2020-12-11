using System.Collections.Generic;
using VyModels;

namespace VyDAL
{
    public interface IRouteRepository
    {
        bool AddRoute(Route NewRoute);
        bool AddRouteStation(int RouteID, Station NewStation);
        bool DeleteRoute(int id);
        bool DeleteRouteStation(int RouteID, int StationID);
        bool EditRoute(Route route);
        bool EditRouteStation(int RouteID, Station OneStation);
        List<Departure> GetAllRouteDepartures(int RouteID);
        List<Route> GetAllRoutes();
        List<Station> GetOneRouteStations(int RouteID);
        Route GetRouteByID(int? id);
        Station GetRouteStationByNumber(int RouteID, int StationNumber);
        Ticket GetTicketWithRoute(Ticket OneTicket);
    }
}