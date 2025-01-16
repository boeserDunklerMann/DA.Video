
using DA.Video.Model;

namespace DA.Video.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="16.01.2025" Entwickler="DA" />
	/// <Change Datum="16.01.2025" Entwickler="DA">context renamed to db</Change>
	/// </ChangeLog>
	public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{
		protected IDbContext db;
		protected readonly IConfiguration configuration;
		protected readonly ILogger logger;
		public ControllerBase(IConfiguration cfg, ILogger<VideoController> log, IDbContext db)
		{
			configuration = cfg;
			logger = log;
			this.db = db;
			this.db.ConnectionString = configuration["ConnectionStrings:da-video-db"]!;
		}
	}
}