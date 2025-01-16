using DA.Video.Model;
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
			var videos = await context.Videos.Include(nameof(IDbContext.Tags)).ToListAsync();

			// try enumerate files in Video-dir
			try
			{
				string searchpath = configuration["VideoSettings:VideoLocation"]!;
				logger.LogInformation($"searchPath: {searchpath}");
				string searchPattern = configuration["VideoSettings:VideoFileSearchPattern"]!;
				logger.LogInformation($"pattern: {searchPattern}");
				IEnumerable<string> files = Directory.EnumerateFileSystemEntries(searchpath, searchPattern, SearchOption.TopDirectoryOnly);
				bool itemAdded = false;
				foreach ( string file in files )
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
						await context.Videos.AddAsync(v);
						itemAdded = true;
					}
				}
				if (itemAdded) await context.SaveAsync();
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
		/// gets video details by filename
		/// </summary>
		/// https://andre-nas.servebeer.com/videoApi/api/Video/pornhub_720_03.mp4
		[HttpGet("{id}")]
		public async Task<VideoEntry> GetAsync(string id)
		{
			//VideoEntry entry = new() { ID = id };
			//string previewBaseUrl = configuration["VideoSettings:PreviewBaseUrl"]!;
			//entry.PreviewFile = previewBaseUrl + id + configuration["VideoSettings:PreviewExtension"]!;
			// DONE DA: fetch Title and Tags from DB
			VideoEntry? entry = await context.Videos.Include("Tags").FirstOrDefaultAsync(v=>v.ID == id);

			logger.LogInformation(entry?.ToString());
			return entry!;
		}

		// POST api/<ValuesController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}