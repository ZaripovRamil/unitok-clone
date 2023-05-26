namespace UI.Models
{
	public class MainPageVM
	{
		public IEnumerable<TokenCardVM> LiveAuctions { get; set; }
		public IEnumerable<SellerVM> TopSellers { get; set; }
		public IEnumerable<TokenCardVM> Tokens { get; set; }
		public MainPageVM(IEnumerable<TokenCardVM> liveAuctions, IEnumerable<SellerVM> topSellers, IEnumerable<TokenCardVM> tokens)
		{
			LiveAuctions = liveAuctions;
			TopSellers = topSellers;
			Tokens = tokens;
		}
	}
}
