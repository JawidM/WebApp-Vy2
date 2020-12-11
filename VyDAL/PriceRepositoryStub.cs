using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class PriceRepositoryStub : IPriceRepository
    {
        public bool AddPrice(Price NewPrice)
        {
            var AllPrices = GetAllPrices();
            var PricesCount = AllPrices.Count();
            if (NewPrice.TicketPrice == 0.0 || NewPrice.PriceID == 0)
            {
                return false;
            }
            AllPrices.Add(NewPrice);
            if (AllPrices.Count() > PricesCount)
            {
                return true;
            }
            else return false;
        }

        public bool DeletePrice(int PriceID)
        {
            var AllPrices = GetAllPrices();
            var OnePrice = AllPrices.FirstOrDefault(p => p.PriceID == PriceID);
            if (OnePrice != null)
            {
                AllPrices.Remove(OnePrice);
                return true;
            }
            else return false;
        }

        public bool EditPrice(Price Price)
        {
            var AllPrices = GetAllPrices();
            var OnePrice = AllPrices.FirstOrDefault(p => p.PriceID == Price.PriceID);
            if (Price.TicketPrice == 0.0 || Price.PriceID == 0)
            {
                return false;
            }
            if (OnePrice != null)
            {
                OnePrice.PassengerType = Price.PassengerType;
                OnePrice.RouteID = Price.RouteID;
                OnePrice.TicketPrice = Price.TicketPrice;
                return true;
            }
            else return false;
        }

        public List<string> GetAllPassengerTypes()
        {
            return new List<string>
            {
                 "Adult",
                 "Student",
                 "Child"
            };
        }

        public List<Price> GetAllPrices()
        {
            return new List<Price>
            {
                new Price { PriceID = 1, RouteID = 1, PassengerType = "Adult", TicketPrice = 50 },
                new Price { PriceID = 2, RouteID = 1, PassengerType = "Student", TicketPrice = 30 },
                new Price { PriceID = 3, RouteID = 1, PassengerType = "Child", TicketPrice = 15 },
                new Price { PriceID = 4, RouteID = 2, PassengerType = "Adult", TicketPrice = 300 },
                new Price { PriceID = 5, RouteID = 2, PassengerType = "Student", TicketPrice = 150 },
                new Price { PriceID = 6, RouteID = 2, PassengerType = "Child", TicketPrice = 75 }
             };
        }

        public Price GetPriceByID(int? PriceID)
        {
            var AllPrices = GetAllPrices();
            return AllPrices.FirstOrDefault(p => p.PriceID == PriceID);
        }
    }
}