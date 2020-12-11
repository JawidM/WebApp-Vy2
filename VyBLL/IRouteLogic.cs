using System.Collections.Generic;
using VyModels;

namespace VyBLL
{
    public interface IRouteLogic
    {
        bool AddRoute(Route NewRoute);
        bool AddRouteStation(int RouteID, Station NewStation);
        bool DeleteRoute(int id);
        bool DeleteRouteStation(int RouteID, int StationID);
        bool EditRoute(Route route);
        bool EditRouteStation(int RouteID, Station OneStation);
        List<Route> GetAllRoutes();
        List<Station> GetAllRouteStations(int RouteID);
        Route GetRouteByID(int? RouteID);
        Station GetRouteStationByNumber(int RouteID, int StationNumber);
        Ticket GetTicketWithRoute(Ticket OneTicket);
    }
}