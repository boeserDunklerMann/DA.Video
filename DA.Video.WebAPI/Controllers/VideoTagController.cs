﻿using DA.Video.Model;
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

		[HttpGet("{videoId}")]
		public async Task<IEnumerable<VideoTag>> GetVideoTagsByVideoAsync(string videoId)
		{
			return await context.Videos.Include(nameof(IDbContext.Tags)).Where(v => v.ID == videoId).SelectMany(v => v.Tags).ToListAsync();
		}
	}
}