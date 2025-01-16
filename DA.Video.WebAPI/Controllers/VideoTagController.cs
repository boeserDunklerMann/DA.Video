using DA.Video.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Video.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="16.01.2025" Entwickler="DA" />
	/// </ChangeLog>
	[Route("api/[controller]")]
	[ApiController]
	public class VideoTagController : ControllerBase
	{
		public VideoTagController(IConfiguration cfg, ILogger<VideoController> log, IDbContext db) : base(cfg, log, db)
		{
		}

		[HttpGet]
		public async Task<IEnumerable<VideoTag>> GetAllVideoTagsAsync()
		{
			return await context.Tags.ToListAsync();
		}
	}
}