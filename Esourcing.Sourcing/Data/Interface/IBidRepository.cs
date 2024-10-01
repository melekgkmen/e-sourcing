using Esourcing.Sourcing.Entities;

namespace Esourcing.Sourcing.Data.Interface
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidsAuctionId(string id);
        Task<Bid> GetWinnerBid(string id); 
    }
}
