using Esourcing.Sourcing.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Esourcing.Sourcing.Data
{
    public class SourcingContextSeed
    {
        public static void SeedData(IMongoCollection<Auction> auctionCollection)
        {
            bool exist = auctionCollection.Find(p => true).Any();
            if (!exist)
            {
                auctionCollection.InsertManyAsync(GetPreconfiguredAuctions());
            }
        }

        private static IEnumerable<Auction> GetPreconfiguredAuctions()
        {
            return new List<Auction>()
            {
                new Auction()
                {
                    Name = "Auction 1",
                    Description = "Auction Description",
                    CreateAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinisedAt = DateTime.Now.AddDays(10),
                    ProductId = "1",
                    IncludedSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active 
                },

                new Auction()
                {
                    Name = "Auction 2",
                    Description = "Auction2 Description",
                    CreateAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinisedAt = DateTime.Now.AddDays(11),
                    ProductId = "2",
                    IncludedSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active
                }

            };
        }
    }
}
