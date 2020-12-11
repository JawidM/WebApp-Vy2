using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyDAL;
using VyModels;

namespace VyBLL
{
    public class PriceLogic : IPriceLogic
    {
        private IPriceRepository _repository;

        public PriceLogic()
        {
            _repository = new PriceRepository();
        }

        public PriceLogic(IPriceRepository stub)
        {
            _repository = stub;
        }

        public List<Price> GetAllPrices()
        {
            var AllPrices = _repository.GetAllPrices();

            return AllPrices;
        }

        public List<string> GetAllPassengerTypes()
        {
            var AllPassengerTypes = _repository.GetAllPassengerTypes();

            return AllPassengerTypes;
        }

        public Price GetPriceByID(int? PriceID)
        {
            var OnePrice = _repository.GetPriceByID(PriceID);

            return OnePrice;
        }

        public bool AddPrice(Price NewPrice)
        {
            var PriceAdded = _repository.AddPrice(NewPrice);

            return PriceAdded;
        }

        public bool EditPrice(Price Price)
        {
            var PriceEdited = _repository.EditPrice(Price);

            return PriceEdited;
        }

        public bool DeletePrice(int id)
        {
            var PriceDeleted = _repository.DeletePrice(id);

            return PriceDeleted;
        }
    }
}