using DA.Video.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Video.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="16.01.2025" Entwickler="DA" />
	/// <Change Datum="16.01.2025" Entwickler="DA">Controller made RestFUL</Change>
		/// </ChangeLog>
	[Route("api/[controller]")]
	[ApiController]
	public class VideoTagController(IConfiguration cfg, ILogger<VideoController> log, IDbContext db) : ControllerBase(cfg, log, db)
	{
		[HttpGet]
		public async Task<IEnumerable<VideoTag>> GetAllVideoTagsAsync()
		{
			return await db.Tags.ToListAsync();
		}

		[HttpGet("byVideo/{videoId}")]
		public async Task<IEnumerable<VideoTag>> GetVideoTagsByVideoAsync(string videoId)
		{
			return await db.Videos.Include(nameof(IDbContext.Tags)).Where(v => v.ID == videoId).SelectMany(v => v.Tags).ToListAsync();
		}

		[HttpGet("byText/{filterText}")]
		public async Task<IEnumerable<VideoTag>> GetVideoTagsByFilterAsync(string filterText)
		{
			return await db.Tags.Where(vt => vt.Tag.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0).ToListAsync();
		}

		[HttpPost]
		public async Task<IActionResult> CreateVideoTagAsync([FromBody] VideoTag videoTag)
		{
			await db.Tags.AddAsync(videoTag);
			await db.SaveAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateVideoTagAsync([FromBody] VideoTag videoTag)
		{
			VideoTag? vtFromDb = await db.Tags.FindAsync(videoTag.ID);
			if (vtFromDb == null)
				throw new Exceptions.ObjectNotFoundException(nameof(VideoTag), videoTag.ID);
			vtFromDb.Tag=videoTag.Tag;
			await db.SaveAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteVideoTagAsync([FromBody] VideoTag videoTag)
		{
			VideoTag? vtFromDb = await db.Tags.FindAsync(videoTag.ID);
			if (vtFromDb == null)
				throw new Exceptions.ObjectNotFoundException(nameof(VideoTag), videoTag.ID);
			db.Tags.Remove(vtFromDb);
			await db.SaveAsync();
			return Ok();
		}
	}
}