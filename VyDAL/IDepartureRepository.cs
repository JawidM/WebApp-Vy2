using System.Collections.Generic;
using VyModels;

namespace VyDAL
{
    public interface IDepartureRepository
    {
        bool AddDeparture(int RouteID, Departure NewDeparture);
        bool DeleteDeparture(int DepartureID);
        bool EditDeparture(Departure OneDeparture);
        List<Departure> GetRouteDepartures(int RouteID);
        Departure GetDepartureByID(int DepartureID);
    }
}