using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyDAL;
using VyModels;

namespace VyBLL
{
    public class RouteLogic : IRouteLogic
    {
        private IRouteRepository _repository;

        public RouteLogic()
        {
            _repository = new RouteRepository();
        }

        public RouteLogic(IRouteRepository Stub)
        {
            _repository = Stub;
        }

        public List<Route> GetAllRoutes()
        {
            var AllRoutes = _repository.GetAllRoutes();

            return AllRoutes;
        }

        public Route GetRouteByID(int? RouteID)
        {
            var OneRoute = _repository.GetRouteByID(RouteID);

            return OneRoute;
        }

        public bool AddRoute(Route NewRoute)
        {
            var RouteAdded = _repository.AddRoute(NewRoute);

            return RouteAdded;
        }

        public bool DeleteRoute(int id)
        {
            var RouteDeleted = _repository.DeleteRoute(id);

            return RouteDeleted;
        }

        public bool EditRoute(Route route)
        {
            var RouteEdited = _repository.EditRoute(route);

            return RouteEdited;
        }

        public Station GetRouteStationByNumber(int RouteID, int StationNumber)
        {
            Station RouteStation = _repository.GetRouteStationByNumber(RouteID, StationNumber);
            return RouteStation;
        }

        public List<Station> GetAllRouteStations(int RouteID)
        {
            List<Station> AllRouteStations = _repository.GetOneRouteStations(RouteID);
            return AllRouteStations;
        }

        public bool AddRouteStation(int RouteID, Station NewStation)
        {
            var StationAdded = _repository.AddRouteStation(RouteID, NewStation);

            return StationAdded;
        }

        public bool EditRouteStation(int RouteID, Station OneStation)
        {
            var StationEdited = _repository.EditRouteStation(RouteID, OneStation);

            return StationEdited;
        }

        public bool DeleteRouteStation(int RouteID, int StationID)
        {
            var StationDeleted = _repository.DeleteRouteStation(RouteID, StationID);

            return StationDeleted;
        }

        public Ticket GetTicketWithRoute(Ticket OneTicket)
        {
            var TicketWithRoute = _repository.GetTicketWithRoute(OneTicket);
            return TicketWithRoute;
        }
    }
}