using System.Collections.Generic;
using VyModels;

namespace VyBLL
{
    public interface IDepartureLogic
    {
        bool AddDeparture(int RouteID, Departure NewDeparture);
        bool DeleteDeparture(int RouteID, int DepartureID);
        bool EditDeparture(int RouteID, Departure Departure);
        List<Departure> GetRouteDepartures(int DepartureID);
        Departure GetDepartureByID(int DepartureID);
    }
}