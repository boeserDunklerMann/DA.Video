﻿using Microsoft.EntityFrameworkCore;

namespace DA.Video.Model
{
	/// <ChangeLog>
	/// <Create Datum="14.01.2025" Entwickler="DA" />
	/// <Change Datum="15.01.2025" Entwickler="DA">IDbContext created</Change>
	/// <Change Datum="16.01.2025" Entwickler="DA">prop Tags added</Change>
	/// </ChangeLog>
	public class VideoContext : DbContext, IDbContext
	{
		public DbSet<VideoEntry> Videos { get; set; }
		public string ConnectionString { get; set; } = "";
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// https://stackoverflow.com/questions/74060289/mysqlconnection-open-system-invalidcastexception-object-cannot-be-cast-from-d
			// MariaDB 11+ doesnt work because of nullable PKs?
			optionsBuilder.UseMySQL(ConnectionString);  // this db must be MariaDB10
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// https://learn.microsoft.com/de-de/ef/core/modeling/relationships/one-to-many
			// https://learn.microsoft.com/de-de/ef/core/modeling/relationships/many-to-many <--- das ist das Richtige!
			modelBuilder.Entity<VideoEntry>(ventry =>
			{
				ventry.HasKey(ve => ve.ID);
			});
		}
		public async Task SaveAsync()
		{
			await SaveChangesAsync();
		}
	}
}