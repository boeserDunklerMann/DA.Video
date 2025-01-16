
using DA.Video.Model;

namespace DA.Video.WebAPI.Controllers
{
	/// <ChangeLog>
		/// <Create Datum="16.01.2025" Entwickler="DA" />
		/// </ChangeLog>
	public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{
		protected IDbContext context;
		protected readonly IConfiguration configuration;
		protected readonly ILogger logger;
		public ControllerBase(IConfiguration cfg, ILogger<VideoController> log, IDbContext db)
		{
			configuration = cfg;
			logger = log;
			context = db;
			context.ConnectionString = configuration["ConnectionStrings:da-video-db"]!;
		}
	}
}
