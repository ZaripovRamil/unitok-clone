using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using UI.Models;

namespace UI.Controllers
{
    [Route("/")]
    public class MainPageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuctionReader _auctionReader;
        private readonly IUserReader _userReader;
        private readonly ITokenReader _tokenReader;
        private readonly IStaticService _staticService;

        public MainPageController(UserManager<User> userManager,
            IAuctionReader auctionReader,
            IUserReader userReader,
            ITokenReader tokenReader,
            IStaticService staticService)
        {
            _userManager = userManager;
            _auctionReader = auctionReader;
            _userReader = userReader;
            _tokenReader = tokenReader;
            _staticService = staticService;
        }

        [HttpGet]
        [Route("/")]
		public async Task<IActionResult> MainPage()
        {
            ViewData["User"] = await _userManager.GetUserAsync(User);

            // var a = GetProfilePicture("default_pfp.jpg");

            var liveAuctions = _auctionReader.Read().Take(10).Select(x => new TokenCardVM
            {
                Author = x.Token.Author.UserName,
                Price = x.BestPrice,
                Title = x.Token.Name,
                AvatarSource = x.Token.Author?.ProfilePicUrl,
                CoverSource = x.Token.FileId,
                Id = x.Token.Id
            });

            var topSellers = _userReader.Read().Take(15).Select(x => new SellerVM
            {
                Name = x.UserName,
                AvatarSource = x.ProfilePicUrl,
                TotalProfit = x.Balance
            });

            var randomTokens = _tokenReader.Read().OrderBy(x => Guid.NewGuid()).Take(10).Select(x => new TokenCardVM
            {
                Author = x.Author.UserName,
                Title = x.Name,
				AvatarSource = x.Author.ProfilePicUrl,
				CoverSource = x.FileId,
                Id = x.Id
			});

			return View(new MainPageVM(liveAuctions, topSellers, randomTokens));
        }

        [Route("/pfp")]
		public IActionResult GetProfilePicture(string pictureUrl)
		{
			var profilePic = _staticService.GetFileAsStream("ProfilePicture", pictureUrl);
			if (profilePic is null) return NotFound();
			return File(profilePic, "image/jpeg");
		}

        [Route("nft")]
		public IActionResult GetNFT(string pictureUrl)
		{
			var profilePic = _staticService.GetFileAsStream("NFT", pictureUrl);
			if (profilePic is null) return NotFound();
			return File(profilePic, "image/jpeg");
		}
	}
}