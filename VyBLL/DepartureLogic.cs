using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;
using VyDAL;

namespace VyBLL
{
    public class DepartureLogic : IDepartureLogic
    {
        private IDepartureRepository _repository;

        public DepartureLogic()
        {
            _repository = new DepartureRepository();
        }

        public DepartureLogic(IDepartureRepository Stub)
        {
            _repository = Stub;
        }

        public List<Departure> GetRouteDepartures(int DepartureID)
        {
            var AllDepartures = _repository.GetRouteDepartures(DepartureID);

            return AllDepartures;
        }

        public Departure GetDepartureByID(int DepartureID)
        {
            var OneDeparture = _repository.GetDepartureByID(DepartureID);

            return OneDeparture;
        }

        public bool AddDeparture(int RouteID, Departure NewDeparture)
        {
            var DepartureAdded = _repository.AddDeparture(RouteID, NewDeparture);

            return DepartureAdded;
        }

        public bool EditDeparture(int RouteID, Departure Departure)
        {
            var DepartureEdited = _repository.EditDeparture(Departure);

            return DepartureEdited;
        }

        public bool DeleteDeparture(int RouteID, int DepartureID)
        {
            var DepartureDeleted = _repository.DeleteDeparture(DepartureID);

            return DepartureDeleted;
        }
    }
}