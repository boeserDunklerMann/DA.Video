using Microsoft.EntityFrameworkCore;

namespace DA.Video.Model
{
	/// <ChangeLog>
	/// <Create Datum="15.01.2025" Entwickler="DA" />
	/// </ChangeLog>
	public interface IDbContext
	{
		DbSet<VideoEntry> Videos { get; set; }
		DbSet<VideoTag> Tags { get; set; }
		string ConnectionString { get; set; }
	}
}