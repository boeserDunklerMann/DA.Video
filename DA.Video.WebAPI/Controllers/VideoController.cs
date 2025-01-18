using DA.Video.Model;
using DA.Video.WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DA.Video.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="14.01.2025" Entwickler="DA" />
	/// <Change Datum="14.01.2025" Entwickler="DA">EF stuff added</Change>	
	/// <Change Datum="15.01.2025" Entwickler="DA">DI stuff added</Change>
	/// <Change Datum="16.01.2025" Entwickler="DA">first load db then scan directory</Change>
	/// <Change Datum="16.01.2025" Entwickler="DA">Post and Put added</Change>
	/// <Change Datum="16.01.2025" Entwickler="DA">context renamed to db</Change>
	/// <Change Datum="18.01.2025" Entwickler="DA">search videos by Tag</Change>
		/// </ChangeLog>
	[Route("api/[controller]")]
	[ApiController]
	public class VideoController(IConfiguration cfg, ILogger<VideoController> log, IDbContext db) : ControllerBase(cfg, log, db)
	{
		// GET: api/<ValuesController>
		/// <summary>
		/// Lists all videos
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IEnumerable<VideoEntry>> GetAllAsync()
		{
			// first load vids from db
			var videos = await db.Videos.ToListAsync();

			// try enumerate files in Video-dir
			try
			{
				string searchpath = configuration["VideoSettings:VideoLocation"]!;
				logger.LogInformation($"searchPath: {searchpath}");
				string searchPattern = configuration["VideoSettings:VideoFileSearchPattern"]!;
				logger.LogInformation($"pattern: {searchPattern}");
				IEnumerable<string> files = Directory.EnumerateFileSystemEntries(searchpath, searchPattern, SearchOption.TopDirectoryOnly);
				bool itemAdded = false;
				foreach (string file in files)
				{
					string filename = Path.GetFileName(file);
					if (!videos.Any(v => v.ID.Equals(filename)))
					{
						VideoEntry v = new()
						{
							ID = filename,
							PreviewFile = $"{filename}.gif"
						};
						videos.Add(v);
						await db.Videos.AddAsync(v);
						itemAdded = true;
					}
				}
				if (itemAdded) await db.SaveAsync();
			}
			catch (Exception e)
			{
				logger.LogError($"Exception was thrown {e.Message}");
				throw;
			}
			return videos;
		}

		// GET api/<ValuesController>/5
		/// <summary>
		/// gets video details by id (filename)
		/// </summary>
		[HttpGet("{id}")]
		public async Task<VideoEntry> GetAsync(string id)
		{
			VideoEntry? entry = await db.Videos.FirstOrDefaultAsync(v => v.ID == id);

			logger.LogInformation(entry?.ToString());
			return entry!;
		}

		[HttpGet("byTag/{tagsSearch}")]
		public async Task<IEnumerable<VideoEntry>> GetVideosByTagAsync(string tagsSearch)
		{
			return await db.Videos.Where(v => v.Tags.Contains(tagsSearch, StringComparison.OrdinalIgnoreCase)).ToListAsync();
		}

		// POST api/<ValuesController>
		[HttpPost]
		public async Task<IActionResult> CreateVideoEntry([FromBody] VideoEntry videoEntry)
		{
			db.Videos.Add(videoEntry);
			await db.SaveAsync();
			return Ok();

		}

		// PUT api/<ValuesController>/5
		[HttpPut()]
		public async Task<IActionResult> UpdateVideoEntry([FromBody] VideoEntry videoEntry)
		{
			VideoEntry? vFromDb = await db.Videos.FirstOrDefaultAsync(v => v.ID.Equals(videoEntry.ID)) ?? throw new ObjectNotFoundException(nameof(VideoEntry), videoEntry.ID);
			vFromDb.Title = videoEntry.Title;
			vFromDb.PreviewFile = videoEntry.PreviewFile;
			vFromDb.Tags = videoEntry.Tags;
			await db.SaveAsync();
			return Ok();
		}
	}
}