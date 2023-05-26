using Contracts.BackToFront.Light;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using UI.Models;

namespace UI.Controllers
{
	[Route("explore")]
	public class ExploreController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly ITokenReader _tokenReader;
		private readonly IAuctionReader _auctionReader;
		private readonly IStaticService _staticService;

		public ExploreController(UserManager<User> userManager,
			ITokenReader tokenReader,
			IAuctionReader auctionReader,
			IStaticService staticService)
		{
			_userManager = userManager;
			_tokenReader = tokenReader;
			_auctionReader = auctionReader;
			_staticService = staticService;
		}

		[HttpGet]
		public async Task<IActionResult> Explore()
		{
			ViewData["User"] = await _userManager.GetUserAsync(User);

			var tokens = _tokenReader.Read().Select(x => new TokenCardVM
			{
				Author = x.Author.UserName,
				Title = x.Name,
				AvatarSource = x.Owner.ProfilePicUrl,
				Id = x.Id,
				CoverSource = "cute_fox.jpg"
			});

			return View(tokens);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> NftToken(string id)
		{
			ViewData["User"] = await _userManager.GetUserAsync(User);

			var token = _tokenReader.Read().FirstOrDefault(x => x.Id == id);

			if (token == null)
				return NotFound();

			var auction = _auctionReader
				.Read()
				.FirstOrDefault(x => x.Winner == null && x.Token.Id == id);

			var authorTokens = _tokenReader
				.Read()
				.Where(x => x.Author.UserName == token.Author.UserName)
				.Take(10)
				.Select(x => new TokenCardVM
				{
					Author = x.Author.UserName,
					Title = x.Name,
					AvatarSource = x.Owner.ProfilePicUrl,
					CoverSource = x.FileId
				});

			var tokenVM = new NftTokenVM
			{
				isAuction = false,
				TokenName = token.Name,
				Description = token.Description,
				TokenImageSrc = token.FileId,
				Owner = token.Owner,
				Author = token.Author,
				AuthorTokens = authorTokens
			};

			if (auction == null)
			{
				return View(tokenVM);
			}

			tokenVM.isAuction = true;
			tokenVM.Start = auction.Start;
			tokenVM.End = auction.Finish;
			tokenVM.MinimalBid = auction.StartPrice;
			tokenVM.Bids = auction.Bids;

			return View(tokenVM);
		}

        public IActionResult GetProfilePicture(string pictureUrl)
        {
            var profilePic = _staticService.GetFileAsStream("ProfilePicture", $"{pictureUrl}");
            if (profilePic is null) return NotFound();
            return File(profilePic, "image/jpeg");
        }
    }
}