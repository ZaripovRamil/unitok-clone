using Contracts.BackToFront.Light.Entities;

namespace UI.Models
{
    public class NftTokenVM
    {
        public bool isAuction { get; set; }
        public string TokenName { get; set; }
        public string Description { get; set; }
        public string TokenImageSrc { get; set; }
        public UserLight Owner { get; set; }
        public UserLight Author { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal MinimalBid { get; set; }
        public IEnumerable<BidLight> Bids { get; set; }
        public IEnumerable<TokenCardVM> AuthorTokens { get; set; }
    }
}
