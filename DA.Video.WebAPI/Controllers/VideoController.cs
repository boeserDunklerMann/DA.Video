using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DA.Video.WebAPI.Controllers
{
	/// <ChangeLog>
		/// <Create Datum="14.01.2025" Entwickler="DA" />
		/// </ChangeLog>
	[Route("api/[controller]")]
	[ApiController]
	public class VideoController(IConfiguration configuration, ILogger<VideoController> logger) : ControllerBase
	{
		// GET: api/<ValuesController>
		/// <summary>
		/// Lists all videos
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			// try enumerate files in Video-dir
			try
			{
				string searchpath = configuration["VideoSettings:VideoLocation"]!;
				logger.LogInformation($"searchPath: {searchpath}");
				string searchPattern = configuration["VideoSettings:VideoFileSearchPattern"]!;
				logger.LogInformation($"pattern: {searchPattern}");
				IEnumerable<string> files = Directory.EnumerateFileSystemEntries(searchpath, searchPattern, SearchOption.TopDirectoryOnly);
				var fArr = files.ToArray();
				for (int i = 0; i<fArr.Length; i++)
				{
					string fullpath = fArr[i];
					fArr[i] = Path.GetFileName(fullpath);
				}
				return fArr;
			}
			catch(Exception e)
			{
				logger.LogError($"Exception was thrown {e.Message}");
				throw;
			}
		}

		// GET api/<ValuesController>/5
		/// <summary>
		/// gets video details by filename
		/// </summary>
		/// https://andre-nas.servebeer.com/videoApi/api/Video/pornhub_720_03.mp4
		[HttpGet("{id}")]
		public VideoEntry Get(string id)
		{
			VideoEntry entry = new() { ID=id};
			string previewBaseUrl = configuration["VideoSettings:PreviewBaseUrl"]!;
			entry.PreviewFile = previewBaseUrl + id + configuration["VideoSettings:PreviewExtension"]!;
			// TODO DA: fetch Title and Tags from DB

			logger.LogInformation(entry.ToString());
			return entry;
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
