using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class PriceRepository : IPriceRepository
    {
        public Price MapPriceDbToPrice(PriceDb PriceDb)
        {
            using (var db = new DB())
            {

                Price OnePrice = new Price
                {
                    PriceID = PriceDb.PriceID,
                    RouteID = PriceDb.RouteID,
                    RouteName = db.Routes.Find(PriceDb.RouteID).RouteName,
                    TicketPrice = PriceDb.TicketPrice,
                    PassengerType = db.PassengerTypes.Find(PriceDb.PassengerTypeID).PassengerType
                };

                return OnePrice;
            }
        }

        public PriceDb MapPriceToPriceDb(Price OnePrice)
        {
            using (var db = new DB())
            {

                var OnePriceDb = new PriceDb
                {
                    PriceID = OnePrice.PriceID,
                    RouteID = OnePrice.RouteID,
                    TicketPrice = OnePrice.TicketPrice,
                    PassengerTypeID = db.PassengerTypes.SingleOrDefault(t => t.PassengerType == OnePrice.PassengerType).PassengerTypeID
                };

                return OnePriceDb;
            }
        }


        public List<Price> GetAllPrices()
        {
            using (var db = new DB())
            {
                var AllPricesDB = db.Prices.ToList();
                var AllPrices = new List<Price>();
                foreach (var P in AllPricesDB)
                {
                    AllPrices.Add(MapPriceDbToPrice(P));
                }

                return AllPrices;
            };
        }

        public List<string> GetAllPassengerTypes()
        {
            var AllPassengerTypes = new List<string>();
            using (var db = new DB())
            {
                var PassengerTypesDb = db.PassengerTypes.ToList();
                foreach (var PassengerType in PassengerTypesDb)
                {
                    AllPassengerTypes.Add(PassengerType.PassengerType);
                }
                return AllPassengerTypes;
            }
        }


        public Price GetPriceByID(int? PriceID)
        {
            using (var db = new DB())
            {
                if (PriceID == null)
                {
                    return null;
                }
                Price OnePrice = MapPriceDbToPrice(db.Prices.Find(PriceID));

                if (OnePrice == null)
                {
                    return null;
                }
                return OnePrice;
            }
        }


        public bool AddPrice(Price NewPrice)
        {
            using (var db = new DB())
            {
                var NewPriceDb = MapPriceToPriceDb(NewPrice);
                var PriceExsist = db.Prices.Where(p => p.RouteID == NewPriceDb.RouteID).SingleOrDefault(t => t.PassengerTypeID == NewPriceDb.PassengerTypeID);
                if (PriceExsist == null)
                {
                    db.Prices.Add(MapPriceToPriceDb(NewPrice));
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }

        public bool EditPrice(Price OnePrice)
        {
            using (var db = new DB())
            {
                var PassengerType = db.PassengerTypes.SingleOrDefault(t => t.PassengerType == OnePrice.PassengerType);

                var OnePriceDb = MapPriceToPriceDb(OnePrice);
                db.Entry(OnePriceDb).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
        }

        public bool DeletePrice(int PriceID)
        {
            try
            {
                using (var db = new DB())
                {
                    PriceDb PriceDb = db.Prices.Find(PriceID);
                    db.Prices.Remove(PriceDb);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
