using Microsoft.EntityFrameworkCore;

namespace DA.Video.Model
{
	/// <ChangeLog>
	/// <Create Datum="15.01.2025" Entwickler="DA" />
	/// <Change Datum="16.01.2025" Entwickler="DA">prop Tags added</Change>
	/// </ChangeLog>
	public interface IDbContext
	{
		DbSet<VideoEntry> Videos { get; set; }
		string ConnectionString { get; set; }
		Task SaveAsync();
	}
}