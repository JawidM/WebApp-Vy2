using System.Collections.Generic;
using VyModels;

namespace VyDAL
{
    public interface IPriceRepository
    {
        bool AddPrice(Price NewPrice);
        bool DeletePrice(int PriceID);
        bool EditPrice(Price OnePrice);
        List<string> GetAllPassengerTypes();
        List<Price> GetAllPrices();
        Price GetPriceByID(int? PriceID);
    }
}