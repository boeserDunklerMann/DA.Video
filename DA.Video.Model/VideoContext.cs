using Microsoft.EntityFrameworkCore;

namespace DA.Video.Model
{
	/// <ChangeLog>
	/// <Create Datum="14.01.2025" Entwickler="DA" />
	/// <Change Datum="15.01.2025" Entwickler="DA">IDbContext created</Change>
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
			modelBuilder.Entity<VideoTag>(tag =>
			{
				tag.HasKey(t => t.ID);
			});
			modelBuilder.Entity<VideoEntry>(ventry =>
			{
				ventry.HasKey(ve => ve.ID);
				ventry.HasMany(ve => ve.Tags);
			});
			/*
					modelBuilder.Entity<User>(user =>
					{
						user.HasKey(u => u.Id);
						user.HasMany(u => u.Allergies);
						user.HasMany(u => u.AddressList).WithOne(a => a.User);
						user.HasMany(u => u.CommunicationList).WithOne(c => c.User);
						user.HasMany(u => u.Pets);
						user.Property(u => u.GoogleId).IsRequired(false);
						user.HasIndex(u => u.GoogleId).HasFilter("LABEL IS NOT NULL")
						.IsUnique();
					});
					modelBuilder.Entity<Allergy>(allergy =>
					{
						allergy.HasKey(a => a.Id);

					});
					modelBuilder.Entity<EatingHabit>(eh =>
					{
						eh.HasKey(e => e.Id);
					});
					modelBuilder.Entity<Communication>(communication =>
					{
						communication.HasKey(c => c.Id);
						communication.HasOne(c => c.CommunicationType);
					});
					modelBuilder.Entity<Address>(address =>
					{
						address.HasKey(a => a.Id);
						address.HasOne(a => a.Country);
					});
					modelBuilder.Entity<Dinner>(dinner =>
					{
						dinner.HasKey(d => d.Id);
						dinner.HasMany(d => d.Reviews).WithOne(r => r.Dinner);
						dinner.HasOne(d => d.Host).WithMany(u => u.DinnerAsHost);
						dinner.HasMany(d => d.Cooks).WithMany(u => u.DinnerAsCook).UsingEntity("DinnerCook");
						dinner.HasMany(d => d.Guests).WithMany(u => u.DinnerAsGuest).UsingEntity("DinnerGuest");
					});
					modelBuilder.Entity<DinnerReview>(dr =>
					{
						dr.HasKey(d => d.Id);
						dr.HasOne(d => d.ReviewsAuthor).WithMany(u => u.Reviews);
						dr.HasOne(d => d.Dinner).WithMany(d => d.Reviews);
					});
					modelBuilder.Entity<UserImage>(image =>
					{
						image.HasKey(i => i.Id);
						image.HasOne(i => i.User).WithMany(u => u.UserImages);
					});
				}*/
		}
	}
}
