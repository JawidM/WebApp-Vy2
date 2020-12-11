using System.Collections.Generic;
using VyModels;

namespace VyBLL
{
    public interface IPriceLogic
    {
        bool AddPrice(Price NewPrice);
        bool DeletePrice(int id);
        bool EditPrice(Price Price);
        List<string> GetAllPassengerTypes();
        List<Price> GetAllPrices();
        Price GetPriceByID(int? PriceID);
    }
}