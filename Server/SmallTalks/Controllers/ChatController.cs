using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmallTalks.Extensions;
using SmallTalks.Payloads;

namespace SmallTalks.Controllers
{

	public class ChatController : BaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IAzureBlobService _azureBlobService;

		public ChatController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAzureBlobService azureBlobService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_azureBlobService = azureBlobService;
		}

		[HttpGet]
		public async Task<ActionResult<List<string>>> GetAllNamesOfUsersAsync()
		{
			var userNames = await _userManager.GetAllUserNamesAsync();
			return Ok(userNames.ToList());
		}

		[HttpPost("upload/image")]
		public async Task<IActionResult> UploadImage([FromForm] UploadBlobPayload payload)
		{
			var url = await _azureBlobService.UploadToAzureBlob(BlobDirectories.Images, payload.File);
			return Ok(new {Url = url});
		}

		[HttpPost("upload/audio")]
		public async Task<IActionResult> UploadAudio([FromForm] UploadBlobPayload payload)
		{
			var url = await _azureBlobService.UploadToAzureBlob(BlobDirectories.Audios, payload.File);
			return Ok(new { Url = url });
		}
	}
}
